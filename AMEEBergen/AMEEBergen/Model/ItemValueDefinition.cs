using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace BergenAmee.Model
{
    [DataContract]
    public class ItemValueDefinition
    {
        [DataMember(EmitDefaultValue = false)]
        public ValueDefinition valueDefinition { get; set; }




        [DataMember(EmitDefaultValue = false)]
        public String uid { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String name { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String path { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String choices { get; set; }
    }
}
