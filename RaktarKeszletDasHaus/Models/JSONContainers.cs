using System.Runtime.Serialization;

namespace RaktarKeszletDasHaus.Models
{


    [DataContract]
    [Serializable]
    public class TermekAdatok
    {
        public TermekAdatok()
        {
            CategoryColumn = string.Empty;
            CategoryBvinColumn = string.Empty;
            SKUColumn = string.Empty;
            ProductNameColumn = string.Empty;
            LocalInventoryColumn = 0;
            OnlineInventoryColumn = 0;
            OnlineInventoryBvinColumn = string.Empty;
            LocalInventoryColumnTmp = LocalInventoryColumn;
            OnlineInventoryColumnTmp = OnlineInventoryColumn;
            BvinColumn = string.Empty;
            ListPriceColumn = 0m;

        }
        [DataMember]
        public string BvinColumn { get; set; }
        [DataMember]
        public decimal ListPriceColumn { get; set; }
        [DataMember]
        public int OnlineInventoryColumn { get; set; }
        [DataMember]
        public int OnlineInventoryColumnTmp { get; set; }
        [DataMember]
        public string ProductNameColumn { get; set; }
        [DataMember]
        public string OnlineInventoryBvinColumn { get; set; }
        [DataMember]
        public int LocalInventoryColumn { get; set; }
        [DataMember]
        public int LocalInventoryColumnTmp { get; set; }
        [DataMember]
        public string SKUColumn { get; set; }
        [DataMember]
        public string CategoryColumn { get; set; }
        [DataMember]
        public string CategoryBvinColumn { get; set; }
    }

    [DataContract]
    [Serializable]
    internal class HCJSONProductsAll
    {

        public HCJSONProductsAll()
        {
            Errors = new List<string>();
            Content = new List<HCProduct>();
        }

        [DataMember]
        public List<string> Errors { get; set; }
        [DataMember]
        public List<HCProduct> Content { get; set; }

    }

    [DataContract]
    [Serializable]
    internal class HCJSONProductsOne
    {

        public HCJSONProductsOne()
        {
            Errors = new List<string>();
            Content = new HCProduct();
        }

        [DataMember]
        public List<string> Errors { get; set; }
        [DataMember]
        public HCProduct Content { get; set; }

    }

    [DataContract]
    [Serializable]
    internal class HCJSONInventories
    {

        public HCJSONInventories()
        {
            Errors = new List<string>();
            Content = new List<HCInventory>();
        }

        [DataMember]
        public List<string> Errors { get; set; }
        [DataMember]
        public List<HCInventory> Content { get; set; }

    }

    [DataContract]
    [Serializable]
    internal class HCJSONInventoriesOne
    {

        public HCJSONInventoriesOne()
        {
            Errors = new List<string>();
            Content = new HCInventory();
        }

        [DataMember]
        public List<string> Errors { get; set; }
        [DataMember]
        public HCInventory Content { get; set; }

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
        public List<string> Errors { get; set; }
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
