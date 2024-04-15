using RaktarKeszletDasHaus.Models;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms;
using System.Runtime.Serialization;
using static System.Windows.Forms.DataFormats;

//using Hotcakes.CommerceDTO.v1.Client;


namespace RaktarKeszletDasHaus
{



    public partial class Form1 : Form
    {
        private TermekAdatok selectedTermek;
        private List<HCAllCategory> categories;
        private List<TermekAdatokDG> TermekekListaDataSource;
        private BindingSource DGBindigSource;

        //BindingSource productsList;
        //List<HCProduct> products;

        public Form1()
        {
            InitializeComponent();



            ListHotCakesCategoryAPI();
            for (int i = 0; i < categories.Count(); i++)
            {
                //Trace.WriteLine(categories[i].ParentId);
                if (categories[i].ParentId == String.Empty)
                {
                    categories.RemoveAt(i);
                }
            }


            comboBox1.DataSource = categories;
            comboBox1.ValueMember = "Bvin";
            comboBox1.DisplayMember = "Name";
            comboBox1.SelectedIndex = 0;

            selectedTermek = new TermekAdatok();

            DGBindigSource = new BindingSource();
            DGBindigSource.DataSource = TermekekListaDataSource;

            HCAllCategory selectedBvin = (HCAllCategory)comboBox1.SelectedItem;
            //Trace.Write(selectedbvin.Bvin + "\n");
            selectedTermek.Category = selectedBvin.Name;
            ListHotCakesProductsAPI(selectedBvin.Bvin);

            //dataGridView1.Enabled = false;
            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;




        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Define the border style of the form to a dialog box.
            FormBorderStyle = FormBorderStyle.FixedDialog;

            // Set the MaximizeBox to false to remove the maximize box.
            MaximizeBox = false;

            // Set the MinimizeBox to false to remove the minimize box.
            MinimizeBox = false;

            // Set the start position of the form to the center of the screen.
            StartPosition = FormStartPosition.CenterScreen;
            //productsList = new BindingSource();

            // Color settings
            Color DasHausBlue = Color.FromArgb(20, 33, 61);
            Color DasHausYellow = Color.FromArgb(252, 163, 17);

            //ListHotCakesCategoryAPI();

        }

        private void ListHotCakesProductsAPI(string Bvin)
        {
            Trace.WriteLine("REST API call to hotCakes store ------------------------------------------");
            const string url = "http://20.234.113.211:8083";
            string urlParameters = "DesktopModules/Hotcakes/API/rest/v1/products/?key=1-2957a058-ea39-4c24-8486-6db59d061085&bycategory=" + Bvin + "&page=[1]&pagesize=[100]";
            HttpClient hotCakesClient = new HttpClient();
            hotCakesClient.BaseAddress = new Uri(url);
            hotCakesClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            HCJSONProductsPage productpage = hotCakesClient.GetFromJsonAsync<HCJSONProductsPagesFinal>(urlParameters).Result.Content;
            int ProductNum = productpage.TotalProductCount;
            List<HCProduct> products = productpage.Products;



            hotCakesClient.Dispose();
            TermekekListaDataSource = new List<TermekAdatokDG>(products.Count());
            for (int i = 0; i < products.Count; i++)
            {
                TermekAdatokDG tmp = new TermekAdatokDG();

                tmp.CategoryColumn = selectedTermek.Category;
                tmp.SKUColumn = products[i].Sku;
                tmp.ProductNameColumn = products[i].ProductName;
                tmp.LocalInventoryColumn = 0;
                tmp.OnlineInventoryColumn = 0;

                TermekekListaDataSource.Add(tmp);
            }
            DGBindigSource.DataSource = TermekekListaDataSource;
            dataGridView1.DataSource = DGBindigSource;

            //listBox2.DataSource = new List<string>();
            //listBox2.DataSource = products;
            //listBox2.ValueMember = "Bvin";
            //listBox2.DisplayMember = "ProductName";
            //listBox2.SelectedIndex = 0;

            //HCProduct tmp = (HCProduct)listBox2.SelectedItem;
            //selectedTermek.Sku = tmp.Sku;
            //selectedTermek.Bvin = tmp.Bvin;
            //selectedTermek.ListPrice = tmp.ListPrice;
            //selectedTermek.ProductName = tmp.ProductName;



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
            //HttpResponseMessage response = hotCakesClient.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            categories = hotCakesClient.GetFromJsonAsync<HCJSONCategories>(urlParameters).Result.Content;
            //if (response.IsSuccessStatusCode)
            //{
            //    Trace.WriteLine("Got it!");
            //    Trace.WriteLine("Status Code: " + response.StatusCode.ToString());
            //    Trace.WriteLine("Response: ");
            //    //Utf8JsonReader utf8JsonReader = new Utf8JsonReader(;

            //    foreach (var categoryitem in categories)
            //    {
            //        Trace.WriteLine(categoryitem.Name);
            //    }

            //    //string result = hotCakesClient.GetFromJsonAsync response.Content.ReadAsStringAsync().Result;
            //    //Trace.WriteLine($"{result}");

            //}
            //else
            //{
            //    //Trace.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            //    Trace.WriteLine("Error!");
            //}

            // Make any other calls using HttpClient here.

            // Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            hotCakesClient.Dispose();
            //HCAllCategory selectedbvin = (HCAllCategory)listBox1.SelectedItem;
            //ListHotCakesProductsAPI(selectedbvin.Bvin);



        }




        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            HCProduct tmp = (HCProduct)listBox2.SelectedItem;
            selectedTermek.Sku = tmp.Sku;
            selectedTermek.Bvin = tmp.Bvin;
            selectedTermek.ListPrice = tmp.ListPrice;
            selectedTermek.ProductName = tmp.ProductName;
            termekNevL.Text = selectedTermek.ProductName;
            skuNevL.Text = selectedTermek.Sku;
            kategoriaNevL.Text = selectedTermek.Category;
            int tmpint = (int)selectedTermek.ListPrice;
            arNevL.Text = tmpint.ToString() + " Ft";
            bvinNevL.Text = selectedTermek.Bvin;
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            HCAllCategory selectedBvin = (HCAllCategory)comboBox1.SelectedItem;
            //Trace.Write(selectedbvin.Bvin + "\n");
            selectedTermek.Category = selectedBvin.Name;
            ListHotCakesProductsAPI(selectedBvin.Bvin);
        }

        private void panel4_MouseClick(object sender, MouseEventArgs e)
        {
            ListHotCakesCategoryAPI();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //HCProduct tmp = (HCProduct)listBox2.SelectedItem;
            //selectedTermek.Sku = tmp.Sku;
            //selectedTermek.Bvin = tmp.Bvin;
            //selectedTermek.ListPrice = tmp.ListPrice;
            //selectedTermek.ProductName = tmp.ProductName;
            //termekNevL.Text = selectedTermek.ProductName;
            //skuNevL.Text = selectedTermek.Sku;
            //kategoriaNevL.Text = selectedTermek.Category;
            //int tmpint = (int)selectedTermek.ListPrice;
            //arNevL.Text = tmpint.ToString() + " Ft";
            //bvinNevL.Text = selectedTermek.Bvin;
        }
    }
}
