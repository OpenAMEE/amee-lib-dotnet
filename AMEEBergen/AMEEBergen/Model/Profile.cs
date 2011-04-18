using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace BergenAmee.Model
{
    [DataContract]
    public class Profile
    {
        [DataMember(EmitDefaultValue = false)]
        public String uid { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Environment environment { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String created { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String modified { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String name { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String path { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public User user { get; set; }
    }
}
