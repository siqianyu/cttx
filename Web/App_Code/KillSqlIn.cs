using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace StarTech
{
    /// <summary>
    /// KillSqlIn 的摘要说明
    /// </summary>
    public class KillSqlIn
    {
        public KillSqlIn()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        private static string toReplaceString = "'| |--|;|,|/|(|)|[|]|{|}|%|@|*|!";

        /// <summary>
        /// Url传输替换数字字符
        /// </summary>
        public static string Url_ReplaceByNumber(string parameter, int maxLengthByparameter)
        {
            if (parameter == null || parameter.Length > maxLengthByparameter) { return "0"; }

            System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex(@"^[-]?\d+[.]?\d*$");
            if (reg1.IsMatch(parameter) == false) { return "0"; }
            return parameter;
        }

        /// <summary>
        /// Url传输替换字符
        /// </summary>
        public static string Url_ReplaceByString(string parameter, int maxLengthByparameter)
        {
            if (parameter == null || parameter.Length > maxLengthByparameter) { return "SysError"; }

            string[] arr = toReplaceString.Split('|');
            foreach (string s in arr)
            {
                parameter = parameter.Replace(s, "");
            }
            return parameter;
        }

        /// <summary>
        /// Url传输替换字符,可以不检查特殊的字符
        /// </summary>
        public static string Url_ReplaceByString(string parameter, int maxLengthByparameter, string[] noRepalceStr)
        {
            if (parameter == null || parameter.Length > maxLengthByparameter) { return "SysError"; }

            string[] arr = toReplaceString.Split('|');
            string noRepalce = String.Join("|", noRepalceStr);
            noRepalce = "|" + noRepalce + "|";

            foreach (string s in arr)
            {
                if (noRepalce.IndexOf("|" + s + "|") == -1)
                {
                    parameter = parameter.Replace(s, "");
                }
            }
            return parameter;
        }

        /// <summary>
        /// Form提交接收处理数字字符
        /// </summary>
        public static string Form_ReplaceByNumber(string parameter, int maxLengthByparameter)
        {
            if (parameter == null || parameter.Length > maxLengthByparameter) { return "0"; }

            System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex(@"^[-]?\d+[.]?\d*$");
            if (reg1.IsMatch(parameter) == false) { return "0"; }
            return parameter;
        }

        /// <summary>
        /// Form提交接收处理字符
        /// </summary>
        public static string Form_ReplaceByString(string parameter, int maxLengthByparameter)
        {
            if (parameter == null) { return "SysError"; }
            if (maxLengthByparameter > 0)
            {
                if (parameter.Length > maxLengthByparameter) { return "SysError"; }
            }

            parameter = HttpContext.Current.Server.HtmlEncode(parameter);
            parameter = parameter.Replace("'", "’");
            parameter = parameter.Replace("\r\n", "<br>");
            parameter = parameter.Replace("\n", "<br>");
            return parameter;
        }
    }

}
