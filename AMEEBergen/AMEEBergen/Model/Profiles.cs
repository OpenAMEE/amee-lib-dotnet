using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace BergenAmee.Model
{
    [DataContract]
    public class Profiles
    {
        [DataMember(EmitDefaultValue = false)]
        public String apiVersion { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Profile profile { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Status status { get; set; }
    }
}
