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
    internal class HCApiOperation
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

