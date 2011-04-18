using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace BergenAmee.Model
{
    [DataContract]
    public class Action
    {
        [DataMember(EmitDefaultValue = false)]
        public String allowCreate { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String allowView { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String allowList { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String allowModify { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String allowDelete { get; set; }
    }
}
