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
