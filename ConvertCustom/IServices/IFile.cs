using System;
using System.Collections.Generic;
using System.Text;

namespace ConvertCustom.IServices
{
    public interface IFile
    {
        /// <summary>
        /// 验证码图片
        /// </summary>
        /// <param name="yzm2">生成的验证码(大写)</param>
        /// <param name="yzm3">生成的验证码</param>
        /// <returns>图片base64格式</returns>
        string CreateVerifyImg(out string yzm2, out string yzm3, int width = 160, int height = 40, int codeLength = 4);
    }
}
