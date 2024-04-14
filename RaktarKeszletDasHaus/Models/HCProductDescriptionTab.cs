using System.Runtime.Serialization;

namespace RaktarKeszletDasHaus.Models
{
    /// <summary>
    ///     This object holds a single instance of an "Info Tab" that customers use to list product specs and other details.
    /// </summary>
    [DataContract]
    [Serializable]
    internal class HCProductDescriptionTab
    {

        public HCProductDescriptionTab()
        {
            Bvin = string.Empty;
            TabTitle = string.Empty;
            HtmlData = string.Empty;
            SortOrder = 0;
        }

        /// <summary>
        ///     The unique ID &amp; primary key of the info tab.
        /// </summary>
        [DataMember]
        public string Bvin { get; set; }

        /// <summary>
        ///     The localized title as you want a customer to see it.
        /// </summary>
        [DataMember]
        public string TabTitle { get; set; }

        /// <summary>
        ///     This is the content of the info tab that is shown to customers.
        /// </summary>
        [DataMember]
        public string HtmlData { get; set; }

        /// <summary>
        ///     Sorting is supported by giving each info tab a sequential number.
        /// </summary>
        [DataMember]
        public int SortOrder { get; set; }
    }
}
