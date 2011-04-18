using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace BergenAmee.Model
{
    [DataContract]
    public class ItemValue
    {

        [DataMember(EmitDefaultValue = false)]
        public ItemValueDefinition itemValueDefinition { get; set; }



        [DataMember(EmitDefaultValue = false)]
        public String uid { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String path { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String name { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String value { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String unit { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String perUnit { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String displayPath { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String displayName { get; set; }
    }
}
