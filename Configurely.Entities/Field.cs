using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Configurely.Entities
{
    public class Field
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public FieldType Type { get; set; }
        [DataMember]
        public string DefaultValue { get; set; }
        [DataMember]
        public int? Sort { get; set; }
        [DataMember]
        public string Value { get; set; }
    }
}
