using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///DataConvert 的摘要说明
/// </summary>
public class DataConvert
{
    /// <summary>
    /// 提供转换方法并阻止异常
    /// </summary>
	public DataConvert()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
        
	}
    /// <summary>
    /// 转换一个数字，如果转换失败则返回0
    /// </summary>
    /// <param name="ints">要转换的值</param>
    /// <returns></returns>
    public static int IntC(object ints)
    {
        try
        {
            int rNum = 0;
            if (int.TryParse(ints.ToString(), out rNum))
                return Convert.ToInt32(ints);
            return rNum;
        }
        catch
        {
            return 0;
        }
    }

    /// <summary>
    /// 转换一个数字，如果转换失败，则返回指定值
    /// </summary>
    /// <param name="ints">要转换的值</param>
    /// <param name="returnNum">转换失败时返回的值</param>
    /// <returns></returns>
    public static int IntC(object ints,int returnNum)
    {
        try{
        int rNum = returnNum;
        if (int.TryParse(ints.ToString(), out rNum))
            return Convert.ToInt32(ints);
        return returnNum;
                }
        catch
        {
            return 0;
        }
    }

    /// <summary>
    /// 转换一个十进制数
    /// </summary>
    /// <param name="dec"></param>
    /// <returns></returns>
    public static decimal DecimalC(object dec)
    {
        try
        {
            decimal rNum = 0;
            if (decimal.TryParse(dec.ToString(), out rNum))
                return Convert.ToDecimal(rNum);
            return rNum;
        }
        catch
        {
            return 0;
        }
    }

    /// <summary>
    /// 转换为双精度浮点型数字
    /// </summary>
    /// <param name="doubles"></param>
    /// <returns></returns>
    public static double DoubleC(object doubles){
        try
        {
            double rNum = 0;
            if (double.TryParse(doubles.ToString(), out rNum))
                return Convert.ToDouble(doubles);
            return rNum;
        }        
        catch
        {
            return 0;
        }
    }

    /// <summary>
    /// 转换为日期
    /// </summary>
    /// <param name="datetime"></param>
    /// <returns></returns>
    public static DateTime DateTimeC(object datetime)
    {
        try{
        DateTime rDate = DateTime.Now;
        if(DateTime.TryParse(datetime.ToString(),out rDate)){
            return Convert.ToDateTime(datetime);
        }
        return rDate;
        }
        catch
        {
            return DateTime.Now;
        }
    }

    /// <summary>
    /// 敏感标签排出
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string UrlReplace(string str)
    {
        str = str.Replace("\"", "");
        str = str.Replace("'", "");
        str = str.Replace("<", "");
        str = str.Replace(">", "");
        return str;
    }

    public static string UrlReplace(string str, int length)
    {
        str = str.Replace("\"", "");
        str = str.Replace("'", "");
        str = str.Replace("<", "");
        str = str.Replace(">", "");
        if (str.Length > length)
            str = str.Substring(0, length);
        return str;
    }
}