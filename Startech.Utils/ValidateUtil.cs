using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security;
using System.Security.Cryptography;
using System.IO;
using System.Collections;
using System.Text;

namespace Startech.Utils
{
    /// <summary>
    /// ValidateUtil 的摘要说明
    /// </summary>
    public class ValidateUtil
    {
        /// <summary>
        /// 验证字符串是否合法
        /// </summary>
        public ValidateUtil()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>是否空</summary>
        /// <param name="strInput">输入字符串</param>
        /// <returns>true/false</returns>
        public static bool isBlank(string strInput)
        {
            if (strInput == null || strInput.Trim() == "")
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>是否数字</summary>
        /// <param name="strInput">输入字符串</param>
        /// <returns>true/false</returns>
        public static bool isNumeric(string strInput)
        {
            char[] ca = strInput.ToCharArray();
            bool found = true;
            for (int i = 0; i < ca.Length; i++)
            {
                if ((ca[i] < '0' || ca[i] > '9') && ca[i] != '.')
                {

                    found = false;
                    break;

                };

            };
            return found;
        }

        /// <summary>是否Null</summary>
        /// <param name="strInput">输入字符串</param>
        /// <returns>true/false</returns>
        public static bool isNull(object strInput)
        {
            return true;
        }

        /// <summary>是否为Double</summary>
        /// <param name="strInput">输入字符串</param>
        /// <returns>true/false</returns>
        public static bool isDouble(string strInput)
        {
            return true;
        }

        /// <summary>是否为Int</summary>
        /// <param name="strInput">输入字符串</param>
        /// <returns>true/false</returns>
        public static bool isInt(string strInput)
        {
            return true;
        }

        /// <summary>
        /// 验证输入邮件地址
        /// </summary>
        /// <param name="unCheck">要验证的字符串</param>
        /// <returns>返回操作状态（true为是数字类型）</returns>
        public static bool IsEmail(string strInput)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(strInput, @"^\w+((-\w+)|(\.\w+))*\@\w+((\.|-)\w+)*\.\w+$");
        }

        /// <summary>
        /// 验证输入日期格式
        /// </summary>
        /// <param name="unCheck">要验证的字符串</param>
        /// <returns>返回操作状态（true为是数字类型）</returns>
        public static bool IsDateTime(string strInput)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(strInput, @"^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})$");
        }

        //字符串截取
        public byte[] StrByte(string strInput)
        {
            byte[] mybyte = System.Text.Encoding.Default.GetBytes(strInput);
            return mybyte;
        }

        //截取字符串
        public string StrReturn(byte[] bytes, int count)
        {
            string str = System.Text.Encoding.Default.GetString(bytes, 0, count) + "..";
            return str;
        }

        /// <summary>
        /// SHA加密字符串
        /// </summary>
        /// <param name="pwd"></param>
        /// <param name="salt"></param>
        /// <returns>加密字符串</returns>
        public static string CreatePasswordHash(string pwd)
        {
            string hashedPwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "sha1");
            return hashedPwd;
        }

        #region MD5
        /// <summary>
        /// MD5 Encrypt
        /// </summary>
        /// <param name="strText">text</param>
        /// <returns>md5 Encrypt string</returns>
        public static string MD5Encrypt(string strText)
        {
            string outString = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strText, "MD5");
            return outString;
        }
        #endregion


        /// <summary>
        /// HTML encode
        /// </summary>
        /// <param name="str">string</param>
        /// <returns></returns>
        public static string Encode(string str)
        {
            str = str.Replace("&", "&amp;");
            str = str.Replace("'", "''");
            str = str.Replace("\"", "&quot;");
            str = str.Replace(" ", "&nbsp;");
            str = str.Replace("<", "&lt;");
            str = str.Replace(">", "&gt;");
            str = str.Replace("\n", "<br>");
            return str;
        }

        /// <summary>
        /// decode
        /// </summary>
        /// <param name="str">string</param>
        /// <returns></returns>
        public static string Decode(string str)
        {
            str = str.Replace("<br>", "\n");
            str = str.Replace("&gt;", ">");
            str = str.Replace("&lt;", "<");
            str = str.Replace("&nbsp;", " ");
            str = str.Replace("&quot;", "\"");
            return str;
        }

        public static void CloseSelfWindow()
        {
            string js = @"<Script language='JavaScript'>
					window.opener = null;
                    window.close();					 
                  </Script>";
            HttpContext.Current.Response.Write(js);
        }

        /// <summary>
        /// ASCII码转字符
        /// </summary>
        /// <param name="asciiCode"></param>
        /// <returns></returns>
        public static string Chr(int asciiCode)
        {
            if (asciiCode >= 0 && asciiCode <= 255)
            {
                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                byte[] byteArray = new byte[] { (byte)asciiCode };
                string strCharacter = asciiEncoding.GetString(byteArray);
                return (strCharacter);
            }
            else
            {
                throw new Exception("ASCII Code is not valid.");
            }
        }

        /// <summary>
        /// 字符转ASCII码
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public static int Asc(string character)
        {
            if (character.Length == 1)
            {
                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                int intAsciiCode = (int)asciiEncoding.GetBytes(character)[0];
                return (intAsciiCode);
            }
            else
            {
                throw new Exception("Character is not valid.");
            }

        }
    }
}