using Shouldly;
using System;
using xlmap;
using Xunit;

namespace UnitTest
{
    public class TestFieldConfig
    {
        [Fact]
        public void isForeignKey_default_value_should_be_false()
        {
            var config = new FieldConfig();
            config.IsForeignKey.ShouldBeFalse();
        }

        [Fact]
        public void can_write_and_read_isForeignKey_property()
        {
            var config = new FieldConfig() { IsForeignKey = true };
            config.IsForeignKey.ShouldBeTrue();
        }

        [Fact]
        public void can_write_and_read_ConfigBasePath_property()
        {
            string expected = "path/to/config";
            var config = new FieldConfig() { ConfigBasePath = expected };
            config.ConfigBasePath.ShouldBe(expected);
        }


        [Fact]
        public void can_write_and_read_ColumnIndex_property()
        {
            int expected = 2;
            var config = new FieldConfig() { ColumnIndex = expected };
            config.ShouldNotBeNull();
            config.ColumnIndex.ShouldBe(expected);
        }

        [Fact]
        public void can_write_and_read_ColumnTitle_property()
        {
            string expected = "Column Title";
            var config = new FieldConfig() { ColumnTitle = expected };
            config.ShouldNotBeNull();
            config.ColumnTitle.ShouldBe(expected);
        }

        [Fact]
        public void can_write_and_read_PropertyName_property()
        {
            string expected = "PropertyName";
            var config = new FieldConfig() { PropertyName = expected };
            config.ShouldNotBeNull();
            config.PropertyName.ShouldBe(expected);
        }

        [Fact]
        public void can_write_and_read_PropertyDataType_property()
        {
            FieldConfig.DataType expected = FieldConfig.DataType.DATETIME;
            var config = new FieldConfig() { PropertyDataType = expected };
            config.ShouldNotBeNull();
            config.PropertyDataType.ShouldBe(expected);
        }

        [Fact]
        public void can_write_and_read_RefCofigFile_property()
        {
            string expected = "configfile.json";
            var config = new FieldConfig() { RefConfigFile = expected };
            config.ShouldNotBeNull();
            config.RefConfigFile.ShouldBe(expected);
        }

        [Fact]
        public void can_load_ReferenceConfiguration_by_passing_filename_as_parameter()
        {
            var config = new FieldConfig();
            config.LoadReferenceConfigFile("config/FlatPoco.json");
            config.ShouldNotBeNull();
            config.RefConfiguration.ShouldNotBeNull();
            config.RefConfiguration.FullAssemblyName = "UnitTest.FlatPoco";
        }

        [Fact]
        public void can_load_ReferenceConfiguration_using_RefConfigFile_property()
        {
            var config = new FieldConfig() { RefConfigFile = "config/FlatPoco.json" };
            config.LoadReferenceConfigFile();
            config.ShouldNotBeNull();
            config.RefConfiguration.ShouldNotBeNull();
            config.RefConfiguration.FullAssemblyName = "UnitTest.FlatPoco";
        }

        [Fact]
        public void load_empty_ReferenceConfiguration_should_throw_exception()
        {
            Should.Throw<ArgumentNullException>(() =>
            {
                var config = new FieldConfig();
                config.LoadReferenceConfigFile();
            });
        }
    }
}
