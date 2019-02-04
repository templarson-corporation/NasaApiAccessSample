using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace NasaApiGateway
{
    [DataContract(Name = "Apod")]
    public class Apod
    {
        [DataMember]
        public string copyright;

        [DataMember]
        public string date;

        [DataMember]
        public string explanation;

        [DataMember]
        public string hdurl;

        [DataMember]
        public string media_type;

        [DataMember]
        public int Sharpless;

        [DataMember]
        public string url;
    }
}
