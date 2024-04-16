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
        private List<HCAllCategory> categories;
        private List<TermekAdatok> TermekekListaDataSource;
        private List<TermekAdatok> OriginalTermekLista;
        private BindingSource DGBindigSource;
        private bool selectionAllowed = false;
        private ApiDataManager apiDataManager { get; }

        // DasHausColorPalette
        private Color DasHausBlue = Color.FromArgb(20, 33, 61);
        private Color DasHausYellow = Color.FromArgb(252, 163, 17);
        private Color DasHausGrey = Color.FromArgb(229, 229, 229);
        private Color DasHausBlack = Color.FromArgb(0, 0, 0);
        private Color DasHausWhite = Color.FromArgb(255, 255, 255);

        public Form1()
        {
            InitializeComponent();


            ApiDataManager apiDataManager = new ApiDataManager();
            if (ApiDataManager.EXIT_STATUS == true)
            {
                Application.Exit();
            }
            TermekekListaDataSource = new List<TermekAdatok>();
            OriginalTermekLista = new List<TermekAdatok>();
            categories = new List<HCAllCategory>();
            selectedCategory = new HCAllCategory();
            selectedTermek = new TermekAdatok();

            this.categories = apiDataManager.Categories;
            this.OriginalTermekLista = apiDataManager.Products.ToList();
            this.TermekekListaDataSource = OriginalTermekLista.ToList();

            comboBox1.DataSource = categories;
            comboBox1.ValueMember = "Bvin";
            comboBox1.DisplayMember = "Name";
            comboBox1.SelectedIndex = 0;

            DGBindigSource = new BindingSource();
            DGBindigSource.DataSource = TermekekListaDataSource;
            dataGridView1.DataSource = DGBindigSource;


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



        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            selectedCategory = (HCAllCategory)comboBox1.SelectedItem;

            // Filter for all categories
            if (selectedCategory.Bvin == "all-cat-0")
            {
                TermekekListaDataSource = OriginalTermekLista.ToList();
            }
            else // only for a specified category
            {
                var searchResult = (from products in OriginalTermekLista
                                    where products.CategoryColumn.Equals(selectedCategory.Name)
                                    select products).ToList();
                TermekekListaDataSource = searchResult;
            }
            DGBindigSource.DataSource = TermekekListaDataSource;
        }

        private void panel4_MouseClick(object sender, MouseEventArgs e)
        {
            //ListHotCakesCategoryAPI();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (selectionAllowed)
            {
                DataGridViewRow tmp = (DataGridViewRow)dataGridView1.CurrentRow;
                Trace.WriteLine(tmp.Cells.Count);
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
            }
        }
    }
}
