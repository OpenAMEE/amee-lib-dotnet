using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace BergenAmee.Model
{
    [DataContract]
    public class ItemDefinition
    {
        [DataMember(EmitDefaultValue = false)]
        public String uid { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String name { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String drillDown { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Environment environment { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String created { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String modified { get; set; }
    }
}
