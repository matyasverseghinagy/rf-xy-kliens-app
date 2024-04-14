using RaktarKeszletDasHaus.Models;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

//using Hotcakes.CommerceDTO.v1.Client;


namespace RaktarKeszletDasHaus
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
 


        }

        private void button1_Click(object sender, EventArgs e)
        {
            ListHotCakesCategoryAPI();
        }

        private void ListHotCakesCategoryAPI()
        {
            Trace.WriteLine("REST API call to hotCakes store ------------------------------------------");

            const string url = "http://20.234.113.211:8083";
            const string urlParameters = "/DesktopModules/Hotcakes/API/rest/v1/categories?key=1-2957a058-ea39-4c24-8486-6db59d061085";
            HttpClient hotCakesClient = new HttpClient();
            hotCakesClient.BaseAddress = new Uri(url);

            hotCakesClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = hotCakesClient.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            var responseJSON = hotCakesClient.GetFromJsonAsync<HCAllCategory>(urlParameters).Result;
            if (response.IsSuccessStatusCode)
            {
                Trace.WriteLine("Got it!");
                Trace.WriteLine("Status Code: " + response.StatusCode.ToString());
                Trace.WriteLine("Response: ");
                //Utf8JsonReader utf8JsonReader = new Utf8JsonReader(;
                
                //string result = hotCakesClient.GetFromJsonAsync response.Content.ReadAsStringAsync().Result;
                //Trace.WriteLine($"{result}");
                
            }
            else
            {
                //Trace.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                Trace.WriteLine("Error!");
            }

            // Make any other calls using HttpClient here.

            // Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            hotCakesClient.Dispose();
        }

    }
}
