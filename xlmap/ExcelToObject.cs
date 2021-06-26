using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xlmap
{
    public class ExcelToObject
    {
        private string _baseConfigPath;

        public ExcelToObject()
        {

        }

        public ExcelToObject(string baseConfigPath)
        {
            _baseConfigPath = baseConfigPath;
        }

        public string BaseConfigPath => _baseConfigPath;

        public List<T> Convert<T>(string excelFileName)
        {
            if (string.IsNullOrEmpty(excelFileName))
                throw new ArgumentNullException(nameof(excelFileName));

            if (!System.IO.File.Exists(excelFileName))
                throw new Exception($"File {excelFileName} not found");

            string configFileName = $"{typeof(T).Name}.json";
            var config = MapConfig.LoadConfiguration(configFileName, _baseConfigPath);
            if (config == null)
                throw new Exception($"Failed to load configuration from '{configFileName}.json'. Base path: '{_baseConfigPath}'.");

            List<T> result = new List<T>();
            Type mType = Type.GetType(config.FullAssemblyName);

            // read excel file, get data from worksheet and process the object mapping
            using(var stream = new FileStream(excelFileName, FileMode.Open))
            {
                stream.Position = 0;
                XSSFWorkbook wb = new XSSFWorkbook(stream);
                ISheet ws = wb.GetSheet(config.WorksheetName);
                IRow headerRow = ws.GetRow(ws.FirstRowNum);
                int colCount = headerRow.LastCellNum;
                for(int y = (ws.FirstRowNum + 1); y <= ws.LastRowNum; y++)
                {
                    IRow row = ws.GetRow(y);
                    if (row == null) continue;
                    if (row.Cells.All(e => e.CellType == CellType.Blank)) continue;

                    var itemResult = Activator.CreateInstance(mType);
                    foreach(var fieldInfo in config.FieldConfigs)
                    {
                        var colIndex = fieldInfo.ColumnIndex.Value - 1;
                        var prop = mType.GetProperty(fieldInfo.PropertyName);
                        if (prop == null)
                            throw new Exception($"Property {fieldInfo.PropertyName} not found. Config file: {configFileName}");

                        switch (fieldInfo.PropertyDataType)
                        {
                            case FieldConfig.DataType.STRING:
                                prop.SetValue(itemResult, row.GetCell(colIndex).ToString());
                                break;
                            case FieldConfig.DataType.DATETIME:
                                prop.SetValue(itemResult, row.GetCell(colIndex).DateCellValue);
                                break;
                            case FieldConfig.DataType.DOUBLE:
                                prop.SetValue(itemResult, row.GetCell(colIndex).NumericCellValue);
                                break;
                            case FieldConfig.DataType.INTEGER:
                                prop.SetValue(itemResult, (int) row.GetCell(colIndex).NumericCellValue);
                                break;
                            case FieldConfig.DataType.OBJECT:
                                break;
                        }
                    }
                    result.Add((T) itemResult);
                }
            }

            return result;
        }
    }
}
