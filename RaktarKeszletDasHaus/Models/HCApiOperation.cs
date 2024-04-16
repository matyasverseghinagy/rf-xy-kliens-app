using System.Runtime.Serialization;

namespace RaktarKeszletDasHaus.Models
{
    [DataContract]
    [Serializable]
    public class HCApiOperation
    {
        public HCApiOperation()
        {
            Uri = string.Empty;
            Rel = string.Empty;
            Description = string.Empty;
        }

        [DataMember]
        public string Uri { get; set; }

        [DataMember]
        public string Rel { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}

