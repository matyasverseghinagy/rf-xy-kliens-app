using RaktarKeszletDasHaus.Models;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http.Json;



namespace RaktarKeszletDasHaus
{



    public partial class Form1 : Form
    {
        private HCAllCategory selectedCategory;
        private TermekAdatok selectedTermek;
        private string selectedTermekName = string.Empty;
        private string selectedTermekSKU = string.Empty;
        private List<HCAllCategory> categories;
        private List<TermekAdatok> TermekekListaDataSource;
        private List<TermekAdatok> OriginalTermekLista;
        private BindingSource DGBindigSource;
        private bool selectionAllowed = false;
        private ApiDataManager apiDataManager;

        // DasHausColorPalette
        private Color DasHausBlue = Color.FromArgb(20, 33, 61);
        private Color DasHausYellow = Color.FromArgb(252, 163, 17);
        private Color DasHausGrey = Color.FromArgb(229, 229, 229);
        private Color DasHausBlack = Color.FromArgb(0, 0, 0);
        private Color DasHausWhite = Color.FromArgb(255, 255, 255);

        public Form1()
        {
            InitializeComponent();

            // Defining data containers
            apiDataManager = new ApiDataManager();
            TermekekListaDataSource = new List<TermekAdatok>();
            OriginalTermekLista = new List<TermekAdatok>();
            categories = new List<HCAllCategory>();
            selectedCategory = new HCAllCategory();
            selectedTermek = new TermekAdatok();

            // Initiating the data
            this.categories = apiDataManager.Categories;
            this.OriginalTermekLista = apiDataManager.Products.ToList();
            this.TermekekListaDataSource = OriginalTermekLista.ToList();

            // Setting up the category combobox
            comboBox1.DataSource = categories;
            comboBox1.ValueMember = "Bvin";
            comboBox1.DisplayMember = "Name";
            comboBox1.SelectedIndex = 0;

            // Setting up the bindingsource of the dgv
            DGBindigSource = new BindingSource();
            DGBindigSource.DataSource = TermekekListaDataSource;
            dataGridView1.DataSource = DGBindigSource;

            // Setting up the layout of the datagridview table
            SetDataGridView();


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

            saveButton.BackColor = DasHausWhite;
            saveButton.ForeColor = DasHausBlack;
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FilterResults();
        }

        private void panel4_MouseClick(object sender, MouseEventArgs e)
        {
            // Data refresh
            apiDataManager.GetData();

            this.categories = apiDataManager.Categories.ToList();
            this.OriginalTermekLista = apiDataManager.Products.ToList();
            this.TermekekListaDataSource = OriginalTermekLista.ToList();

            comboBox1.DataSource = categories;
            comboBox1.ValueMember = "Bvin";
            comboBox1.DisplayMember = "Name";
            comboBox1.SelectedIndex = 0;

            DGBindigSource.DataSource = TermekekListaDataSource;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            // Update the product information
            if (selectionAllowed && (DataGridViewRow)dataGridView1.CurrentRow != null)
            {
                DataGridViewRow tmp = (DataGridViewRow)dataGridView1.CurrentRow;
                selectedTermek.SKUColumn = tmp.Cells["SKUColumn"].Value.ToString();
                selectedTermek.BvinColumn = tmp.Cells["BvinColumn"].Value.ToString();
                selectedTermek.ListPriceColumn = Convert.ToDecimal(tmp.Cells["ListPriceColumn"].Value.ToString());
                selectedTermek.ProductNameColumn = tmp.Cells["ProductNameColumn"].Value.ToString();
                selectedTermek.CategoryColumn = tmp.Cells["CategoryColumn"].Value.ToString();
                termekNevL.Text = selectedTermek.ProductNameColumn;
                toolTip1.SetToolTip(termekNevL, termekNevL.Text);
                skuNevL.Text = selectedTermek.SKUColumn;
                toolTip1.SetToolTip(skuNevL, skuNevL.Text);
                kategoriaNevL.Text = selectedTermek.CategoryColumn;
                toolTip1.SetToolTip(kategoriaNevL, kategoriaNevL.Text);
                int tmpint = (int)selectedTermek.ListPriceColumn;
                arNevL.Text = tmpint.ToString() + " Ft";
                toolTip1.SetToolTip(arNevL, arNevL.Text);
                bvinNevL.Text = selectedTermek.BvinColumn;
                toolTip1.SetToolTip(bvinNevL, bvinNevL.Text);

                // Setting the value to the local inventory modifier textbox
                textBox3.Text = tmp.Cells["LocalInventoryColumnTmp"].Value.ToString();


                // Setting the value to the online inventory modifier textbox
                textBox4.Text = tmp.Cells["OnlineInventoryColumnTmp"].Value.ToString();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            FilterResults();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            FilterResults();
        }

        private void clear_productname_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            FilterResults();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            FilterResults();
        }

        private void localInvAdd_Click(object sender, EventArgs e)
        {
            int tmp = Convert.ToInt32(textBox3.Text.ToString());
            tmp += 1;
            textBox3.Text = tmp.ToString();
        }

        private void localInvSub_Click(object sender, EventArgs e)
        {
            int tmp = Convert.ToInt32(textBox3.Text.ToString());
            if (tmp > 0)
            {
                tmp -= 1;
            }
            textBox3.Text = tmp.ToString();
        }

        private void onlineInvAdd_Click(object sender, EventArgs e)
        {
            int tmp = Convert.ToInt32(textBox4.Text.ToString());
            tmp += 1;
            textBox4.Text = tmp.ToString();
        }

        private void onlineInvSub_Click(object sender, EventArgs e)
        {
            int tmp = Convert.ToInt32(textBox4.Text.ToString());
            if (tmp > 0)
            {
                tmp -= 1;
            }
            textBox4.Text = tmp.ToString();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {

            apiDataManager.PostUpdate();
        }

        private void FilterResults()
        {
            selectedCategory = (HCAllCategory)comboBox1.SelectedItem;
            selectedTermekName = textBox1.Text;
            selectedTermekSKU = textBox2.Text;

            // 1. Minden_textbox_üres
            if (selectedTermekName.Equals(string.Empty) && selectedTermekSKU.Equals(string.Empty))
            {
                // Filter for all categories
                if (selectedCategory.Bvin == "all-cat-0")
                {
                    var searchResult = (from products in OriginalTermekLista
                                        select products).ToList();
                    TermekekListaDataSource = searchResult;
                }
                else // only for a specified category
                {
                    var searchResult = (from products in OriginalTermekLista
                                        where products.CategoryColumn.Equals(selectedCategory.Name)
                                        select products).ToList();
                    TermekekListaDataSource = searchResult;
                }
            }
            // 2. A termeknev nem üres, skunév üres
            else if (!selectedTermekName.Equals(string.Empty) && selectedTermekSKU.Equals(string.Empty))
            {
                // Filter for all categories
                if (selectedCategory.Bvin == "all-cat-0")
                {
                    var searchResult = (from products in OriginalTermekLista
                                        where products.ProductNameColumn.ToLower().Contains(selectedTermekName.ToLower())
                                        select products).ToList();
                    TermekekListaDataSource = searchResult;
                }
                else // only for a specified category
                {
                    var searchResult = (from products in OriginalTermekLista
                                        where products.CategoryColumn.Equals(selectedCategory.Name) && products.ProductNameColumn.ToLower().Contains(selectedTermekName.ToLower())
                                        select products).ToList();
                    TermekekListaDataSource = searchResult;
                }
            }
            // 3. A termeknev üres, skunév nem üres
            else if (selectedTermekName.Equals(string.Empty) && !selectedTermekSKU.Equals(string.Empty))
            {
                // Filter for all categories
                if (selectedCategory.Bvin == "all-cat-0")
                {
                    var searchResult = (from products in OriginalTermekLista
                                        where products.SKUColumn.ToLower().StartsWith(selectedTermekSKU.ToLower())
                                        select products).ToList();
                    TermekekListaDataSource = searchResult;
                }
                else // only for a specified category
                {
                    var searchResult = (from products in OriginalTermekLista
                                        where products.CategoryColumn.Equals(selectedCategory.Name) && products.SKUColumn.ToLower().StartsWith(selectedTermekSKU.ToLower())
                                        select products).ToList();
                    TermekekListaDataSource = searchResult;
                }
            }
            // 4. A termeknev nem üres, skunév nem üres
            else if (!selectedTermekName.Equals(string.Empty) && !selectedTermekSKU.Equals(string.Empty))
            {
                // Filter for all categories
                if (selectedCategory.Bvin == "all-cat-0")
                {
                    var searchResult = (from products in OriginalTermekLista
                                        where products.SKUColumn.ToLower().StartsWith(selectedTermekSKU.ToLower()) && products.ProductNameColumn.ToLower().Contains(selectedTermekName.ToLower())
                                        select products).ToList();
                    TermekekListaDataSource = searchResult;
                }
                else // only for a specified category
                {
                    var searchResult = (from products in OriginalTermekLista
                                        where products.CategoryColumn.Equals(selectedCategory.Name) && products.SKUColumn.ToLower().StartsWith(selectedTermekSKU.ToLower()) && products.ProductNameColumn.ToLower().Contains(selectedTermekName.ToLower())
                                        select products).ToList();
                    TermekekListaDataSource = searchResult;
                }
            }
            DGBindigSource.DataSource = TermekekListaDataSource;
        }

        private void SetDataGridView()
        {

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
            dataGridView1.Columns["LocalInventoryColumnTmp"].Visible = false;
            dataGridView1.Columns["LocalInventoryColumnTmp"].DisplayIndex = 7;
            dataGridView1.Columns["OnlineInventoryColumnTmp"].Visible = false;
            dataGridView1.Columns["OnlineInventoryColumnTmp"].DisplayIndex = 8;
            dataGridView1.Columns["CategoryBvinColumn"].Visible = false;
            dataGridView1.Columns["CategoryBvinColumn"].DisplayIndex = 8;

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].ReadOnly = true;
                dataGridView1.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            selectionAllowed = true;
        }

    }
}
