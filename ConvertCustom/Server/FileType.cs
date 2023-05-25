using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
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
            FileStream fs = new FileStream(Guid.NewGuid().ToString() + type, FileMode.Create, FileAccess.ReadWrite);
            GetWorkbook(dts, type).Write(fs);
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
            var extension = Path.GetExtension(fileName);
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
            GetWorkbook(dts, extension).Write(fs);
            return fs.Name;
        }

        /// <summary>
        /// 获取NPOI
        /// </summary>
        private static IWorkbook GetWorkbook(List<DataTable> dts, string type)
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
            return workbook;
        }

        /// <summary>
        /// 验证码图片
        /// </summary>
        /// <param name="yzm2">生成的验证码(大写)</param>
        /// <param name="yzm3">生成的验证码</param>
        /// <returns>图片base64格式</returns>
        public static string CreateVerifyImg(out string yzm2, out string yzm3, int width = 160, int height = 40, int codeLength = 4)
        {
            // 创建一个画布
            Bitmap image = new Bitmap(width, height);
            // 开始绘制
            Graphics g = Graphics.FromImage(image);
            try
            {
                // 清空图片背景色
                g.Clear(Color.White);

                // 字母颜色池
                List<Color> clist = new List<Color>
                {
                    Color.FromArgb(92, 34, 35),
                    Color.FromArgb(234, 81, 127),
                    Color.FromArgb(15, 89, 164),
                    Color.FromArgb(92, 179, 204),
                    Color.FromArgb(198, 223, 200),
                    Color.FromArgb(166, 82, 44)
                };

                // 背景色池
                List<Color> bgclist = new List<Color>
                {
                    Color.White
                };

                // 干扰线颜色池
                List<Color> bgsclist = new List<Color>
                {
                    Color.FromArgb(189, 174, 173)
                };

                // 随机数
                Random ran = new Random();

                // 验证码正文
                string yzm = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                yzm2 = string.Empty;

                //定义一个颜色的值当背景色选择了这个颜色后，把这个颜色从集合内删掉，防止字体颜色和背景颜色重复；
                Color bgcolor = bgclist[ran.Next(0, bgclist.Count)];
                //clist.Remove(bgcolor);

                //画背景色，参数：颜色，X轴位置，Y轴位置，宽度，高度；
                g.FillRectangle(new SolidBrush(bgcolor), 0, 0, width, height);

                //循环生成一个验证码；
                for (var i = 0; i < codeLength; i++)
                {
                    yzm2 += yzm[ran.Next(0, yzm.Length)];
                }

                int fonts = (width - Convert.ToInt32(width / codeLength * 1.5)) / codeLength;
                int fontX = ran.Next(0, fonts);
                foreach (var item in yzm2)
                {
                    //定义字体：样式“微软雅黑”，字体大小“23”；
                    Font font = new Font("微软雅黑", ran.Next(20, 23));
                    //定义画刷“SolidBrush”实线画刷，颜色从集合内选择随机颜色；
                    Color fontColor = clist[ran.Next(0, clist.Count)];
                    Brush brush = new SolidBrush(fontColor);
                    clist.Remove(fontColor);
                    // 画验证码，参数：验证码，字体，画刷，X轴位置，Y轴位置；
                    g.DrawString(item.ToString(), font, brush, ran.Next(fontX + (fonts / 2), fontX + fonts), ran.Next(0, 7));
                    fontX += fonts;
                }

                //循环生成干扰线
                for (var i = 0; i < 20; i++)
                {
                    //定义一个画笔，参数：画笔的颜色，画笔的粗细度
                    Pen pen = new Pen(new SolidBrush(bgsclist[ran.Next(0, bgsclist.Count)]), ran.Next(2, 3));
                    //干扰线的第一个点，参数：X轴位置，Y轴位置；
                    int x = ran.Next(0, width);
                    int y = ran.Next(0, height);
                    Point point = new Point(x, y);
                    //干扰线的第二个点，参数：X轴位置，Y轴位置；
                    Point point2 = new Point(ran.Next(x - 10, x + 10), ran.Next(y - 10, y + 10));
                    //画干扰线，参数：画笔，第一个点，第二个点；
                    g.DrawLine(pen, point, point2);
                }

                // 保存图片数据
                MemoryStream stream = new MemoryStream();
                image.Save(stream, ImageFormat.Png);

                //将图片转换为base64格式字符串
                string jpg_base64 = Convert.ToBase64String(stream.ToArray());
                yzm3 = yzm2;
                yzm2 = yzm2.ToUpper();
                return $"data:image/png;base64,{jpg_base64}";
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }
    }
}