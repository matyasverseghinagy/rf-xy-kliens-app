using RaktarKeszletDasHaus.Models;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms;
using System.Runtime.Serialization;
using static System.Windows.Forms.DataFormats;
using System.Security.Permissions;



namespace RaktarKeszletDasHaus
{



    public partial class Form1 : Form
    {
        private TermekAdatok selectedTermek;
        private List<HCAllCategory> categories;
        private List<TermekAdatokDG> TermekekListaDataSource;
        private BindingSource DGBindigSource;
        private bool selectionAllowed = false;

        // DasHausColorPalette
        private Color DasHausBlue = Color.FromArgb(20, 33, 61);
        private Color DasHausYellow = Color.FromArgb(252, 163, 17);
        private Color DasHausGrey = Color.FromArgb(229, 229, 229);
        private Color DasHausBlack = Color.FromArgb(0, 0, 0);
        private Color DasHausWhite = Color.FromArgb(255, 255, 255);

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
            selectedTermek.Category = selectedBvin.Name;
            ListHotCakesProductsAPI(selectedBvin.Bvin);

            int dgw = dataGridView1.Width - 20 + 20;


            dataGridView1.Columns["CategoryColumn"].DisplayIndex = 0;
            dataGridView1.Columns["CategoryColumn"].HeaderText = "Kategória";
            dataGridView1.Columns["CategoryColumn"].Width = Convert.ToInt32(dgw * 0.17);
            dataGridView1.Columns["SKUColumn"].DisplayIndex = 1;
            dataGridView1.Columns["SKUColumn"].HeaderText = "SKU";
            dataGridView1.Columns["SKUColumn"].Width = Convert.ToInt32(dgw * 0.15);
            dataGridView1.Columns["ProductNameColumn"].DisplayIndex = 2;
            dataGridView1.Columns["ProductNameColumn"].HeaderText = "Terméknév";
            dataGridView1.Columns["ProductNameColumn"].Width = Convert.ToInt32(dgw * 0.48);
            dataGridView1.Columns["LocalInventoryColumn"].DisplayIndex = 3;
            dataGridView1.Columns["LocalInventoryColumn"].HeaderText = "Bolti készlet";
            dataGridView1.Columns["LocalInventoryColumn"].Width = Convert.ToInt32(dgw * 0.10);
            dataGridView1.Columns["OnlineInventoryColumn"].DisplayIndex = 4;
            dataGridView1.Columns["OnlineInventoryColumn"].HeaderText = "Online készlet";
            dataGridView1.Columns["OnlineInventoryColumn"].Width = Convert.ToInt32(dgw * 0.10);
            dataGridView1.Columns["BvinColumn"].Visible = false;
            dataGridView1.Columns["BvinColumn"].DisplayIndex = 5;
            dataGridView1.Columns["ListPriceColumn"].Visible = false;
            dataGridView1.Columns["ListPriceColumn"].DisplayIndex = 6;

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].ReadOnly = true;
                dataGridView1.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            selectionAllowed = true;

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

            //Adattábla Módosítása
            dataGridView1.BackgroundColor = DasHausWhite;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ScrollBars = ScrollBars.Vertical;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.ColumnHeadersHeight = 50;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = DasHausBlue;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = DasHausWhite;
            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = DasHausBlue;
            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionForeColor = DasHausWhite;
            dataGridView1.RowsDefaultCellStyle.SelectionForeColor = DasHausWhite;
            dataGridView1.RowsDefaultCellStyle.SelectionBackColor = DasHausYellow;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.GridColor = DasHausGrey;


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
                tmp.BvinColumn = products[i].Bvin;
                tmp.ListPriceColumn = products[i].ListPrice;

                TermekekListaDataSource.Add(tmp);
            }
            DGBindigSource.DataSource = TermekekListaDataSource;
            dataGridView1.DataSource = DGBindigSource;

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
            if (selectionAllowed)
            {
                DataGridViewRow tmp = (DataGridViewRow)dataGridView1.CurrentRow;
                Trace.WriteLine(tmp.Cells.Count);
                selectedTermek.Sku = tmp.Cells["SKUColumn"].Value.ToString();
                selectedTermek.Bvin = tmp.Cells["BvinColumn"].Value.ToString();
                selectedTermek.ListPrice = Convert.ToDecimal(tmp.Cells["ListPriceColumn"].Value.ToString());
                selectedTermek.ProductName = tmp.Cells["ProductNameColumn"].Value.ToString();
                termekNevL.Text = selectedTermek.ProductName;
                toolTip1.SetToolTip(termekNevL, termekNevL.Text);
                skuNevL.Text = selectedTermek.Sku;
                toolTip1.SetToolTip(skuNevL, skuNevL.Text);
                kategoriaNevL.Text = selectedTermek.Category;
                toolTip1.SetToolTip(kategoriaNevL, kategoriaNevL.Text);
                int tmpint = (int)selectedTermek.ListPrice;
                arNevL.Text = tmpint.ToString() + " Ft";
                toolTip1.SetToolTip(arNevL, arNevL.Text);
                bvinNevL.Text = selectedTermek.Bvin;
                toolTip1.SetToolTip(bvinNevL, bvinNevL.Text);
            }
        }
    }
}
