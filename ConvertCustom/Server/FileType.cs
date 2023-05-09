using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace ConvertCustom.Server
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
        /// <param name="dt"></param>
        /// <param name="type">.xlsx/.xls</param>
        /// <returns></returns>
        public static byte[] ParseExcelByte(DataTable dt, string type)
        {
            return ParseExcelBytes(new List<DataTable> { dt }, type);
        }

        /// <summary>
        /// Excel
        /// NPOI生成文件，读取二进制流后删除文件
        /// </summary>
        /// <param name="dts"></param>
        /// <param name="type">.xlsx/.xls</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static byte[] ParseExcelBytes(List<DataTable> dts, string type)
        {
            IWorkbook workbook;
            if (type == ".xlsx")
            {
                workbook = new XSSFWorkbook();
            }
            else if (type == ".xls")
            {
                workbook = new HSSFWorkbook();
            }
            else
            {
                throw new ArgumentException("请添加文件名后缀为xlsx或lsx。");
            }
            for (int d = 0; d < dts.Count; d++)
            {
                DataTable dt = dts[d];
                ISheet sheet = (string.IsNullOrEmpty(dt.TableName) ? workbook.CreateSheet($"Sheet{d + 1}") : workbook.CreateSheet(dt.TableName));
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
            FileStream fs = new FileStream(Guid.NewGuid().ToString() + type, FileMode.Create, FileAccess.ReadWrite);
            workbook.Write(fs);
            fs = new FileStream(fs.Name, FileMode.Open);
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            File.Delete(fs.Name);
            return bytes;
        }
    }
}