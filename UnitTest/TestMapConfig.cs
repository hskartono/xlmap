using Shouldly;
using System;
using xlmap;
using Xunit;

namespace UnitTest
{
    public class TestMapConfig
    {
        [Fact]
        public void can_write_and_read_ConfigBasePath_property()
        {
            string expected = "config/path/";
            var config = new MapConfig() { ConfigBasePath = expected };
            config.ShouldNotBeNull();
            config.ConfigBasePath.ShouldBe(expected);
        }

        [Fact]
        public void can_write_and_read_FullAssemblyName_property()
        {
            string expected = "xlmap.XlMapConfig";
            var config = new MapConfig() { FullAssemblyName = expected };
            config.ShouldNotBeNull();
            config.FullAssemblyName.ShouldBe(expected);
        }

        [Fact]
        public void can_write_and_read_WorksheetName_property()
        {
            string expected = "worksheetname";
            var config = new MapConfig() { WorksheetName = expected };
            config.ShouldNotBeNull();
            config.WorksheetName.ShouldBe(expected);
        }

        [Fact]
        public void can_read_config_file_with_configbasepath()
        {
            var config = MapConfig.LoadConfiguration("FlatPoco.json", "config");
            config.ShouldNotBeNull();
        }

        [Fact]
        public void can_read_flat_poco_config_file()
        {
            var config = MapConfig.LoadConfiguration("config/FlatPoco.json");
            config.ShouldNotBeNull();
            config.FullAssemblyName.ShouldBe("UnitTest.FlatPoco");
            config.WorksheetName.ShouldBe("FlatPocoWorksheet");
            config.FieldConfigs.ShouldNotBeEmpty();
            config.FieldConfigs.Count.ShouldBe(6);

            config.FieldConfigs[0].ColumnIndex = 1;
            config.FieldConfigs[0].ColumnTitle = "My Integer Number";
            config.FieldConfigs[0].PropertyName = "MyIntegerNumber";
            config.FieldConfigs[0].PropertyDataType = FieldConfig.DataType.INTEGER;

            config.FieldConfigs[1].ColumnIndex = 2;
            config.FieldConfigs[1].ColumnTitle = "My Foreign Key";
            config.FieldConfigs[1].PropertyName = "MyForeignKey";
            config.FieldConfigs[1].PropertyDataType = FieldConfig.DataType.STRING;

            config.FieldConfigs[2].ColumnIndex = 3;
            config.FieldConfigs[2].ColumnTitle = "My String Name";
            config.FieldConfigs[2].PropertyName = "MyStringName";
            config.FieldConfigs[2].PropertyDataType = FieldConfig.DataType.STRING;

            config.FieldConfigs[3].ColumnIndex = 4;
            config.FieldConfigs[3].ColumnTitle = "My Double Price";
            config.FieldConfigs[3].PropertyName = "MyDoublePrice";
            config.FieldConfigs[3].PropertyDataType = FieldConfig.DataType.DOUBLE;

            config.FieldConfigs[4].ColumnIndex = 5;
            config.FieldConfigs[4].ColumnTitle = "My Birth Date";
            config.FieldConfigs[4].PropertyName = "MyBirthDate";
            config.FieldConfigs[4].PropertyDataType = FieldConfig.DataType.DATETIME;

            config.FieldConfigs[5].ColumnIndex = 6;
            config.FieldConfigs[5].ColumnTitle = "My Sign In Date Time";
            config.FieldConfigs[5].PropertyName = "MySigninDateTime";
            config.FieldConfigs[5].PropertyDataType = FieldConfig.DataType.DATETIME;
        }

        [Fact]
        public void can_read_flat_related_poco_config_file()
        {
            var config = xlmap.MapConfig.LoadConfiguration("NestedPoco.json", "config");
            config.ShouldNotBeNull();
            config.FullAssemblyName.ShouldBe("UnitTest.NestedPoco");
            config.WorksheetName.ShouldBe("NestedPocoWorksheet");

            config.FieldConfigs.ShouldNotBeEmpty();
            config.FieldConfigs.Count.ShouldBe(7);

            config.FieldConfigs[6].LoadReferenceConfigFile();
            config.FieldConfigs[6].RefConfiguration.ShouldNotBeNull();
            config.FieldConfigs[6].RefConfiguration.FullAssemblyName.ShouldBe("UnitTest.FlatPoco");
        }
    }
}
