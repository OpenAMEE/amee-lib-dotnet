using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace BergenAmee.Model
{
    [DataContract]
    public class ProfileItem
    {

        [DataMember(EmitDefaultValue = false)]
        public Amounts amounts { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public ItemValue[] itemValues { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public ItemDefinition itemDefinition { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DataCategory dataCategory { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DataItem dataItem { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Environment environment { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Profile profile { get; set; }




        [DataMember(EmitDefaultValue = false)]
        public decimal energyPerTime { get; set; }




        [DataMember(EmitDefaultValue = false)]
        public String unit { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String created { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String modified { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String startDate { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String endDate { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String uid { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String name { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String path { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String dataItemUid { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String responsibleArea { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String volumePerTime { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String distance { get; set; }
    }
}
