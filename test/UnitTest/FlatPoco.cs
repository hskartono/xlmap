using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public class FlatPoco
    {
        public FlatPoco()
        {

        }

        public FlatPoco(int myIntegerNumber, string myForeignKey, string myStringName, double myDoublePrice, DateTime myBirth, DateTime mySignin)
        {
            MyIntegerNumber = myIntegerNumber;
            MyForeignKey = myForeignKey;
            MyStringName = myStringName;
            MyDoublePrice = myDoublePrice;
            MyBirthDate = myBirth;
            MySigninDateTime = mySignin;
        }

        public int MyIntegerNumber { get; set; }
        public string MyForeignKey { get; set; }
        public string MyStringName { get; set; }
        public double MyDoublePrice { get; set; }
        public DateTime MyBirthDate { get; set; }
        public DateTime MySigninDateTime { get; set; }
    }
}
