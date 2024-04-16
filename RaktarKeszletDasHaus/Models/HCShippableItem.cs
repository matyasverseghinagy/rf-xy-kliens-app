using System.Runtime.Serialization;

namespace RaktarKeszletDasHaus.Models
{
    [DataContract]
    public enum HCShippingMode
    {
        /// <summary>
        ///     This member should not be used.
        /// </summary>
        [EnumMember] None = 0,

        /// <summary>
        ///     If specified, this will have the product ship from the store address.
        /// </summary>
        [EnumMember] ShipFromSite = 1,

        /// <summary>
        ///     This member dictates that the product will ship from the vendors address.
        /// </summary>
        [EnumMember] ShipFromVendor = 2,

        /// <summary>
        ///     When specified, the product will ship from the manufacturers address.
        /// </summary>
        [EnumMember] ShipFromManufacturer = 3
    }

    /// <summary>
    ///     This is the primary class used for all shippable items in the REST API
    /// </summary>
    [DataContract]
    [Serializable]
    public class HCShippableItem
    {
        public HCShippableItem()
        {
            IsNonShipping = false;
            ExtraShipFee = 0m;
            Weight = 0m;
            Length = 0m;
            Width = 0m;
            Height = 0m;
            ShippingSource = HCShippingMode.ShipFromSite;
            ShippingSourceId = string.Empty;
            ShipSeparately = false;
        }

        /// <summary>
        ///     If true, the associated product will not be shipped and therefore should not have shipping logic applied.
        /// </summary>
        [DataMember]
        public bool IsNonShipping { get; set; }

        /// <summary>
        ///     If greater than zero, the specified fee should be added to the shipping fee presented to the customer.
        /// </summary>
        [DataMember]
        public decimal ExtraShipFee { get; set; }

        /// <summary>
        ///     The shippable weight of the product in pounds.
        /// </summary>
        [DataMember]
        public decimal Weight { get; set; }

        /// <summary>
        ///     The shippable length of the product in inches.
        /// </summary>
        [DataMember]
        public decimal Length { get; set; }

        /// <summary>
        ///     The shippable width of the product in inches.
        /// </summary>
        [DataMember]
        public decimal Width { get; set; }

        /// <summary>
        ///     The shippable height of the product in inches.
        /// </summary>
        [DataMember]
        public decimal Height { get; set; }

        /// <summary>
        ///     This defines where the product will be shipped from.
        /// </summary>
        [DataMember]
        public HCShippingMode ShippingSource { get; set; }

        /// <summary>
        ///     This ID value should match a vendor or manufacture when that respective ShippingSource is specified.
        /// </summary>
        [DataMember]
        public string ShippingSourceId { get; set; }

        /// <summary>
        ///     If true, the associated product cannot be shipped with other products.
        /// </summary>
        [DataMember]
        public bool ShipSeparately { get; set; }
    }
}
