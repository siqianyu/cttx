using System;
using System.Collections.Generic;
using System.Text;

namespace StarTech.Util
{
    public class StringUtility
    {
        private StringUtility()
        { }

        /// <summary>
        /// 将整型数组转化成字符数组
        /// </summary>
        /// <param name="intArray">整型数组</param>
        /// <returns>字符数组</returns>
        public static string[] TranslateToStringArray(int[] intArray)
        {
            string[] stringArray = new string[intArray.Length];
            for (int i = 0; i < intArray.Length; i++)
            {
                stringArray[i] = intArray[i].ToString();
            }
            return stringArray;
        }
    }
}
