﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace Foodie.Models
{
    public class Food
    {
        public static List<String> AllIngredients = new List<String>();
        public static List<Food> FoodList = new List<Food>();
        public static List<Food> RandomFoodList = new List<Food>();

        public String FoodUri;
        public String FoodName;
        public String FoodRecipe;
        public String Image;
        public List<String> Ingredients;

        public Food() { }

        public Food(String fUri, String name, String rLink, String iLink, List<String> ingr)
        {
            this.FoodUri = fUri;
            this.FoodName = name;
            this.FoodRecipe = rLink;
            this.Image = iLink;
            this.Ingredients = ingr;

        }

        public static double[] convertFood(Food f)
        {
            AllIngredients.Clear();
            List<double> convertedInputs = new List<double>();
            SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\AS-Bolshoi\\source\\repos\\Foodie\\Foodie\\App_Data\\Foodie.mdf;Integrated Security=True");

            connection.Open();
            //Read from the database
            SqlCommand command = new SqlCommand("SELECT IngredientName FROM IngredientList", connection);

            SqlDataReader dataReader = command.ExecuteReader();
            int count = 0;
            while (dataReader.Read())
            {
                AllIngredients.Add(dataReader[0].ToString());
                count++;
            }
            connection.Close();
            String IngredientString = "";
            for (int i = 0; i < f.Ingredients.Count; i++)
                IngredientString += f.Ingredients[i] + " ";
            IngredientString=IngredientString.ToLower();
            String controlstring = "";
            String ing = "";
            for (int j = 0; j < AllIngredients.Count; j++)
            {
                ing += AllIngredients[j] + ",";
                if (IngredientString.Contains(AllIngredients[j]))
                {
                    convertedInputs.Add(1);
                    
                    controlstring += AllIngredients[j];
                }
                else
                {
                    convertedInputs.Add(0);
                }
            }
            double[] convertedInp = new double[convertedInputs.Count];
            for (int i = 0; i < convertedInputs.Count; i++)
                convertedInp[i] = convertedInputs[i];
            /*
            String converted = "";
            for (int i = 0; i < convertedInputs.Count; i++)
                converted += convertedInputs[i];
            String path = "C:\\inetpub\\wwwroot\\Foodie\\convertFood.txt";
            File.WriteAllText(path, converted);
            String path2 = "C:\\inetpub\\wwwroot\\Foodie\\controlString.txt";
            File.WriteAllText(path2, controlstring);
            String path3 = "C:\\inetpub\\wwwroot\\Foodie\\ing.txt";
            File.WriteAllText(path3, ing);
            String path4 = "C:\\inetpub\\wwwroot\\Foodie\\ing2.txt";
            File.WriteAllText(path4, IngredientString);
            */
            return convertedInp;
        }
        public String displayFood(Food f)
        {
            String display = "FoodUri " + f.FoodUri + "<br>";
            display += "FoodName: " + f.FoodName + "<br>";
            display += "FoodRecipe: " + f.FoodRecipe + "<br>";
            display += "Image: " + f.Image + "<br>";
            display += "Ingredients: ";
            for (int i = 0; i < f.Ingredients.Count; i++)
                display += f.Ingredients[i] + "<br>";
            return display;
        }
        public static List<Food> CreateRandomFood()
        {
            FoodList.Clear();
            AllIngredients.Clear();
            SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\AS-Bolshoi\\source\\repos\\Foodie\\Foodie\\App_Data\\Foodie.mdf;Integrated Security=True");

            connection.Open();
            //Read from the database
            SqlCommand command = new SqlCommand("SELECT IngredientName FROM IngredientList", connection);

            SqlDataReader dataReader = command.ExecuteReader();
            int count = 0;
            while (dataReader.Read())
            {
                AllIngredients.Add(dataReader[0].ToString());
                count++;
            }
            connection.Close();

            
                for (int j = 0; j < 5; j++)
                {

                    Random random = new Random();
                    int rn = random.Next(0, 812);
                    string html = string.Empty;
                    string url = @"https://api.edamam.com/search?q=+" + AllIngredients[rn] + "&app_id=d378a03e&app_key=84ee37aef5d7110f94d119e922ae96f2&from=0&to=5";

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.AutomaticDecompression = DecompressionMethods.GZip;

                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    using (Stream stream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        html = reader.ReadToEnd();
                    }

                    Console.WriteLine(html);

                    string json = html;

                    var serializer = new JavaScriptSerializer();
                    serializer.RegisterConverters(new[] { new DynamicJsonConverter() });

                    dynamic data = serializer.Deserialize(json, typeof(object));
                    Console.WriteLine(data.hits[0].recipe.uri);
                    /*deneme.InnerHtml = data.hits[0].recipe.label + "alp";*/
                    for (int i = 0; i < data.hits.Count; i++)
                    {
                        if (i == 5)
                            break;
                        String fUri = data.hits[i].recipe.uri;
                        String foodName = data.hits[i].recipe.label;
                        String FoodRecipe = data.hits[i].recipe.url;
                        String Image = data.hits[i].recipe.image;
                        List<String> Ingredients = new List<string>();
                        for (int k = 0; k < data.hits[i].recipe.ingredients.Count; k++)
                            Ingredients.Add(data.hits[i].recipe.ingredients[k].text);
                        FoodList.Add(new Food(fUri, foodName, FoodRecipe, Image, Ingredients));
                    }
                }
            
            return FoodList;
        }
        public static List<Food> calculateFood(String[] IngredientsList)
        {
            List<Food> getFoods = new List<Food>();
            List<Food> sendFoods = new List<Food>();
            string url = @"https://api.edamam.com/search?q=";
            for (int j = 0; j < IngredientsList.Length - 1; j++)
                url += IngredientsList[j] + ",";
            url += IngredientsList[IngredientsList.Length - 1];
            url += "&app_id=d378a03e&app_key=84ee37aef5d7110f94d119e922ae96f2&from=0&to=5";
            string html = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            Console.WriteLine(html);
            string json = html;
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new[] { new DynamicJsonConverter() });

            dynamic data = serializer.Deserialize(json, typeof(object));
            Console.WriteLine(data.hits[0].recipe.uri);
            for (int j = 0; j < data.hits.Count; j++)
            {
                String fUri = data.hits[j].recipe.uri;
                String foodName = data.hits[j].recipe.label;
                String FoodRecipe = data.hits[j].recipe.url;
                String Image = data.hits[j].recipe.image;
                List<String> Ingredients = new List<string>();
                for (int k = 0; k < data.hits[j].recipe.ingredients.Count; k++)
                    Ingredients.Add(data.hits[j].recipe.ingredients[k].text);
                Food f = new Food(fUri, foodName, FoodRecipe, Image, Ingredients);
                getFoods.Add(f);
            }
            List<double[]> inputs = new List<double[]>();
            double[] answers = new double[getFoods.Count];
            BackPropProgram bp = new BackPropProgram();
            double[] brain = bp.getBrain();
            for (int i = 0; i < getFoods.Count; i++)
            {
                inputs.Add(Food.convertFood(getFoods[i]));
                answers[i]=bp.testFood(brain, inputs[i]);
            }
            Food[] myFoods = new Food[getFoods.Count];
            for (int i = 0; i < getFoods.Count; i++)
                myFoods[i] = getFoods[i];
            //Array.Sort(answers, myFoods);

            String getFood = "";
            String myFood = "";

            for (int i = 0; i < getFoods.Count; i++)
                sendFoods.Add(myFoods[i]);

            for (int i = 0; i < getFoods.Count; i++)
                myFood += answers[i];

            String path = "C:\\inetpub\\wwwroot\\Foodie\\getFood.txt";
            File.WriteAllText(path, getFood);

            String path2 = "C:\\inetpub\\wwwroot\\Foodie\\myFood.txt";
            File.WriteAllText(path2, myFood);

            return sendFoods;
        }
        public static List<String> searchIngredient(String s)
        {
            int counter = 0;
            AllIngredients.Clear();
            List<String> foundIngredients = new List<String>();
            SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\AS-Bolshoi\\source\\repos\\Foodie\\Foodie\\App_Data\\Foodie.mdf;Integrated Security=True");

            connection.Open();
            //Read from the database
            SqlCommand command = new SqlCommand("SELECT IngredientName FROM IngredientList ORDER BY IngredientName ASC", connection);

            SqlDataReader dataReader = command.ExecuteReader();
            int count = 0;
            while (dataReader.Read())
            {
                AllIngredients.Add(dataReader[0].ToString());
                count++;
            }
            connection.Close();

            for(int j=0; j<AllIngredients.Count; j++)
            {
                if (counter == 25)
                    break;
                if (AllIngredients[j].Contains(s.ToLower()))
                {
                    foundIngredients.Add(AllIngredients[j]);
                    counter++;
                }

            }
            return foundIngredients;
        }
        public static List<Food> searchFood(String s)
        {
            List<Food> getFoods = new List<Food>();
            List<Food> sendFoods = new List<Food>();
            string url = @"https://api.edamam.com/search?q=" + s + "&app_id=d378a03e&app_key=84ee37aef5d7110f94d119e922ae96f2&from=0&to=20"; 
            string html = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            Console.WriteLine(html);
            string json = html;
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new[] { new DynamicJsonConverter() });

            dynamic data = serializer.Deserialize(json, typeof(object));
            Console.WriteLine(data.hits[0].recipe.uri);
            for (int j = 0; j < data.hits.Count; j++)
            {
                String fUri = data.hits[j].recipe.uri;
                String foodName = data.hits[j].recipe.label;
                String FoodRecipe = data.hits[j].recipe.url;
                String Image = data.hits[j].recipe.image;
                List<String> Ingredients = new List<string>();
                for (int k = 0; k < data.hits[j].recipe.ingredients.Count; k++)
                    Ingredients.Add(data.hits[j].recipe.ingredients[k].text);
                Food f = new Food(fUri, foodName, FoodRecipe, Image, Ingredients);
                getFoods.Add(f);
            }
            for(int i=0; i<getFoods.Count; i++)
            {
                if (getFoods[i].FoodName.Contains(s))
                    sendFoods.Add(getFoods[i]);
                else if(getFoods[i].FoodName.ToLower().Contains(s.ToLower()))
                    sendFoods.Add(getFoods[i]);
            }
            return sendFoods;
        }

        public static List<Food> searchFoods(String s)
        {
            List<Food> getFoods = new List<Food>();
            List<Food> sendFoods = new List<Food>();
            string url = @"https://api.edamam.com/search?q=" + s + "&app_id=d378a03e&app_key=84ee37aef5d7110f94d119e922ae96f2&from=0&to=5";
            string html = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            Console.WriteLine(html);
            string json = html;
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new[] { new DynamicJsonConverter() });

            dynamic data = serializer.Deserialize(json, typeof(object));
            Console.WriteLine(data.hits[0].recipe.uri);
            for (int j = 0; j < data.hits.Count; j++)
            {
                String fUri = data.hits[j].recipe.uri;
                String foodName = data.hits[j].recipe.label;
                String FoodRecipe = data.hits[j].recipe.url;
                String Image = data.hits[j].recipe.image;
                List<String> Ingredients = new List<string>();
                for (int k = 0; k < data.hits[j].recipe.ingredients.Count; k++)
                    Ingredients.Add(data.hits[j].recipe.ingredients[k].text);
                Food f = new Food(fUri, foodName, FoodRecipe, Image, Ingredients);
                getFoods.Add(f);
            }

            
            return getFoods;
        }

        public static List<Food> searchFoods2(String s)
        {
            List<Food> getFoods = new List<Food>();
            List<Food> sendFoods = new List<Food>();
            string url = @"https://api.edamam.com/search?q=" + s + "&app_id=d378a03e&app_key=84ee37aef5d7110f94d119e922ae96f2&from=5&to=10";
            string html = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            Console.WriteLine(html);
            string json = html;
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new[] { new DynamicJsonConverter() });

            dynamic data = serializer.Deserialize(json, typeof(object));
            Console.WriteLine(data.hits[0].recipe.uri);
            for (int j = 0; j < data.hits.Count; j++)
            {
                String fUri = data.hits[j].recipe.uri;
                String foodName = data.hits[j].recipe.label;
                String FoodRecipe = data.hits[j].recipe.url;
                String Image = data.hits[j].recipe.image;
                List<String> Ingredients = new List<string>();
                for (int k = 0; k < data.hits[j].recipe.ingredients.Count; k++)
                    Ingredients.Add(data.hits[j].recipe.ingredients[k].text);
                Food f = new Food(fUri, foodName, FoodRecipe, Image, Ingredients);
                getFoods.Add(f);
            }


            return getFoods;
        }

        public static void getAnswers(List<Food> foods, List<double[]> answers)
        {

            BackPropProgram bp = new BackPropProgram();
            double[] brain;

            List<double[]> inputs = new List<double[]>();
            for (int i = 0; i < foods.Count; i++)
            {
                inputs.Add(Food.convertFood(foods[i]));
            }

            if (answers[answers.Count - 1][0] == 5.0)
            {
                brain = bp.generateBrain();
            }
            else if (answers[answers.Count - 1][0] == 4.0)
            {
                brain = bp.getBrain();
            }
            else
                brain = null;

            bp.learningFood(brain, inputs, answers);
            for(int i=0; i<inputs.Count; i++)
                bp.testFood(bp.getBrain(), inputs[i]);

        }

        public static void setMostLoved(String[] foodUri)
        {
            List<Food> foods = Food.convertUri(foodUri);
            List<double[]> inputs = new List<double[]>();
            List<double[]> answers = new List<double[]>();
            BackPropProgram bp = new BackPropProgram();
            double[] brain = bp.getBrain();
            for (int i = 0; i < foods.Count; i++)
            {
                inputs.Add(Food.convertFood(foods[i]));
                answers.Add((new double[2] { 1.0, 0.0 }));
            }
            bp.testFood(bp.getBrain(), inputs[0]);
            for (int i=0; i<5; i++)
                bp.learningFood(brain, inputs, answers);
            bp.testFood(bp.getBrain(), inputs[0]);
        }

        public static List<Food> convertUri(String[] foodUri)
        {
            List<Food> getFoods = new List<Food>();
            for (int i=0; i<foodUri.Length; i++)
            {
                foodUri[i]=foodUri[i].Replace(":", "%3A");
                foodUri[i]=foodUri[i].Replace("/", "%2F");
                foodUri[i]=foodUri[i].Replace("#", "%23");
            }

            String deneme = "";
            for (int i = 0; i < foodUri.Length; i++)
                deneme += foodUri[i];
            String path3 = "C:\\inetpub\\wwwroot\\Foodie\\deneme.txt";
            File.WriteAllText(path3, deneme);

  
            for (int i = 0; i < foodUri.Length; i++)
            {
                string url = @"https://api.edamam.com/search?r=" + foodUri[i] + "&app_id=d378a03e&app_key=84ee37aef5d7110f94d119e922ae96f2&from=0&to=1";
                string html = string.Empty;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                }

                Console.WriteLine(html);
                string json = html;
                var serializer = new JavaScriptSerializer();
                serializer.RegisterConverters(new[] { new DynamicJsonConverter() });

                dynamic data = serializer.Deserialize(json, typeof(object));

                for (int j = 0; j < 1; j++)
                {
                    String fUri = data[0].uri;
                    String foodName = data[0].label;
                    String FoodRecipe = data[0].url;
                    String Image = data[0].image;
                    List<String> Ingredients = new List<string>();
                    for (int k = 0; k < data[0].ingredients.Count; k++)
                        Ingredients.Add(data[0].ingredients[k].text);
                    Food f = new Food(fUri, foodName, FoodRecipe, Image, Ingredients);
                    getFoods.Add(f);
                }
                return getFoods;
            }
            return null;
        }

    }
}
