using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace BergenAmee.Model
{
    [DataContract]
    public class ValueDefinition
    {
        [DataMember(EmitDefaultValue = false)]
        public String uid { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Environment env { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String name { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String creation { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String description { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String valueType { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String modified { get; set; }
    }
}
