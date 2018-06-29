using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;

/// <summary>
/// //http://www.codeproject.com/KB/printing/datagridviewprinter.aspx
/// </summary>
/// 
namespace StarTech.AbcSettlement.BLL
{

    public class DataGridViewExcel
    {
        private int position = 0;
        public string ExcelHeader()
        {
            // Excel 标准头部定义
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<?xml version=\"1.0\"?>\n");
            sb.Append("<?mso-application progid=\"Excel.Sheet\"?>\n");
            sb.Append(
              "<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\" ");
            sb.Append("xmlns:o=\"urn:schemas-microsoft-com:office:office\" ");
            sb.Append("xmlns:x=\"urn:schemas-microsoft-com:office:excel\" ");
            sb.Append("xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\" ");
            sb.Append("xmlns:html=\"http://www.w3.org/TR/REC-html40\">\n");
            sb.Append(
              "<DocumentProperties xmlns=\"urn:schemas-microsoft-com:office:office\">");
            sb.Append("</DocumentProperties>");
            sb.Append(
              "<ExcelWorkbook xmlns=\"urn:schemas-microsoft-com:office:excel\">\n");
            sb.Append("<ProtectStructure>False</ProtectStructure>\n");
            sb.Append("<ProtectWindows>False</ProtectWindows>\n");
            sb.Append("</ExcelWorkbook>\n");
            return sb.ToString();
        }
    }
    public class ExcelHelper
    {
        public static StringWriter CreateExcelTableByXml(DataTable source, ArrayList list, Hashtable hTable)
        {
            StringBuilder strb = new StringBuilder();
            //position = 0;

            string startExcelXML = new DataGridViewExcel().ExcelHeader();
            startExcelXML += "<Styles>\r\n " +
                "<Style ss:ID=\"Default\" ss:Name=\"Normal\">\r\n " +
                "<Alignment ss:Vertical=\"Bottom\"/>\r\n <Borders/>" +
                "\r\n <Font/>\r\n <Interior/>\r\n <NumberFormat/>" +
                "\r\n <Protection/>\r\n </Style>\r\n " +
                "<Style ss:ID=\"BoldColumn\">\r\n <Font " +
                "x:Family=\"Swiss\" ss:Bold=\"1\"/>\r\n </Style>\r\n " +
                "<Style ss:ID=\"StringLiteral\">\r\n <NumberFormat" +
                " ss:Format=\"@\"/>\r\n </Style>\r\n <Style " +
                "ss:ID=\"Decimal\">\r\n <NumberFormat " +
                "ss:Format=\"0.00\"/>\r\n </Style>\r\n " +
                "<Style ss:ID=\"Integer\">\r\n <NumberFormat " +
                "ss:Format=\"0\"/>\r\n </Style>\r\n <Style " +
                "ss:ID=\"DateLiteral\">\r\n <NumberFormat " +
                "ss:Format=\"yyyy-mm-dd hh:mm:ss;@\"/>\r\n </Style>\r\n " +
                "</Styles>\r\n ";
            const string endExcelXML = "</Workbook>";

            int rowCount = 0;
            int sheetCount = 1;

            strb.Append(startExcelXML);
            strb.Append("<Worksheet ss:Name=\"Sheet" + sheetCount + "\">");
            strb.Append("<Table>");

            #region header
            strb.Append("<Row>");
            for (int i = 0; i < list.Count; i++)
            {
                strb.Append("<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">");
                strb.Append(list[i].ToString().Split('$')[1]);
                strb.Append("</Data></Cell>");
            }
            strb.Append("</Row>");
            #endregion

            foreach (DataRow x in source.Rows)
            {
                rowCount++;
                //if the number of rows is > 64000 create a new page to continue output
                if (rowCount == 64000)
                {
                    rowCount = 0;
                    sheetCount++;
                    strb.Append("</Table>");
                    strb.Append(" </Worksheet>");
                    strb.Append("<Worksheet ss:Name=\"Sheet" + sheetCount + "\">");
                    strb.Append("<Table>");
                }
                #region body
                strb.Append("<Row>");
                for (int i = 0; i < list.Count; i++)
                {
                    string field = list[i].ToString().Split('$')[0];
                    System.Type rowType;
                    rowType = x[field].GetType();

                    switch (rowType.ToString())
                    {
                        case "System.String":
                            string XMLstring = x[field].ToString();
                            XMLstring = XMLstring.Trim();
                            XMLstring = XMLstring.Replace("&", "&");
                            XMLstring = XMLstring.Replace(">", "&gt;");
                            XMLstring = XMLstring.Replace("<", "&lt;");
                            strb.Append("<Cell ss:StyleID=\"StringLiteral\">" +
                                           "<Data ss:Type=\"String\">");
                            strb.Append(XMLstring);
                            strb.Append("</Data></Cell>");
                            break;
                        case "System.DateTime":
                            //Excel has a specific Date Format of YYYY-MM-DD followed by  
                            //the letter 'T' then hh:mm:sss.lll Example 2005-01-31T24:01:21.000
                            //The Following Code puts the date stored in XMLDate 
                            //to the format above
                            DateTime XMLDate = (DateTime)x[field];
                            string XMLDatetoString = ""; //Excel Converted Date
                            XMLDatetoString = XMLDate.Year.ToString() +
                                 "-" +
                                 (XMLDate.Month < 10 ? "0" +
                                 XMLDate.Month.ToString() : XMLDate.Month.ToString()) +
                                 "-" +
                                 (XMLDate.Day < 10 ? "0" +
                                 XMLDate.Day.ToString() : XMLDate.Day.ToString()) +
                                 "T" +
                                 (XMLDate.Hour < 10 ? "0" +
                                 XMLDate.Hour.ToString() : XMLDate.Hour.ToString()) +
                                 ":" +
                                 (XMLDate.Minute < 10 ? "0" +
                                 XMLDate.Minute.ToString() : XMLDate.Minute.ToString()) +
                                 ":" +
                                 (XMLDate.Second < 10 ? "0" +
                                 XMLDate.Second.ToString() : XMLDate.Second.ToString()) +
                                 ".000";
                            strb.Append("<Cell ss:StyleID=\"DateLiteral\">" +
                                         "<Data ss:Type=\"DateTime\">");
                            strb.Append(XMLDatetoString);
                            strb.Append("</Data></Cell>");
                            break;
                        case "System.Boolean":
                            strb.Append("<Cell ss:StyleID=\"StringLiteral\">" +
                                        "<Data ss:Type=\"String\">");
                            strb.Append(x[field].ToString());
                            strb.Append("</Data></Cell>");
                            break;
                        case "System.Int16":
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            strb.Append("<Cell>" +
                                    "<Data ss:Type=\"Number\">");
                            strb.Append(x[field].ToString());
                            strb.Append("</Data></Cell>");
                            break;
                        case "System.Decimal":
                        case "System.Double":
                            strb.Append("<Cell>" +
                                  "<Data ss:Type=\"Number\">");
                            strb.Append(x[field].ToString());
                            strb.Append("</Data></Cell>");
                            break;
                        case "System.DBNull":
                            strb.Append("<Cell ss:StyleID=\"StringLiteral\">" +
                                  "<Data ss:Type=\"String\">");
                            strb.Append("");
                            strb.Append("</Data></Cell>");
                            break;
                        default:
                            if (Regex.IsMatch(x[field].ToString(), "^([+-]?)\\d*\\.?\\d+$"))
                            {
                                strb.Append("<Cell><Data ss:Type=\"Number\">");
                                strb.Append(x[field].ToString());
                                strb.Append("</Data></Cell>");
                            }
                            else
                            {
                                strb.Append("<Cell><Data ss:Type=\"String\">");
                                strb.Append(x[field].ToString());
                                strb.Append("</Data></Cell>");
                            }
                            break;
                    }
                }
                strb.Append("</Row>");
                #endregion
            }

            #region footer
            if (hTable != null)
            {
                //sum
                string keyStr = "";
                foreach (string key in hTable.Keys) { keyStr += key + ","; }

                if (keyStr != "")
                {
                    string[] keyArr = keyStr.TrimEnd(',').Split(',');
                    foreach (string s in keyArr)
                    {
                        hTable[s] = source.Compute("sum(" + s + ")", "");
                    }
                }

                strb.Append("<Row>");
                for (int i = 0; i < list.Count; i++)
                {
                    string field = list[i].ToString().Split('$')[0];
                    if (hTable.ContainsKey(field))
                    {
                        strb.Append("<Cell><Data ss:Type=\"Number\">");
                        strb.Append(hTable[field]);
                        strb.Append("</Data></Cell>");
                    }
                    else
                    {
                        strb.Append("<Cell><Data ss:Type=\"String\"></Data></Cell>");
                    }
                }
                strb.Append("</Row>");
            }
            #endregion

            strb.Append("</Table>");
            strb.Append(" </Worksheet>");
            strb.Append(endExcelXML);

            StringWriter sw = new StringWriter();
            sw.Write(strb.ToString());
            sw.Close();
            return sw;

        }

        public static StringWriter CreateExcelTableByXml(DataTable source, ArrayList list, Hashtable hTable,string DateFormate)
        {
            StringBuilder strb = new StringBuilder();
            //position = 0;

            string startExcelXML = new DataGridViewExcel().ExcelHeader();
            startExcelXML += "<Styles>\r\n " +
                "<Style ss:ID=\"Default\" ss:Name=\"Normal\">\r\n " +
                "<Alignment ss:Vertical=\"Bottom\"/>\r\n <Borders/>" +
                "\r\n <Font/>\r\n <Interior/>\r\n <NumberFormat/>" +
                "\r\n <Protection/>\r\n </Style>\r\n " +
                "<Style ss:ID=\"BoldColumn\">\r\n <Font " +
                "x:Family=\"Swiss\" ss:Bold=\"1\"/>\r\n </Style>\r\n " +
                "<Style ss:ID=\"StringLiteral\">\r\n <NumberFormat" +
                " ss:Format=\"@\"/>\r\n </Style>\r\n <Style " +
                "ss:ID=\"Decimal\">\r\n <NumberFormat " +
                "ss:Format=\"0.00\"/>\r\n </Style>\r\n " +
                "<Style ss:ID=\"Integer\">\r\n <NumberFormat " +
                "ss:Format=\"0\"/>\r\n </Style>\r\n <Style " +
                "ss:ID=\"DateLiteral\">\r\n <NumberFormat " +
                "ss:Format=\""+DateFormate+";@\"/>\r\n </Style>\r\n " +
                "</Styles>\r\n ";
            const string endExcelXML = "</Workbook>";

            int rowCount = 0;
            int sheetCount = 1;

            strb.Append(startExcelXML);
            strb.Append("<Worksheet ss:Name=\"Sheet" + sheetCount + "\">");
            strb.Append("<Table>");

            #region header
            strb.Append("<Row>");
            for (int i = 0; i < list.Count; i++)
            {
                strb.Append("<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">");
                strb.Append(list[i].ToString().Split('$')[1]);
                strb.Append("</Data></Cell>");
            }
            strb.Append("</Row>");
            #endregion

            foreach (DataRow x in source.Rows)
            {
                rowCount++;
                //if the number of rows is > 64000 create a new page to continue output
                if (rowCount == 64000)
                {
                    rowCount = 0;
                    sheetCount++;
                    strb.Append("</Table>");
                    strb.Append(" </Worksheet>");
                    strb.Append("<Worksheet ss:Name=\"Sheet" + sheetCount + "\">");
                    strb.Append("<Table>");
                }
                #region body
                strb.Append("<Row>");
                for (int i = 0; i < list.Count; i++)
                {
                    string field = list[i].ToString().Split('$')[0];
                    System.Type rowType;
                    rowType = x[field].GetType();

                    switch (rowType.ToString())
                    {
                        case "System.String":
                            string XMLstring = x[field].ToString();
                            XMLstring = XMLstring.Trim();
                            XMLstring = XMLstring.Replace("&", "&");
                            XMLstring = XMLstring.Replace(">", "&gt;");
                            XMLstring = XMLstring.Replace("<", "&lt;");
                            strb.Append("<Cell ss:StyleID=\"StringLiteral\">" +
                                           "<Data ss:Type=\"String\">");
                            strb.Append(XMLstring);
                            strb.Append("</Data></Cell>");
                            break;
                        case "System.DateTime":
                            //Excel has a specific Date Format of YYYY-MM-DD followed by  
                            //the letter 'T' then hh:mm:sss.lll Example 2005-01-31T24:01:21.000
                            //The Following Code puts the date stored in XMLDate 
                            //to the format above
                            DateTime XMLDate = (DateTime)x[field];
                            string XMLDatetoString = ""; //Excel Converted Date
                            XMLDatetoString = XMLDate.Year.ToString() +
                                 "-" +
                                 (XMLDate.Month < 10 ? "0" +
                                 XMLDate.Month.ToString() : XMLDate.Month.ToString()) +
                                 "-" +
                                 (XMLDate.Day < 10 ? "0" +
                                 XMLDate.Day.ToString() : XMLDate.Day.ToString()) +
                                 "T" +
                                 (XMLDate.Hour < 10 ? "0" +
                                 XMLDate.Hour.ToString() : XMLDate.Hour.ToString()) +
                                 ":" +
                                 (XMLDate.Minute < 10 ? "0" +
                                 XMLDate.Minute.ToString() : XMLDate.Minute.ToString()) +
                                 ":" +
                                 (XMLDate.Second < 10 ? "0" +
                                 XMLDate.Second.ToString() : XMLDate.Second.ToString()) +
                                 ".000";
                            strb.Append("<Cell ss:StyleID=\"DateLiteral\">" +
                                         "<Data ss:Type=\"DateTime\">");
                            strb.Append(XMLDatetoString);
                            strb.Append("</Data></Cell>");
                            break;
                        case "System.Boolean":
                            strb.Append("<Cell ss:StyleID=\"StringLiteral\">" +
                                        "<Data ss:Type=\"String\">");
                            strb.Append(x[field].ToString());
                            strb.Append("</Data></Cell>");
                            break;
                        case "System.Int16":
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            strb.Append("<Cell>" +
                                    "<Data ss:Type=\"Number\">");
                            strb.Append(x[field].ToString());
                            strb.Append("</Data></Cell>");
                            break;
                        case "System.Decimal":
                        case "System.Double":
                            strb.Append("<Cell>" +
                                  "<Data ss:Type=\"Number\">");
                            strb.Append(x[field].ToString());
                            strb.Append("</Data></Cell>");
                            break;
                        case "System.DBNull":
                            strb.Append("<Cell ss:StyleID=\"StringLiteral\">" +
                                  "<Data ss:Type=\"String\">");
                            strb.Append("");
                            strb.Append("</Data></Cell>");
                            break;
                        default:
                            if (Regex.IsMatch(x[field].ToString(), "^([+-]?)\\d*\\.?\\d+$"))
                            {
                                strb.Append("<Cell><Data ss:Type=\"Number\">");
                                strb.Append(x[field].ToString());
                                strb.Append("</Data></Cell>");
                            }
                            else
                            {
                                strb.Append("<Cell><Data ss:Type=\"String\">");
                                strb.Append(x[field].ToString());
                                strb.Append("</Data></Cell>");
                            }
                            break;
                    }
                }
                strb.Append("</Row>");
                #endregion
            }

            #region footer
            if (hTable != null)
            {
                //sum
                string keyStr = "";
                foreach (string key in hTable.Keys) { keyStr += key + ","; }

                if (keyStr != "")
                {
                    string[] keyArr = keyStr.TrimEnd(',').Split(',');
                    foreach (string s in keyArr)
                    {
                        hTable[s] = source.Compute("sum(" + s + ")", "");
                    }
                }

                strb.Append("<Row>");
                for (int i = 0; i < list.Count; i++)
                {
                    string field = list[i].ToString().Split('$')[0];
                    if (hTable.ContainsKey(field))
                    {
                        strb.Append("<Cell><Data ss:Type=\"Number\">");
                        strb.Append(hTable[field]);
                        strb.Append("</Data></Cell>");
                    }
                    else
                    {
                        strb.Append("<Cell><Data ss:Type=\"String\"></Data></Cell>");
                    }
                }
                strb.Append("</Row>");
            }
            #endregion

            strb.Append("</Table>");
            strb.Append(" </Worksheet>");
            strb.Append(endExcelXML);

            StringWriter sw = new StringWriter();
            sw.Write(strb.ToString());
            sw.Close();
            return sw;

        }

        public static StringWriter DataSetToExcel(DataTable source, string fileName)
        {
            StringBuilder strb = new StringBuilder();
            //position = 0;
           
            string startExcelXML = new DataGridViewExcel().ExcelHeader();
            startExcelXML += "<Styles>\r\n " +
                "<Style ss:ID=\"Default\" ss:Name=\"Normal\">\r\n " +
                "<Alignment ss:Vertical=\"Bottom\"/>\r\n <Borders/>" +
                "\r\n <Font/>\r\n <Interior/>\r\n <NumberFormat/>" +
                "\r\n <Protection/>\r\n </Style>\r\n " +
                "<Style ss:ID=\"BoldColumn\">\r\n <Font " +
                "x:Family=\"Swiss\" ss:Bold=\"1\"/>\r\n </Style>\r\n " +
                "<Style ss:ID=\"StringLiteral\">\r\n <NumberFormat" +
                " ss:Format=\"@\"/>\r\n </Style>\r\n <Style " +
                "ss:ID=\"Decimal\">\r\n <NumberFormat " +
                "ss:Format=\"0.00\"/>\r\n </Style>\r\n " +
                "<Style ss:ID=\"Integer\">\r\n <NumberFormat " +
                "ss:Format=\"0\"/>\r\n </Style>\r\n <Style " +
                "ss:ID=\"DateLiteral\">\r\n <NumberFormat " +
                "ss:Format=\"yyyy-mm-dd hh:mm:ss;@\"/>\r\n </Style>\r\n " +
                "</Styles>\r\n ";
            const string endExcelXML = "</Workbook>";

            int rowCount = 0;
            int sheetCount = 1;

            strb.Append(startExcelXML);
            strb.Append("<Worksheet ss:Name=\"Sheet" + sheetCount + "\">");
            strb.Append("<Table>");
            strb.Append("<Row>");
            for (int x = 0; x < source.Columns.Count; x++)
            {
                strb.Append("<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">");
                strb.Append(source.Columns[x].ColumnName);
                strb.Append("</Data></Cell>");
            }
            strb.Append("</Row>");
            foreach (DataRow x in source.Rows)
            {
                rowCount++;
                //if the number of rows is > 64000 create a new page to continue output
                if (rowCount == 64000)
                {
                    rowCount = 0;
                    sheetCount++;
                    strb.Append("</Table>");
                    strb.Append(" </Worksheet>");
                    strb.Append("<Worksheet ss:Name=\"Sheet" + sheetCount + "\">");
                    strb.Append("<Table>");
                }
                strb.Append("<Row>");
                for (int y = 0; y < source.Columns.Count; y++)
                {
                    System.Type rowType;
                    rowType = x[y].GetType();
                    switch (rowType.ToString())
                    {
                        case "System.String":
                            string XMLstring = x[y].ToString();
                            XMLstring = XMLstring.Trim();
                            XMLstring = XMLstring.Replace("&", "&");
                            XMLstring = XMLstring.Replace(">", "&gt;");
                            XMLstring = XMLstring.Replace("<", "&lt;");
                            strb.Append("<Cell ss:StyleID=\"StringLiteral\">" +
                                           "<Data ss:Type=\"String\">");
                            strb.Append(XMLstring);
                            strb.Append("</Data></Cell>");
                            break;
                        case "System.DateTime":
                            //Excel has a specific Date Format of YYYY-MM-DD followed by  
                            //the letter 'T' then hh:mm:sss.lll Example 2005-01-31T24:01:21.000
                            //The Following Code puts the date stored in XMLDate 
                            //to the format above
                            DateTime XMLDate = (DateTime)x[y];
                            string XMLDatetoString = ""; //Excel Converted Date
                            XMLDatetoString = XMLDate.Year.ToString() +
                                 "-" +
                                 (XMLDate.Month < 10 ? "0" +
                                 XMLDate.Month.ToString() : XMLDate.Month.ToString()) +
                                 "-" +
                                 (XMLDate.Day < 10 ? "0" +
                                 XMLDate.Day.ToString() : XMLDate.Day.ToString()) +
                                 "T" +
                                 (XMLDate.Hour < 10 ? "0" +
                                 XMLDate.Hour.ToString() : XMLDate.Hour.ToString()) +
                                 ":" +
                                 (XMLDate.Minute < 10 ? "0" +
                                 XMLDate.Minute.ToString() : XMLDate.Minute.ToString()) +
                                 ":" +
                                 (XMLDate.Second < 10 ? "0" +
                                 XMLDate.Second.ToString() : XMLDate.Second.ToString()) +
                                 ".000";
                            strb.Append("<Cell ss:StyleID=\"DateLiteral\">" +
                                         "<Data ss:Type=\"DateTime\">");
                            strb.Append(XMLDatetoString);
                            strb.Append("</Data></Cell>");
                            break;
                        case "System.Boolean":
                            strb.Append("<Cell ss:StyleID=\"StringLiteral\">" +
                                        "<Data ss:Type=\"String\">");
                            strb.Append(x[y].ToString());
                            strb.Append("</Data></Cell>");
                            break;
                        case "System.Int16":
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            strb.Append("<Cell ss:StyleID=\"Integer\">" +
                                    "<Data ss:Type=\"Number\">");
                            strb.Append(x[y].ToString());
                            strb.Append("</Data></Cell>");
                            break;
                        case "System.Decimal":
                        case "System.Double":
                            strb.Append("<Cell ss:StyleID=\"Integer\">" +
                                  "<Data ss:Type=\"Number\">");
                            strb.Append(x[y].ToString());
                            strb.Append("</Data></Cell>");
                            break;
                        case "System.DBNull":
                            strb.Append("<Cell ss:StyleID=\"StringLiteral\">" +
                                  "<Data ss:Type=\"String\">");
                            strb.Append("");
                            strb.Append("</Data></Cell>");
                            break;
                        default:
                            throw (new Exception(rowType.ToString() + " not handled."));
                    }

                    //position = y;
                    //ProgressEventArgs pe = new ProgressEventArgs(position);
                    //OnProgressChange(pe);
                }
                strb.Append("</Row>");
            }
            strb.Append("</Table>");
            strb.Append(" </Worksheet>");
            strb.Append(endExcelXML);

            StringWriter sw = new StringWriter();
            sw.Write(strb.ToString());
            sw.Close();
            return sw;
            
        }

    }

}
