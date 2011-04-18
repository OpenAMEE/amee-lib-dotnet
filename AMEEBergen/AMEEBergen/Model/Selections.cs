using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace BergenAmee.Model
{
    [DataContract]
    public class Selections
    {
        [DataMember(EmitDefaultValue = false)]
        public String name { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String value { get; set; }
    }
}
