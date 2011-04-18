using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace BergenAmee.Model
{
    [DataContract]
    public class Choices
    {
        [DataMember(EmitDefaultValue = false)]
        public String name { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String value { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Choices[] choices { get; set; }
    }
}
