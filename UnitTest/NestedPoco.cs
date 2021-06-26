using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public class NestedPoco
    {
        public int MyIntegerNumber { get; set; }
        public string MyForeignKey { get; set; }
        public string MyStringName { get; set; }
        public double MyDoublePrice { get; set; }
        public DateTime MyBirthDate { get; set; }
        public DateTime MySigninDateTime { get; set; }
        public FlatPoco MyRelatedObject { get; set; }
    }
}
