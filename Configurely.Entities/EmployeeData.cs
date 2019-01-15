using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Configurely.Entities
{
    public class EmployeeData
    {
        [DataMember]
        public int EmployeeID { get; set; }
        [DataMember]
        public Field Field { get; set; }
        [DataMember]
        public string Value { get; set; }
    }
}
