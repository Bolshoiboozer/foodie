using Foodie.Models;
using System;
using System.Collections.Generic;
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
            BackPropProgram bp = new BackPropProgram();
            double[] brain= bp.generateBrain();   
            bp.testFood(bp.learningFood(brain));*/
            String[] listIng= new String[3];
            listIng[0] = "chicken";
            listIng[1] = "oil";
            listIng[2] = "tomato";
            String yazi = "";
            Food myFood = Food.getFood(listIng);
                yazi += myFood.displayFood(myFood);

            deneme.InnerHtml = yazi;
        }
    }
}