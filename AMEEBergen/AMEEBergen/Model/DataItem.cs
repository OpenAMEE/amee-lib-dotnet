using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace BergenAmee.Model
{
    [DataContract]
    public class DataItem
    {
        [DataMember(EmitDefaultValue = false)]
        public String apiVersion { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String uid { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String label { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DataCategory dataCategory { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Choices choices { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Selections[] selections { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public ItemDefinition itemDefinition { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Status status { get; set; }
    }
}
