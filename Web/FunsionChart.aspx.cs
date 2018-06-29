using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using InfoSoftGlobal;

public partial class FunsionChart : StarTech.Adapter.StarTechPage
{
    public string x = string.Empty;
    public string y = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        x = Request.QueryString["x"] == null ? "" : Request.QueryString["x"].ToString();
        y = Request.QueryString["y"] == null ? "" : Request.QueryString["y"].ToString();
        if (!IsPostBack)
        {
            BindChart();
        }
    }

    
    public void BindChart()
    {

        string xml = "";
        string[] _xArray=null;
        string[] _yArray = null;
        if (!string.IsNullOrEmpty(x) && !string.IsNullOrEmpty(y))
        {
            _xArray = x.Trim().Split(',');
            _yArray = y.Trim().Split(',');



            int _xlength = _xArray.Length;
            int _ylength = _yArray.Length;

            decimal _xmaxvalue = GetMaxValue(_xArray);
            decimal _ymaxvalue = GetMaxValue(_yArray);

            int _xvalue = Convert.ToInt32((_xmaxvalue + Convert.ToDecimal(_xmaxvalue * Convert.ToDecimal(0.3))).ToString("F0"));
            int _yvalue = Convert.ToInt32((_ymaxvalue + Convert.ToDecimal(_ymaxvalue * Convert.ToDecimal(0.4))).ToString("F0"));
            //palette--调色板，caption--标题，yaxisname--Y轴说明，xaxisname--X轴说明，xaxismaxvalue--X轴最大值，xaxisminvalue--X轴最小值，bgcolor--背景颜色，legendshadow--阴影（估计），legendborderalpha--边线（估计），canvasborderthickness--边框厚度，canvasborderalpha--边框，divlinealpha--div线，showregressionline--显示回归线，yaxisminvalue--Y轴最小值，numbersuffix--数字后缀，animation--动画方案

            xml = @"<?xml version='1.0' encoding='utf-8'?><chart palette='2' yaxisname='' caption='' xaxisname='' xaxismaxvalue='" + _xvalue.ToString() + "' xaxisminvalue='0' bgcolor='FFFFFF' legendshadow='1' legendborderalpha='1' canvasborderthickness='1' canvasborderalpha='1' divlinealpha='2' showregressionline='1' yaxisminvalue='0' yaxismaxvalue='" + _yvalue.ToString() + "' numbersuffix='' animation='0'>";

            //X轴位数
            xml += @"<categories verticallinethickness='1' verticallinealpha='10'>";
            string s = "";
            for (int i = 1; i < 11; i++)
            {
                s += "<category label='" + (_xvalue * Convert.ToDecimal(0.1 * i)) + "' x='" + (_xvalue * Convert.ToDecimal(0.1 * i)) + "' showverticalline='1' />";
            }

            

            xml += s + "</categories>";
            //X，Y数据位置id为ID，X为X轴坐标，Y为Y轴坐标，toolText为鼠标滑过显示的文本
            xml += @"<dataset seriesname=''  color='#1790E1' showplotborder='1' anchorSides='4' anchorRadius='4' anchorBgColor='#F8BD19' anchorBorderColor='#F8BD19'>";
            for (int i = 0; i < _xlength; i++)
            {
                xml += "<set y='" + _yArray[i] + "' x='" + _xArray[i] + "' />";
            }

            xml += "</dataset>";

            xml += @"</chart>";

            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(xml);
            xdoc.Save(Server.MapPath("/FusionChartsFlash/FusionCharts.XML"));
            this.divimg1.InnerHtml = FusionCharts.RenderChartHTML("/FusionChartsFlash/Scatter.swf", "/FusionChartsFlash/FusionCharts.XML", "", "MyChart1", "600", "300", false);
        }
         
    }

    public decimal GetMaxValue(string[] vArray)
    {
        decimal max = 0;
        if (vArray.Length > 0)
        {

            foreach (string v in vArray)
            {
                if (!string.IsNullOrEmpty(v))
                {
                    if (Convert.ToDecimal(v) > max)
                    {
                        max = Convert.ToDecimal(v);
                    }
                }
            }
        }
        return max;
    }
}