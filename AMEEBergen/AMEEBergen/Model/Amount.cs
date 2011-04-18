using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace BergenAmee.Model
{
    [DataContract]
    public class Amount
    {
        [DataMember(EmitDefaultValue = false)]
        public Double value { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String unit { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String type { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String perUnit { get; set; }
    }
}
