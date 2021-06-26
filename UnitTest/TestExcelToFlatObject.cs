using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTest
{
    public class TestExcelToFlatObject
    {
        [Fact]
        public void excel_should_map_to_flat_poco()
        {
            List<FlatPoco> expected = new List<FlatPoco>()
            {
                {new FlatPoco(1, "Satu","Empat Lima Enam",1.2,new DateTime(2021,6,25,0,0,0), new DateTime(2021,6,25,10,30,0)) },
                {new FlatPoco(22, "2","Tujuh Delapan",1.25,new DateTime(2021,3,21,0,0,0), new DateTime(2021,3,21,15,30,0)) },
                {new FlatPoco(333, "Tiga Empat","Sembilan",25.765,new DateTime(2021,10,8,0,0,0), new DateTime(2021,10,8,5,40,0)) }
            };

            var converter = new xlmap.ExcelToObject("config");
            var result = converter.Convert<FlatPoco>("sample/flat_excel.xlsx");
            result.ShouldNotBeNull();
            result.Count.ShouldBe(3);
            result.ShouldBeEquivalentTo(expected);
        }
    }
}
