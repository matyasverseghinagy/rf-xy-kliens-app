using RaktarKeszletDasHaus.Models;
using System;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Windows.Forms;


namespace RaktarKeszletDasHaus
{
    public class ApiDataManager
    {
        private const string HotCakesAPIKey = "1-2957a058-ea39-4c24-8486-6db59d061085";
        private const string BaseURL = "http://20.234.113.211:8083";
        public List<HCAllCategory> Categories;
        public List<TermekAdatok> Products;
        public static bool FAIL_STATUS = false;
        public ApiDataManager()
        {
            
            GetData();
        }

        public void GetData()
        {
            FAIL_STATUS = false;
            // Getting the category list from HotCakes

            Categories = new List<HCAllCategory>();
            Products = new List<TermekAdatok>();
            GetCategories();
            GetAllProducts();

            
        }

        private void GetAllProducts()
        {
            HttpClient hotCakesClient = new HttpClient();
            hotCakesClient.BaseAddress = new Uri(BaseURL);
            hotCakesClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            foreach (var category in Categories)
            {
                string currentCatBvin = category.Bvin;
                
                // If we select a specific category (not the "Összes kategória")
                if (category.Bvin != "all-cat-0") 
                {
                    string productParametersFromCategoryEndpoint = $"DesktopModules/Hotcakes/API/rest/v1/products/?key={HotCakesAPIKey}&bycategory={currentCatBvin}&page=[1]&pagesize=[100]";

                    try
                    {
                        var jsonResponse = hotCakesClient.GetFromJsonAsync<HCJSONProductsPagesFinal>(productParametersFromCategoryEndpoint).Result.Content;
                        if (jsonResponse != null)
                        {
                            foreach (var product in jsonResponse.Products)
                            {
                                TermekAdatok tmpTermek = new TermekAdatok();
                                tmpTermek.SKUColumn = product.Sku;
                                tmpTermek.ProductNameColumn = product.ProductName;
                                tmpTermek.BvinColumn = product.Bvin;
                                tmpTermek.ListPriceColumn = product.ListPrice;
                                tmpTermek.OnlineInventoryColumn = 0;
                                tmpTermek.LocalInventoryColumn = 0;
                                tmpTermek.LocalInventoryColumnTmp = tmpTermek.LocalInventoryColumn;
                                tmpTermek.OnlineInventoryColumnTmp = tmpTermek.OnlineInventoryColumn;
                                tmpTermek.CategoryColumn = category.Name;
                                tmpTermek.CategoryBvinColumn = category.Bvin;

                                Products.Add(tmpTermek);
                            }
                        }            
                    }
                    catch (Exception ex)
                    {
                        FAIL_STATUS = true;
                        MessageBox.Show(ex.Message);
                        hotCakesClient.Dispose();
                        return;
                    }
                }
            }
            if (!FAIL_STATUS)
            {
                MessageBox.Show("A legfrisebb adatokat használod!", "Adatfrissítés sikeres");
            }
            hotCakesClient.Dispose();
        }

        private void GetCategories()
        {
            // Adding all category to the category list
            HCAllCategory allcategory = new HCAllCategory();
            allcategory.Bvin = "all-cat-0";
            allcategory.Name = "Összes kategória";
            Categories.Add(allcategory);


            string categoryEndpoint = $"/DesktopModules/Hotcakes/API/rest/v1/categories?key={HotCakesAPIKey}";
            HttpClient hotCakesClient = new HttpClient();
            hotCakesClient.BaseAddress = new Uri(BaseURL);

            hotCakesClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var jsonResponse = hotCakesClient.GetFromJsonAsync<HCJSONCategories>(categoryEndpoint).Result;
                if (jsonResponse != null)
                {
                    foreach (var category in jsonResponse.Content)
                    {
                        Categories.Add(category);
                    }
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                FAIL_STATUS = true;
                hotCakesClient.Dispose();
                return;
            }

            hotCakesClient.Dispose();

            // Manage the category List content -> Delete the parent categories
            for (int i = 0; i < Categories.Count(); i++)
            {
                if (Categories[i].Name.Equals("Háztartási nagygépek") || Categories[i].Name.Equals("Háztartási kisgépek") || Categories[i].Name.Equals("Szórakoztatás"))
                {
                    Categories.RemoveAt(i);
                }
            }
        }
    }
}
