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
            string html = string.Empty;
            string url = @"https://api.edamam.com/search?q=chicken&app_id=d378a03e&app_key=84ee37aef5d7110f94d119e922ae96f2&from=0&to=3&calories=591-722&health=alcohol-free";

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
            deneme.InnerHtml = data.hits[0].recipe.ingredients[1].text + "alp";
        }
    }
}