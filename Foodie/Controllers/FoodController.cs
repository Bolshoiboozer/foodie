using Foodie.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Foodie.Controllers
{
    public class FoodController : ApiController
    {
        // GET: api/Foods
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        [ActionName("getRandomFood")]
        public List<Food> getRandomFood()
        {
            List<Food> randomFoods = Food.CreateRandomFood();
            return randomFoods;
        }

        [HttpGet]
        [ActionName("searchIngredient")]
        public List<String> searchIngredient(String id)
        {
            return Food.searchIngredient(id);
        }

        [HttpGet]
        [ActionName("searchFood")]
        public List<Food> searchFood(String id)
        {
            List<Food> sendFood = new List<Food>();
            sendFood = Food.searchFood(id);

            String path = "C:\\inetpub\\wwwroot\\Foodie\\searchFood.txt";
            File.WriteAllText(path, id);

            return sendFood;
        }

        [HttpPost]
        [ActionName("calculateFood")]
        public List<Food> calculateFood([FromBody]String[] IngredientsList)
        {
            String path = "C:\\inetpub\\wwwroot\\Foodie\\calculateFood.txt";
            File.WriteAllText(path, IngredientsList[0]);
            return Food.calculateFood(IngredientsList);
        }

        [HttpPost]
        [ActionName("userRegister")]
        public HttpWebResponse userRegister([FromBody]String[] userId)
        {

            SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\AS-Bolshoi\\source\\repos\\Foodie\\Foodie\\App_Data\\Foodie.mdf;Integrated Security=True");

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO Users(UserID) VALUES(@param1)";

                cmd.Parameters.AddWithValue("@param1", userId[0]);

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    return null;
                }

            }

            connection.Close();
            String path = "C:\\inetpub\\wwwroot\\Foodie\\userRegister.txt";
            File.WriteAllText(path, userId[0]);
            return null;
        }

        [HttpPost]
        [ActionName("setAllergens")]
        public HttpWebResponse setAllergens([FromBody]String[] allergen)
        {
            /*
            SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\AS-Bolshoi\\source\\repos\\Foodie\\Foodie\\App_Data\\Foodie.mdf;Integrated Security=True");

            connection.Open();

            string sql = "INSERT INTO Users(UserID) VALUES(@param1)";
            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@param1", userId);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();


            connection.Close();
            String path = "C:\\inetpub\\wwwroot\\Foodie\\userRegisters.txt";
            File.WriteAllText(path, userId);*/

            String path = "C:\\inetpub\\wwwroot\\Foodie\\setAllergens.txt";
            File.WriteAllText(path, allergen[0]);
            return null;
        }

        [HttpPost]
        [ActionName("addFavorite")]
        public HttpWebResponse addFavorite([FromBody]String[] foodUri)
        {
            String path = "C:\\inetpub\\wwwroot\\Foodie\\addFavorite.txt";
            File.WriteAllText(path, foodUri[0]);
            //neural networke 3 kere sokulacak
            return null;
        }

        [HttpPost]
        [ActionName("setMostLoved")]
        public HttpWebResponse setMostLoved([FromBody]String[] foodUri)
        {
            Food.setMostLoved(foodUri);
            return null;
        }
        [HttpPost]
        [ActionName("deleteFavorite")]
        public HttpWebResponse deleteFavorite([FromBody]String[] foodUri)
        {
            String path = "C:\\inetpub\\wwwroot\\Foodie\\deleteFavorite.txt";
            File.WriteAllText(path, foodUri[0]);
            //neural networke 3 kere ters sokulacak
            return null;
        }

        [HttpPost]
        [ActionName("clickedRecipe")]
        public HttpWebResponse clickedRecipe([FromBody]String[] foodUri)
        {
            String path = "C:\\inetpub\\wwwroot\\Foodie\\clickedRecipe.txt";
            File.WriteAllText(path, foodUri[0]);
            //neural networke 1 kere sokulacak
            return null;
        }
        [HttpPost]
        [ActionName("deleteFromList")]
        public HttpWebResponse deleteFromList([FromBody]String[] foodUri)
        {
            String path = "C:\\inetpub\\wwwroot\\Foodie\\deleteFromList.txt";
            File.WriteAllText(path, foodUri[0]);
            //neural networke 1 kere sokulacak
            return null;
        }
        
        [HttpPost]
        [ActionName("postAnswers")]
        public HttpWebResponse postAnswers([FromBody]double[] answers)
        {
            List<double[]> sendAnswers = new List<double[]>();
            for (int i = 0; i < answers.Length; i++)
            {
                if (answers[i] == 1)
                    sendAnswers.Add((new double[2] { 1.0, 0.0 }));
                else if (answers[i]== 0)
                    sendAnswers.Add((new double[2] { 0.0, 1.0 }));
                else if(answers[i] == -1)
                    sendAnswers.Add((new double[2] { 0.5, 0.5 }));
                else if(answers[i] == 5)
                    sendAnswers.Add((new double[2] { 5.0, 5.0 }));
                else if (answers[i] == 4)
                    sendAnswers.Add((new double[2] { 4.0, 4.0 }));

            }

            String a = "";
            for (int i = 0; i < answers.Length; i++)
                a += answers[i] + " ";
            String path2 = "C:\\inetpub\\wwwroot\\Foodie\\answers.txt";
            File.WriteAllText(path2, a);

            Food.getAnswers(Food.FoodList, sendAnswers);
            return null;
        }



        // POST: api/Foods
        public void Post([FromBody]string value)
        {
            
        }

        // PUT: api/Foods/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Foods/5
        public void Delete(int id)
        {
        }
    }
}
