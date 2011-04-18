using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace BergenAmee.Model
{
    [DataContract]
    public class Amounts
    {
        [DataMember(EmitDefaultValue = false)]
        public Amount[] amount { get; set; }
    }
}
