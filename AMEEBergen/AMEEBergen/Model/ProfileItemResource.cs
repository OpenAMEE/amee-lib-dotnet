using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace BergenAmee.Model
{
    [DataContract]
    public class ProfileItemResource
    {
        [DataMember(EmitDefaultValue = false)]
        public ProfileItem[] profileItems { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Action actions { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Profile profile { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Status status { get; set; }



        [DataMember(EmitDefaultValue = false)]
        public String apiVersion { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String path { get; set; }
    }
}
