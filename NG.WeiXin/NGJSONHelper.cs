using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Json;

namespace NG.WeiXin
{
    /// <summary>
    ///JSONHelper 的摘要说明
    /// </summary>
    public class NGJSONHelper
    {


        public static string GetJSON(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            try
            {
                if (dt.Rows.Count > 0)
                {
                    Hashtable ht = new Hashtable();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        ht.Add(i, dt.Columns[i].ColumnName);
                    }
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append("{");
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            string txt = dt.Rows[i][j].ToString().Replace("\\", "\\\\").Replace("\t", "").Replace("\r", "").Replace("\n", "").Replace("\"", "’");
                            //string txt = dt.Rows[i][j].ToString();
                            sb.Append(string.Format("\"{0}\":\"{1}\",", ht[j], txt));
                        }
                        sb.Remove(sb.ToString().LastIndexOf(","), 1);
                        sb.Append("},");
                    }
                    sb.Remove(sb.ToString().LastIndexOf(","), 1);
                    ht.Clear();
                    ht = null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                sb.Append("]");
            }
            return sb.ToString();
        }
        public static T ParseFromJson<T>(string szJson)
        {
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(szJson)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                return (T)serializer.ReadObject(ms);
            }
        }
        public static string ToJGGridJson(string curPage, string totalPage, string totalRecords, DataTable dt, string[] fields, string keyField)
        {
            //{
            //"page":"2",
            //"total":2,
            //"records":"13",
            //"rows":[
            //{"id":"3","cell":["3","2007-10-02","Client 2","300.00","60.00","360.00","note invoice 3 & and amp test"]},
            //{"id":"2","cell":["2","2007-10-03","Client 1","200.00","40.00","240.00","note 2"]},
            //{"id":"1","cell":["1","2007-10-01","Client 1","100.00","20.00","120.00","note 1"]}
            //],
            //"userdata":{"amount":600,"tax":120,"total":720,"name":"Totals:"}
            //}

            string s = "{";
            s += "\"page\":\"" + curPage + "\",";
            s += "\"total\":" + totalPage + ",";
            s += "\"records\":\"" + totalRecords + "\",";
            s += "\"rows\":[";
            s += NGJSONHelper.GetJGGridRows(dt, fields, keyField);
            s += "],";
            s += "\"userdata\":{}";
            s += "}";
            return s;
        }

        public static string GetJGGridRows(DataTable dt, string[] fields, string keyField)
        {
            string s = "";
            foreach (DataRow row in dt.Rows)
            {
                string item = "";
                foreach (string field in fields)
                {
                    string txt = row[field.Trim()].ToString().Replace("\\", "\\\\").Replace("\t", "").Replace("\r", "").Replace("\n", "").Replace("\"", "’");
                    item += "\"" + txt + "\",";
                }
                if (item != "") { item = item.TrimEnd(','); }

                s += "{\"" + keyField + "\":\"" + row[keyField] + "\",\"cell\":[" + item + "]},";
            }
            if (s != "") { s = s.TrimEnd(','); }
            return s;
        }

        public static int GetTotalPages(int totalRecords, int pageSize)
        {
            if (totalRecords <= pageSize) { return 1; }
            int totalPage = totalRecords / pageSize;
            if (totalRecords % pageSize > 0) { totalPage++; }
            return totalPage;
        }

        public static Hashtable GetSearchFilter(string filter)
        {
            Hashtable htable = new Hashtable();
            if (filter.Trim() == "")
            {
                return htable;
            }
            else
            {
                string[] filters = filter.Split(new string[] { "_$$_" }, StringSplitOptions.None);
                foreach (string f in filters)
                {
                    string[] field = f.Split(new string[] { "$$" }, StringSplitOptions.None);
                    if (field.Length == 2)
                    {
                        htable.Add(field[0].Trim(), field[1].Trim());
                    }
                }
                return htable;
            }
        }
    }
}