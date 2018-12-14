using System;
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

        public String FoodUri;
        public String FoodName;
        public String RecipeLink;
        public String ImageLink;
        public List<String> Ingredients;

        public Food() { }

        public Food(String fUri, String name, String rLink, String iLink, List<String> ingr)
        {
            this.FoodUri = fUri;
            this.FoodName = name;
            this.RecipeLink = rLink;
            this.ImageLink = iLink;
            this.Ingredients = ingr;

        }

        public String displayFood(Food f)
        {
            String display = "FoodUri " + f.FoodUri + "<br>";
            display += "FoodName: " + f.FoodName + "<br>";
            display += "RecipeLink: " + f.RecipeLink + "<br>";
            display += "ImageLink: " + f.ImageLink + "<br>";
            display += "Ingredients: ";
            for (int i = 0; i < f.Ingredients.Count; i++)
                display += f.Ingredients[i] + "<br>";
            return display;
        }



        public static List<Food> CreateRandomFood()
        {
            FoodList.Clear();
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
                string url = @"https://api.edamam.com/search?q=+" + AllIngredients[rn] + "&app_id=d378a03e&app_key=84ee37aef5d7110f94d119e922ae96f2&from=0&to=1&calories=591-722&health=alcohol-free";

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
                String fUri = data.hits[0].recipe.uri;
                String foodName = data.hits[0].recipe.label;
                String recipeLink = data.hits[0].recipe.url;
                String imageLink = data.hits[0].recipe.image;
                List<String> Ingredients = new List<string>();
                for (int k = 0; k < data.hits[0].recipe.ingredients.Count; k++)
                    Ingredients.Add(data.hits[0].recipe.ingredients[k].text);
                FoodList.Add(new Food(fUri, foodName, recipeLink, imageLink, Ingredients));
            }
            return FoodList;
        }

        public static Food getFood(String[] IngredientsList)
        {
            string url = @"https://api.edamam.com/search?q=";
            for (int j = 0; j < IngredientsList.Length-1; j++)
                url +=  IngredientsList[j] + ",";
            url += IngredientsList[IngredientsList.Length - 1];
            url+= "&app_id=d378a03e&app_key=84ee37aef5d7110f94d119e922ae96f2&from=0&to=1&calories=591-722&health=alcohol-free";
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
            String fUri = data.hits[0].recipe.uri;
            String foodName = data.hits[0].recipe.label;
            String recipeLink = data.hits[0].recipe.url;
            String imageLink = data.hits[0].recipe.image;
            List<String> Ingredients = new List<string>();
            for (int k = 0; k < data.hits[0].recipe.ingredients.Count; k++)
                Ingredients.Add(data.hits[0].recipe.ingredients[k].text);
            Food f = new Food(fUri, foodName, recipeLink, imageLink, Ingredients);
            return f;
        }
    }
}
