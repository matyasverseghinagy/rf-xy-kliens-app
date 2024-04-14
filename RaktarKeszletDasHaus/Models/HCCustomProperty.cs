using System.Runtime.Serialization;

namespace RaktarKeszletDasHaus.Models
{
    internal class HCCustomProperty
    {
        public HCCustomProperty()
        {
            DeveloperId = string.Empty;
            Key = string.Empty;
            Value = string.Empty;
        }

        /// <summary>
        ///     This is a unique ID that you use to group your own properties together.
        /// </summary>
        /// <remarks>Use any value that you wish here as long as it is unique to your company and you use it consistently.</remarks>
        [DataMember]
        public string DeveloperId { get; set; }

        /// <summary>
        ///     This is the unique name that is used to store and retrieve the custom property.
        /// </summary>
        [DataMember]
        public string Key { get; set; }

        /// <summary>
        ///     This is the information that you are saving to later retrieve.
        /// </summary>
        /// <remarks>You are not limited in the number of characters that you can use.</remarks>
        [DataMember]
        public string Value { get; set; }
    }
}

