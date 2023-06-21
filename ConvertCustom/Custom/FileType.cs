using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace ConvertCustom.Custom
{
    /// <summary>
    /// 转换二进制流
    /// </summary>
    public static class FileType
    {
        /// <summary>
        /// Excel
        /// NPOI生成文件，读取二进制流后删除文件
        /// </summary>
        /// <param name="type">.xlsx/.xls</param>
        public static byte[] ParseExcelByte(DataTable dt, string type)
        {
            return ParseExcelBytes(new List<DataTable> { dt }, type);
        }

        /// <summary>
        /// Excel
        /// NPOI生成文件，读取二进制流后删除文件
        /// </summary>
        /// <param name="type">.xlsx/.xls</param>
        public static byte[] ParseExcelBytes(List<DataTable> dts, string type)
        {
            FileStream fs;
            GetWorkbook(dts, Guid.NewGuid() + type, out fs);
            fs = new FileStream(fs.Name, FileMode.Open);
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            File.Delete(fs.Name);
            return bytes;
        }

        /// <summary>
        /// Excel
        /// NPOI生成文件,不删除文件
        /// </summary>
        /// <param name="fileName">.xlsx/.xls后缀</param>
        public static string ParseExcelURL(DataTable dt, string fileName)
        {
            return ParseExcelURLs(new List<DataTable> { dt }, fileName);
        }

        /// <summary>
        /// Excel
        /// NPOI生成文件,不删除文件
        /// </summary>
        /// <param name="fileName">.xlsx/.xls后缀</param>
        /// <returns>文件本地路径</returns>
        public static string ParseExcelURLs(List<DataTable> dts, string fileName)
        {
            FileStream fs;
            GetWorkbook(dts, fileName, out fs);
            return fs.Name;
        }

        /// <summary>
        /// 获取文件流
        /// </summary>
        private static void GetWorkbook(List<DataTable> dts, string fileName, out FileStream fileStream)
        {
            var extension = Path.GetExtension(fileName);
            IWorkbook workbook;
            if (extension == ".xlsx")
            {
                workbook = new XSSFWorkbook();
            }
            else if (extension == ".xls")
            {
                workbook = new HSSFWorkbook();
            }
            else
            {
                throw new ArgumentException("请添加文件后缀.xlsx或.lsx。");
            }
            for (int d = 0; d < dts.Count; d++)
            {
                DataTable dt = dts[d];
                ISheet sheet = string.IsNullOrEmpty(dt.TableName) ? workbook.CreateSheet($"Sheet{d + 1}") : workbook.CreateSheet(dt.TableName);
                IRow row = sheet.CreateRow(0);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    row.CreateCell(j).SetCellValue(dt.Columns[j].ColumnName);
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row2 = sheet.CreateRow(i + 1);
                    for (int k = 0; k < dt.Columns.Count; k++)
                    {
                        row2.CreateCell(k).SetCellValue(dt.Rows[i][k].ToString());
                    }
                }
            }
            fileStream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
            workbook.Write(fileStream);
        }
    }
}