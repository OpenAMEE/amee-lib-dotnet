using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace BergenAmee.Model
{
    [DataContract]
    public class Environment
    {
        [DataMember(EmitDefaultValue = false)]
        public String uid { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String itemsPerFeed { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String description { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String name { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String owner { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String path { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String itemsPerPage { get; set; }
    }
}
