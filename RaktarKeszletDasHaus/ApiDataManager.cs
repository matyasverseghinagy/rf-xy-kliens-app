using RaktarKeszletDasHaus.Models;
using System;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Text.Json;
using System.Text;
using System.Windows.Forms;
using System.Buffers.Text;
using Microsoft.EntityFrameworkCore.Metadata;


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

        public async void PostUpdate()
        {
            string productBvin = "0dcf8471-e5d6-420a-88a4-ed3630fb6799";
            string allProductEndpoint = $"/DesktopModules/Hotcakes/API/rest/v1/products?key={HotCakesAPIKey}";
            
            HttpClient hotCakesClient = new HttpClient();
            hotCakesClient.BaseAddress = new Uri(BaseURL);
            hotCakesClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            List<HCProduct> tmpContainer = new List<HCProduct>();
            //string json = JsonSerializer.Serialize(requestData);
            //HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            //HttpResponseMessage response = await client.PostAsync(url, content);
            //string responseContent = await response.Content.ReadAsStringAsync();
            //MessageBox.Show(responseContent);
            try
            {
                // Get all the product
                var jsonResponse = hotCakesClient.GetFromJsonAsync<HCJSONProductsPagesFinal>(allProductEndpoint).Result;

                if (jsonResponse != null)
                {
                    var Content = jsonResponse.Content;
                    if (Content != null)
                    {
                        tmpContainer = Content.Products;
                    }
                }

                tmpContainer.Count();

                HCProduct tesztTermek = (from product in tmpContainer
                                         where product != null && product.Bvin.Equals(productBvin)
                                         select product).FirstOrDefault();
                if (tesztTermek != null)
                {
                    // Módosítom a teszttermék adatait.
                    tesztTermek.ProductName = "teszt-termék - 🌏🌏🌏";
                    tesztTermek.CreationDateUtc = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc).ToString();
                    tesztTermek.ImageFileSmall = "placeholder_correct.png";

                    string picture_name = "placeholder_correct_modified.png";

                    // POST HTTP request
                    string updateProductEndpoint = $"DesktopModules/Hotcakes/API/rest/v1/products/{tesztTermek.Bvin}?key={HotCakesAPIKey}";
                    string jsonToSend = JsonSerializer.Serialize(tesztTermek);
                    HttpContent data = new StringContent(jsonToSend, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await hotCakesClient.PostAsync(updateProductEndpoint, data);
                    string responseContent = await response.Content.ReadAsStringAsync();
                    MessageBox.Show(responseContent);

                    string uploadImageEndpoint = $"DesktopModules/Hotcakes/API/rest/v1/productimagesupload/{productBvin}/b90ec862-cb8d-456b-ba36-782e7cc151fa?key={HotCakesAPIKey}&filename={picture_name}";
                    byte[] image = File.ReadAllBytes("C:\\Users\\keres\\Documents\\university\\negyedik_szem\\it_rendszer\\others\\placeholder_correct_modified.png");
                    string medium_imgbase64json = JsonSerializer.Serialize(image);
                    //var imgToSend = new ByteArrayContent(medium_img);
                    //string imgAsJson = JsonSerializer.Serialize(medium_img);
                    
                    //            var ser = new JavaScriptSerializer { MaxJsonLength = MAXLENGTH };
                    //return ser.Serialize(target);

                    //Stream stream = new MemoryStream(medium_img);
                    data = new StringContent(medium_imgbase64json, Encoding.UTF8, "application/json");
                    response = await hotCakesClient.PostAsync(uploadImageEndpoint, data);
                    byte[] responseContentByte = await response.Content.ReadAsByteArrayAsync();
                    MessageBox.Show(responseContentByte.ToString());
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


        }

    }
}
