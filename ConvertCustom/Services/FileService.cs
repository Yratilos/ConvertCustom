using ConvertCustom.IServices;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ConvertCustom.Services
{
    public class FileService : IFile
    {
        public string CreateVerifyImg(out string yzm2, out string yzm3, int width = 160, int height = 40, int codeLength = 4)
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
                    g.DrawString(item.ToString(), font, brush, ran.Next(fontX + fonts / 2, fontX + fonts), ran.Next(0, 7));
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