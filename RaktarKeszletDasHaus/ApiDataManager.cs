using RaktarKeszletDasHaus.Models;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;


namespace RaktarKeszletDasHaus
{
    public class ApiDataManager
    {
        // Constansok és egyedi osztály változok
        private const string HotCakesAPIKey = "1-2957a058-ea39-4c24-8486-6db59d061085";
        private const string BaseURL = "http://20.234.113.211:8083";
        public static bool FAIL_STATUS = false;


        private HttpClient hotCakesClient;
        public List<HCAllCategory> Categories;
        private List<HCInventory> Inventories;
        public List<TermekAdatok> Products;
        public ApiDataManager()
        {
            // alapadatok megszerzése
            GetData();
        }


        public async Task PostInventoryUpdate(string? productBvin, string? invBvin, int localInvNew, int localInvOld, int onlineInvNew, int onlineInvOld)
        {
            // A HotCakes weboldalunk frissítése
            FAIL_STATUS = false;

            hotCakesClient = new HttpClient();
            hotCakesClient.BaseAddress = new Uri(BaseURL);
            hotCakesClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            string updateProductEndpoint = $"DesktopModules/Hotcakes/API/rest/v1/products/{productBvin}?key={HotCakesAPIKey}";
            string updateInventoryEndpoint = $"DesktopModules/Hotcakes/API/rest/v1/productinventory/{invBvin}?key={HotCakesAPIKey}";
            HCProduct productToUpdate = new HCProduct();
            HCInventory inventoryToUpdate = new HCInventory();

            try
            {
                // Ha van változás a korábbi bolti árúszámban és a módosított bolti árúszámban akkor egy POST metódussal frissítem
                if (localInvNew != localInvOld)
                {
                    // Lekérem az adott Bvin-nel rendelkező terméket
                    var jsonResponse = hotCakesClient.GetFromJsonAsync<HCJSONProductsOne>(updateProductEndpoint).Result;

                    if (jsonResponse != null)
                    {
                        productToUpdate = jsonResponse.Content;
                    }

                    // Módosítom a bolti készletet
                    productToUpdate.CreationDateUtc = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc).ToString();
                    productToUpdate.Tabs[0].HtmlData = $"<p>{localInvNew}</p>";

                    // POST metódus küldése & JSONná formázás
                    string jsonToSend = JsonSerializer.Serialize(productToUpdate);
                    HttpContent data = new StringContent(jsonToSend, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await hotCakesClient.PostAsync(updateProductEndpoint, data);
                    string responseContent = await response.Content.ReadAsStringAsync();
                    //MessageBox.Show(responseContent);
                }

                // Ha van változás a korábbi online árú száámban és az új online árű számban
                if (onlineInvNew != onlineInvOld)
                {
                    // Lekérem az adott Bvin-nel rendelkező készletrekordot
                    var jsonResponse = hotCakesClient.GetFromJsonAsync<HCJSONInventoriesOne>(updateInventoryEndpoint).Result;

                    if (jsonResponse != null)
                    {
                        inventoryToUpdate = jsonResponse.Content;
                    }

                    // Módosítom az online készletet
                    inventoryToUpdate.LastUpdated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc).ToString();
                    inventoryToUpdate.QuantityOnHand = onlineInvNew;

                    // POST metódus küldése & JSONná formázás
                    string jsonToSend = JsonSerializer.Serialize(inventoryToUpdate);
                    HttpContent data = new StringContent(jsonToSend, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await hotCakesClient.PostAsync(updateInventoryEndpoint, data);
                    string responseContent = await response.Content.ReadAsStringAsync();
                    //MessageBox.Show(responseContent);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                FAIL_STATUS = true;
            }

            if (!FAIL_STATUS)
            {
                MessageBox.Show("A mentés sikeres, készletek frissítve!", "Sikeres Mentés!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void GetData()
        {
            FAIL_STATUS = false;

            // HTTPCliens megnyitása
            hotCakesClient = new HttpClient();
            hotCakesClient.BaseAddress = new Uri(BaseURL);
            hotCakesClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Különböző adatkonténerek feltöltése a HotCakes-ből jövő adatokkal
            Categories = new List<HCAllCategory>();
            Products = new List<TermekAdatok>();
            Inventories = new List<HCInventory>();
            GetInventoryData();
            GetCategories();
            GetAllProducts();

            // HTTP Cliens Zárása
            hotCakesClient.Dispose();

            // Sikeres lefutás esetén visszajelzés a felhasználónak
            if (!FAIL_STATUS)
            {
                MessageBox.Show($"A legfrisebb adatokat használod!\nTermékek száma: {Products.Count}", "Adatfrissítés sikeres");
            }
        }

        public TermekAdatok GetOneProductData(TermekAdatok product)
        {
            HCInventory selectedInv = new HCInventory();
            HCProduct selectedProduct = new HCProduct();
            // Online raktár készlet adatok megszerzése -> eredmény: Lista ami HCInventory elemeket tartalmaz (minden termék online készlet adata)
            FAIL_STATUS = false;
            string oneInventoryEndpoint = $"/DesktopModules/Hotcakes/API/rest/v1/productinventory/{product.OnlineInventoryBvinColumn}?key={HotCakesAPIKey}";
            string oneProductEndpoint = $"DesktopModules/Hotcakes/API/rest/v1/products/{product.BvinColumn}?key={HotCakesAPIKey}";

            try
            {
                var jsonResponseInventory = hotCakesClient.GetFromJsonAsync<HCJSONInventoriesOne>(oneInventoryEndpoint).Result;
                if (jsonResponseInventory != null)
                {
                    var Content = jsonResponseInventory.Content;
                    if (Content != null)
                    {
                        selectedInv = Content;
                    }
                }
                var jsonResponseProduct = hotCakesClient.GetFromJsonAsync<HCJSONProductsOne>(oneProductEndpoint).Result;
                if (jsonResponseProduct != null)
                {
                    var Content = jsonResponseProduct.Content;
                    if (Content != null)
                    {
                        selectedProduct = Content;
                    }
                }

                // A Product tömb értékének frissítése
                int onlineInv = selectedInv.QuantityOnHand;
                int boltiInv = Convert.ToInt32(selectedProduct.Tabs[0].HtmlData.Substring(3, selectedProduct.Tabs[0].HtmlData.IndexOf("</p>") - 3));
                product.LocalInventoryColumn = boltiInv;
                product.LocalInventoryColumnTmp = boltiInv;
                product.OnlineInventoryColumn = onlineInv;
                product.OnlineInventoryColumnTmp = onlineInv;
                var item = Products.FirstOrDefault(r => r.BvinColumn == product.BvinColumn);
                item = product;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                FAIL_STATUS = true;
            }
            hotCakesClient.Dispose();
            return product;
        }

        private void GetInventoryData()
        {
            // Online raktár készlet adatok megszerzése -> eredmény: Lista ami HCInventory elemeket tartalmaz (minden termék online készlet adata)
            FAIL_STATUS = false;
            string allInventoryEndpoint = $"/DesktopModules/Hotcakes/API/rest/v1/productinventory?key={HotCakesAPIKey}";
            try
            {
                var jsonResponseInventory = hotCakesClient.GetFromJsonAsync<HCJSONInventories>(allInventoryEndpoint).Result;
                if (jsonResponseInventory != null)
                {
                    var Content = jsonResponseInventory.Content;
                    if (Content != null)
                    {
                        Inventories = Content;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                FAIL_STATUS = true;
            }
        }

        private void GetAllProducts()
        {
            // Kategóriánként termékek leszedése: a kategória tulajdonságot el lehet így menteni
            foreach (var category in Categories)
            {
                string currentCatBvin = category.Bvin;

                // Ha nem az összes kategória cím van kiválasztva
                if (category.Bvin != "all-cat-0")
                {
                    string productParametersFromCategoryEndpoint = $"DesktopModules/Hotcakes/API/rest/v1/products/?key={HotCakesAPIKey}&bycategory={currentCatBvin}&page=[1]&pagesize=[3000]";
                    // Termékek letöltése adott kategóriánként
                    try
                    {
                        var jsonResponse = hotCakesClient.GetFromJsonAsync<HCJSONProductsPagesFinal>(productParametersFromCategoryEndpoint).Result.Content;
                        if (jsonResponse != null)
                        {
                            foreach (var product in jsonResponse.Products)
                            {
                                // A nyers termék adatok egy letisztultabb "TermekAdatok" osztályba tétele
                                // az online készlet megkeresése és hozzáadása a termékhez
                                string invbVin = string.Empty;
                                int onlineNumtmp = 0;
                                foreach (var item in Inventories)
                                {
                                    if (product.Bvin.Equals(item.ProductBvin))
                                    {
                                        invbVin = item.Bvin;
                                        onlineNumtmp = item.QuantityOnHand;
                                        break;
                                    }
                                }

                                // A TermekAdatok osztály feltöltése
                                TermekAdatok tmpTermek = new TermekAdatok();
                                tmpTermek.SKUColumn = product.Sku;
                                tmpTermek.ProductNameColumn = product.ProductName;
                                tmpTermek.BvinColumn = product.Bvin;
                                tmpTermek.ListPriceColumn = product.ListPrice;
                                tmpTermek.OnlineInventoryColumn = onlineNumtmp;
                                tmpTermek.LocalInventoryColumn = Convert.ToInt32(product.Tabs[0].HtmlData.Substring(3, product.Tabs[0].HtmlData.IndexOf("</p>") - 3));
                                tmpTermek.LocalInventoryColumnTmp = tmpTermek.LocalInventoryColumn;
                                tmpTermek.OnlineInventoryColumnTmp = tmpTermek.OnlineInventoryColumn;
                                tmpTermek.CategoryColumn = category.Name;
                                tmpTermek.CategoryBvinColumn = category.Bvin;
                                tmpTermek.OnlineInventoryBvinColumn = invbVin;

                                // Az elkészült osztályt hozzáadom a TermékListához (ez fog megjelenni a Táblázatban alapszinten)
                                Products.Add(tmpTermek);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        FAIL_STATUS = true;
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void GetCategories()
        {
            // Kategóriák letöltése és összes kategória lehetőség hozzáadása a kategóriákhoz
            HCAllCategory allcategory = new HCAllCategory();
            allcategory.Bvin = "all-cat-0";
            allcategory.Name = "Összes kategória";
            Categories.Add(allcategory);

            string categoryEndpoint = $"/DesktopModules/Hotcakes/API/rest/v1/categories?key={HotCakesAPIKey}&page=[1]&pagesize=[3000]";

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
            }

            List<string> deletetebvins = new List<string>();

            // Kitörlöm a kategóriák listából azokat amik szülőként viselkednek (van gyerek kategóriájuk) -> nem lesz termék duplikáció
            for (int i = 0; i < Categories.Count(); i++)
            {
                //Trace.WriteLine("Termék: " + Categories[i].Name.ToString());
                if (Categories[i].ParentId == string.Empty && Categories[i].Bvin != "all-cat-0")
                {
                    //Trace.WriteLine("Termék: " + Categories[i].Name.ToString());
                    deletetebvins.Add(Categories[i].Bvin.ToString());
                }
            }

            foreach (var index in deletetebvins) 
            {
                for (int i = 0; i < Categories.Count(); i++)
                {
                    if (Categories[i].Bvin.Equals(index))
                    {
                        Categories.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        // Nem használt metódus - Bolti raktárkészlet lehetővé tétele hotcakes-en belül (csak egyszer kellett futtatni)
        private async void LocalInvInitDoNotCall()
        {
            FAIL_STATUS = false;
            string allProductEndpoint = $"/DesktopModules/Hotcakes/API/rest/v1/products?key={HotCakesAPIKey}";
            List<HCProduct> tmpContainer = new List<HCProduct>();
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
                Random rnd = new Random();
                foreach (HCProduct item in tmpContainer)
                {
                    HCProductDescriptionTab tmp = new HCProductDescriptionTab();
                    int veletlenszam = rnd.Next(4, 18);
                    tmp.TabTitle = "local_inventory";
                    tmp.HtmlData = $"<p>{veletlenszam}</p>";
                    item.Tabs.Add(tmp);
                    item.CreationDateUtc = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc).ToString();

                    // Upload the data
                    //// POST HTTP request
                    string updateProductEndpoint = $"DesktopModules/Hotcakes/API/rest/v1/products/{item.Bvin}?key={HotCakesAPIKey}";
                    string jsonToSend = JsonSerializer.Serialize(item);
                    HttpContent data = new StringContent(jsonToSend, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await hotCakesClient.PostAsync(updateProductEndpoint, data);
                    string responseContent = await response.Content.ReadAsStringAsync();
                    //MessageBox.Show(responseContent);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                FAIL_STATUS = true;

                //return;
            }

            hotCakesClient.Dispose();
            if (!FAIL_STATUS)
            {
                MessageBox.Show("Adatfrissítés jól sikerült!");
            }
        }
        // Online raktárkészlet frissítése HotCakesen belül -> csak egyszer kellett használni
        private async void OnlineInvInitDoNotCall()
        {
            FAIL_STATUS = false;
            string allProductEndpoint = $"/DesktopModules/Hotcakes/API/rest/v1/products?key={HotCakesAPIKey}";
            string allInventoryEndpoint = $"/DesktopModules/Hotcakes/API/rest/v1/productinventory?key={HotCakesAPIKey}";
            List<HCProduct> tmpContainer = new List<HCProduct>();
            List<HCInventory> tmpInventories = new List<HCInventory>();
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

                var jsonResponseInventory = hotCakesClient.GetFromJsonAsync<HCJSONInventories>(allInventoryEndpoint).Result;
                if (jsonResponseInventory != null)
                {
                    var Content = jsonResponseInventory.Content;
                    if (Content != null)
                    {
                        tmpInventories = Content;
                    }
                }

                //tmpInventories.Count();

                Random rnd = new Random();

                foreach (HCInventory inventory in tmpInventories)
                {
                    int veletlenszam = rnd.Next(76, 200);
                    HCInventory tmp = inventory;
                    tmp.OutOfStockPoint = 0;
                    tmp.LowStockPoint = 10;
                    tmp.QuantityOnHand = veletlenszam;
                    tmp.LastUpdated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc).ToString();

                    //// Upload the data
                    ////// POST HTTP request
                    string updateInventoryEndpoint = $"DesktopModules/Hotcakes/API/rest/v1/productinventory/{tmp.Bvin}?key={HotCakesAPIKey}";
                    string jsonToSend = JsonSerializer.Serialize(tmp);
                    HttpContent data = new StringContent(jsonToSend, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await hotCakesClient.PostAsync(updateInventoryEndpoint, data);
                    string responseContent = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                FAIL_STATUS = true;

                //return;
            }

            hotCakesClient.Dispose();
            if (!FAIL_STATUS)
            {
                MessageBox.Show("Adatfrissítés jól sikerült!");
            }
        }

    }
}
