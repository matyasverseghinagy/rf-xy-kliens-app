using System.Runtime.Serialization;

namespace RaktarKeszletDasHaus.Models
{
    [DataContract]
    [Serializable]
    public class HCInventory
    {

        public HCInventory()
        {
            Bvin = string.Empty;
            LastUpdated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc).ToString();
            ProductBvin = string.Empty;
            VariantId = string.Empty;
            QuantityOnHand = 0;
            QuantityReserved = 0;
            LowStockPoint = 0;
            OutOfStockPoint = 0;
        }

        /// <summary>
        ///     This is the unique ID or primary key of the product inventory record.
        /// </summary>
        [DataMember]
        public string Bvin { get; set; }

        /// <summary>
        ///     The last updated date is used for auditing purposes to know when the product inventory was last updated.
        /// </summary>
        [DataMember]
        public string LastUpdated { get; set; }

        /// <summary>
        ///     The unique ID or Bvin of the product that this inventory relates to.
        /// </summary>
        [DataMember]
        public string ProductBvin { get; set; }

        /// <summary>
        ///     When populated, the variant ID specifies that this record relates to a specific variant of the product.
        /// </summary>
        [DataMember]
        public string VariantId { get; set; }

        /// <summary>
        ///     The total physical count of items on hand.
        /// </summary>
        [DataMember]
        public int QuantityOnHand { get; set; }

        /// <summary>
        ///     Count of items in stock but reserved for carts or orders.
        /// </summary>
        [DataMember]
        public int QuantityReserved { get; set; }

        /// <summary>
        ///     Determines when a product has hit a point to where it is considered to be low on stock.
        /// </summary>
        [DataMember]
        public int LowStockPoint { get; set; }

        /// <summary>
        ///     The value that signifies that the the product should be considered out of stock.
        /// </summary>
        [DataMember]
        public int OutOfStockPoint { get; set; }
    }
}

