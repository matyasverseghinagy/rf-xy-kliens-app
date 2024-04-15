using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RaktarKeszletDasHaus.Models
{
    [DataContract]
    [Serializable]
    internal class TermekAdatok
    {
        public TermekAdatok()
        {
            Bvin = string.Empty;
            ProductName = string.Empty;
            ListPrice = 0m;
            Sku = string.Empty;
            Category = string.Empty;
        }
        [DataMember]
        public string Bvin { get; set; }
        [DataMember]
        public string ProductName { get; set; }
        [DataMember]
        public decimal ListPrice { get; set; }
        [DataMember]
        public string Sku { get; set; }
        [DataMember]
        public string Category { get; set; }
    }

    [DataContract]
    [Serializable]
    internal class TermekAdatokDG
    {
        public TermekAdatokDG()
        {
            CategoryColumn = string.Empty;
            SKUColumn = string.Empty;
            ProductNameColumn = string.Empty;
            LocalInventoryColumn = 0;
            OnlineInventoryColumn = 0;
            
        }
        [DataMember]
        public int OnlineInventoryColumn { get; set; }
        [DataMember]
        public string ProductNameColumn { get; set; }
        [DataMember]
        public int LocalInventoryColumn { get; set; }
        [DataMember]
        public string SKUColumn { get; set; }
        [DataMember]
        public string CategoryColumn { get; set; }
    }


    [DataContract]
    [Serializable]
    internal class HCJSONProductsPagesFinal
    {

        public HCJSONProductsPagesFinal()
        {
            Errors = new List<string>();
            Content = new HCJSONProductsPage();
        }

        [DataMember]
        public List<String> Errors { get; set; }
        [DataMember]
        public HCJSONProductsPage Content { get; set; }

    }

    [DataContract]
    [Serializable]
    internal class HCJSONProductsPage
    {
        public HCJSONProductsPage()
        {
            Products = new List<HCProduct>();
            TotalProductCount = 0;
        }

        [DataMember]
        public List<HCProduct> Products { get; set; }

        [DataMember]
        public int TotalProductCount { get; set; }
    }

    [DataContract]
    [Serializable]
    internal class HCJSONCategories
    {

        public HCJSONCategories()
        {
            Errors = new List<string>();
            Content = new List<HCAllCategory>();
        }

        [DataMember]
        public List<String> Errors { get; set; }
        [DataMember]
        public List<HCAllCategory> Content { get; set; }

    }

}
