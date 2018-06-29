using System;
using System.Collections.Generic;
using System.Text;

namespace StarTech.Util
{
    /// <summary>
    /// 标准曲线一元线性回归方程计算公式y=bx+a
    /// </summary>
    public class BZQX
    {
        /// <summary>
        /// 一元线性回归方程计算公式y=bx+a,返回值(b=0.0761,a=0.003132,r=0.9999)
        /// </summary>
        /// <param name="strXs">x坐标轴集合多个用逗号(,)隔开</param>
        /// <param name="strYs">y坐标轴集合多个用逗号(,)隔开</param>
        /// <returns></returns>
        public static string ToComputer(string strXs, string strYs)
        {
            try
            {
                if (strXs.Trim().Length <= 0) //数组X的检验
                {
                    return "请输入数组X";
                }

                if (strYs.Trim().Length <= 0) //数组Y的检验
                {
                    return "请输入数组Y";
                }

                double[] arr = new double[2]; //用来存放a,b的值

                string[] x = strXs.Trim().Split(new string[] { "," }, StringSplitOptions.None); //数组X
                string[] y = strYs.Trim().Split(new string[] { "," }, StringSplitOptions.None); //数组Y

                if (x.Length != y.Length)
                {
                    return "数组X与数组Y的长度不等，请检查两个数组的长度！";
                }


                int len = x.Length; //取得数组长度

                double A = 0.0, B = 0.0, C = 0.0, D = 0.0, delta;

                for (int i = 0; i < len; ++i)
                {
                    A += double.Parse(x[i]) * double.Parse(x[i]);
                    B += double.Parse(x[i]);
                    C += double.Parse(x[i]) * double.Parse(y[i]);
                    D += double.Parse(y[i]);
                }
                delta = A * len - B * B;

                if (Math.Abs(delta) < 1e-10)
                {
                    return "Error!Divide by zero";
                }
                else
                {
                    arr[0] = ((C * len - B * D) / delta);
                    arr[1] = ((A * D - C * B) / delta);
                }


                //求相关性R的值
                double x2 = 0, y2 = 0, xy = 0, s_x = 0, s_y = 0, R;

                for (int i = 0; i < len; i++)
                {
                    x2 += double.Parse(x[i]) * double.Parse(x[i]);
                    y2 += double.Parse(y[i]) * double.Parse(y[i]);
                    xy += double.Parse(x[i]) * double.Parse(y[i]);
                    s_x += double.Parse(x[i]);
                    s_y += double.Parse(y[i]);
                }
                R = (len * xy - s_x * s_y) / (Math.Sqrt(len * x2 - s_x * s_x) * Math.Sqrt(len * y2 - s_y * s_y));


                return "b=" + arr[0].ToString() + ",a=" + arr[1].ToString() + ",r=" + R.ToString();


            }
            catch (Exception ee) { return "error=" + ee.Message; }
        }


        /// <summary>
        /// 截取字符串长度
        /// </summary>
        public static string GetStringByLen(string str, int len)
        {
            if (str.Length <= len) { return str; }
            return str.Substring(0, len);
        }

        /// <summary>
        /// 截取字符串长度保留0后面3位
        /// </summary>
        public static string GetStringByZeroLen(string str)
        {
            if (str.StartsWith("0.00000")) { return GetStringByLen(str, 10); }
            if (str.StartsWith("-0.00000")) { return GetStringByLen(str, 11); }
            if (str.StartsWith("0.0000")) { return GetStringByLen(str, 9); }
            else if (str.StartsWith("-0.000")) { return GetStringByLen(str, 10); }
            else if (str.StartsWith("0.000")) { return GetStringByLen(str, 8); }
            else if (str.StartsWith("-0.000")) { return GetStringByLen(str, 9); }
            else if (str.StartsWith("-0.00")) { return GetStringByLen(str, 8); }
            else { return GetStringByLen(str, 7); }
        }
    }

}
