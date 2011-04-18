using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace BergenAmee.Model
{
    [DataContract]
    public class Status
    {
        [DataMember(EmitDefaultValue = false)]
        public String description { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String name { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int code { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String uri { get; set; }
    }
}
