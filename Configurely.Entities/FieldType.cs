using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Configurely.Entities
{
    public class FieldType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Type { get; set; }
    }
}
