using Foodie.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Foodie
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*String yazi="";
            List<Foods> myFoods= Foods.CreateRandomFood();
            for (int i = 0; i < myFoods.Count; i++)
                yazi+=myFoods[i].displayFood(myFoods[i]);

            deneme.InnerHtml = yazi;*/

            /*
            String[] listIng = new String[3];
            listIng[0] = "chicken";
            listIng[1] = "oil";
            listIng[2] = "tomato";

            BackPropProgram bp = new BackPropProgram();
            double[] brain = bp.generateBrain();
            
            List<Food> foods = Food.searchFoods("chicken, oil, tomato");
            double[] cevaplar = new double[foods.Count];
            List<double[]> inputs = new List<double[]>();
            List<double[]> answers = new List<double[]>();
            answers.Add(new double[2] { 1, 0 });
            answers.Add(new double[2] { 1, 0 });
            answers.Add(new double[2] { 0, 1 });
            answers.Add(new double[2] { 0, 1 });
            answers.Add(new double[2] { 1, 0 });

            for (int i = 0; i < foods.Count; i++) {
                inputs.Add(Food.convertFood(foods[i]));
                

            }
            bp.learningFood(brain, inputs, answers);
            /*for (int i = 0; i < foods.Count; i++)
            {
                cevaplar[i] = bp.testFood(bp.getBrain(), inputs[i]);
            }

            String yazi = "";
            for (int i = 0; i < cevaplar.Length; i++)
                yazi += cevaplar[i] + " ";*/



            /*
            List<Food> foods2 = Food.searchFoods2("chicken, oil, tomato");
            double[] cevaplar2 = new double[foods.Count];
            List<double[]> inputs2 = new List<double[]>();

            for (int i = 0; i < foods.Count; i++)
            {
                inputs2.Add(Food.convertFood(foods2[i]));


            }
            for (int i = 0; i < foods.Count; i++)
            {
                cevaplar2[i] = bp.testFood(bp.getBrain(), inputs2[i]);
            }



            

            for (int i = 0; i < cevaplar.Length; i++)
                yazi += cevaplar[i] + " ";

            */
            /*
            List<String> userID = new List<String>();
            SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\AS-Bolshoi\\source\\repos\\Foodie\\Foodie\\App_Data\\Foodie.mdf;Integrated Security=True");
            connection.Open();
            //Read from the database
            SqlCommand command = new SqlCommand("SELECT UserID FROM Users", connection);
            SqlDataReader dataReader = command.ExecuteReader();
            int count = 0;
            while (dataReader.Read())
            {
                userID.Add(dataReader[0].ToString());
                count++;
            }
            connection.Close();
            for (int i = 0; i < userID.Count; i++)
            {
                yazi += userID[i] + "</br>";
            }

            /*
            Food f= Food.searchFood("apple,chicken")[0];
            Food.convertFood(f);
            String yazi = f.displayFood(f);*/


            /*
              String yazi = "";
              String[] listIng= new String[3];
              listIng[0] = "chicken";
              listIng[1] = "oil";
              listIng[2] = "tomato";

              List<Food> myFood = Food.calculateFood(listIng);
              for (int i = 0; i < myFood.Count; i++)
              {
                  yazi += myFood[i].displayFood(myFood[i]);
              }*/
            List<Food> alp = new List<Food>();
            String[] alper = new String[1];
            alper[0] = "http://www.edamam.com/ontologies/edamam.owl#recipe_9b5945e03f05acbf9d69625138385408";
            alp=Food.convertUri(alper);
            String yazi = alp[0].displayFood(alp[0]);
            String path3 = "C:\\inetpub\\wwwroot\\Foodie\\deneme2.txt";
            File.WriteAllText(path3, yazi);
            Food.setMostLoved(alper);
        }
    }
}