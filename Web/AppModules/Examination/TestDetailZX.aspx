<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestDetailZX.aspx.cs" Inherits="Member_TestDetailZX" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>试卷预览</title>
    <link href="../../../Style/List.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/redmond/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
    <link href="../../../Style/List.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/tableStyle/Common.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/tableStyle/PopUp.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../../js/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="../../js/layer-v1-8-3/layer/layer.min.js" type="text/javascript"></script>
    <style>
    .txt_blank td{height:28px;}

    #identifier-pannel {
        bottom:345px;
        margin-left:512px;
        position:fixed;
        _position:absolute;
        /*left:50%;*/
        right:0px;
        width:120px;
        _top:expression(eval(document.documentElement.scrollTop || document.body.scrollTop) +eval(document.documentElement.clientHeight || document.body.clientHeight) -300 +'px');
        z-index:998;
    }
    </style>

    <script type="text/javascript" src="../style/jpuery1.4.1a.js"></script>

    <script type="text/javascript" src="../js/jquery-1.4.2.js"></script>

    <script type="text/javascript" language="javascript" src="../js/jquery.mouseevent.js"></script>

    <script type="text/javascript">
        function reloadhref() {
            if (confirm("确定要重新答卷吗？") == false) { return false; }
            var url = window.location.href.replace("#","");
            window.location.href = url;
        }
        function showAnswer() {
            //if( checkSelectALL()==false){return false;}
            
            //考试计算得分
            if (confirm("确定要提交吗？") == false) { return false; }
            var i = computerAnswer();
            var zql = parseInt(parseInt(i)*100/g_answers_arr.length);
            var zql2 = zql+"";
            var id=document.getElementById("hidID").value;
            //var cj=Member_TestDetailZX.addTestResult(zql2,id,'<%=this.MemberId %>').value;
            alert("提交成功，你本次答题的成绩为："+zql+"分");
            
            //跳转    
            //location.href="TestDetailZXPage2.aspx?Nid=<%=this.tesSys %>&step1Score="+zql2+"";
        }
        
        //时间到
        function overTimeSave(){
            //考试计算得分
            alert("考试时间已经结束，请按【确定】按钮提交答案！");
            var i = computerAnswer();
            var zql = parseInt(parseInt(i)*100/g_answers_arr.length);
            var zql2 = zql+"";
            var id=document.getElementById("hidID").value;
            var cj=Member_TestDetailZX.addTestResult(zql2,id,'<%=this.MemberId %>').value;
            alert("提交成功，你本次考试的成绩为："+zql2+"分");
            //跳转    
            //location.href="KS2.aspx";
        }
        
        function checkSelectALL(){
            var unSelectIds="";
            var unSelectTitle="";
            $(".jquery_options").each(
                function () {
                    var id = $(this).attr("id");
                    if ($(this).val() == "") {
                        unSelectIds+=id+",";
                        unSelectTitle+= $("#em_"+id).text()+"\n";
                    }
                }
            );
            if(unSelectTitle!=""){
                return confirm("以下题目未作答，是否继续提交：\n"+unSelectTitle);
            }
            return true;
        }

        function getAnswer2(id,flag){
            var ids="";
            if(flag=="radio"){
                $("input[name='radio-"+id+"']").each(function () {
                    if ($(this).attr("checked") == true) {
                       if($(this).val()){ids += $(this).val() + ",";}
                    }
                });
            }else{
                $("input[name='checkbox-"+id+"']").each(function () {
                    if ($(this).attr("checked") == true) {
                        if($(this).val()){ids += $(this).val() + ",";}
                    }
                });
            }
            $("#" + id).val(ids);
            //alert($("#" + id).val());
            return ids;
            
        }

        function computerAnswer() {
            var total = 0;
            $(".jquery_options").each(
                function () {
                    var id = $(this).attr("id");
                    if ($(this).val() == get_answer(id)) {
                        total++;
                    }
                }
            );
            //alert(total);
            return total;
        }
        
        //倒计时
        var global_flag_time=1;
        function time(){
	        var maxtime;
	        var h=document.getElementById("h");//总时间(秒)
	        var m=document.getElementById("m");
	        var s=document.getElementById("s");
	        if(global_flag_time == 1){
		        maxtime = h.value;
		        m.innerHTML = Math.floor(maxtime/60); 
		        s.innerHTML = Math.floor(maxtime%60);   
		        h.value = h.value - 1;
		        document.getElementById("B1").innerHTML=m.innerHTML;
		        setTimeout("time()",1000)
	        }
	        else{
		        return;
		        }
	        if(h.value<=0){
	            if(parseInt(h.value)<-1){location.href="ks2.aspx";}
                overTimeSave();
                global_flag_time=0;
                location.href="ks2.aspx";
	        }
        }
    </script>

</head>
<body style="font-size:14px; line-height:200%;" >
    <form id="form1" runat="server">
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:HiddenField ID="hidAnswers" runat="server" />
        <asp:HiddenField ID="hidRndQuestions" runat="server" />
         <asp:HiddenField ID="hidTaskId" runat="server" />
         <asp:HiddenField ID="hidCourseId" runat="server" />
        <div class="B C">
            <div class="wrapper kclist">
                <div class="txt_main_box" id="divQuestion" style="text-align: left; font-size: 12px;
                    color: Black; font-family: '微软雅黑'">
                    <p class="from_p">
                        <asp:Literal runat="server" ID="ltTime" Visible="false"></asp:Literal>
                       <div style="color:#014693; font-size:14px; margin:10px; text-align:center; display:none" id="div_show_total"></div>
                    </p>
                    <div class="txt_blank" id="div_PD">
                        <p style="display: ">
                            <strong style="font-size: 14px;">判断题</strong></p>
                        <%=CreateCommon("判断题")%>
                    </div>
                    <!--1-->
                    <div class="txt_blank" id="div_DX">
                        <p style="display: ">
                            <strong style="font-size: 14px;">单项选择题</strong></p>
                        <%=CreateCommon("单选题")%>
                    </div>
                    <!--1-->
                    <div class="txt_blank" id="div_FX">
                        <p style="display: ">
                            <strong style="font-size: 14px;">多项选择题</strong></p>
                        <%=CreateCommon("多选题")%>
                    </div>
                    <!--1-->
                    <div class="txt_blank" id="div_AL">
                        <p style="display: ">
                            <strong style="font-size: 14px;">案例题</strong></p>
                        <%=this.CreateAL() %>
                    </div>
                    <div>
                    </div>
                    <div class="btn_box_txt" style="display:none">
                        <input type="button" value=" 提 交 " style="height: 30px;" onclick="return showAnswer()" />&nbsp;&nbsp;
                    </div>
                </div>
                <div class="HackBox">
                </div>
            </div>
        </div>
        
        <asp:HiddenField ID="hidCourseQuestions" runat="server" />
        <asp:HiddenField ID="hidZYQuestions" runat="server" />
        <asp:HiddenField ID="hidOtherQuestions" runat="server" />
       
    </form>
</body>
</html>
<script language="javascript">
var g_answers="<%=this.allAnswers.TrimEnd('$').TrimStart('$') %>";
var g_answers_arr = g_answers.split('$');
function get_answer(id){
    for(var i=0;i<g_answers_arr.length;i++){
        if(g_answers_arr[i].indexOf(id)>-1){return g_answers_arr[i].split('|')[1];}
    }
}

(function show_total_info(){
    var t = parseInt(<%=this.pd_num %>)+parseInt(<%=this.dx_num %>)+parseInt(<%=this.fx_num %>)+parseInt(<%=this.al_num %>);
    var s="本次考试题目总数为（"+t+"），其中：判断题（<%=this.pd_num %>）、单项选择题（<%=this.dx_num %>）、多项选择题（<%=this.fx_num %>）";
    if("<%=this.al_num %>"!="0"){s+="、案例题（<%=this.al_num %>）"}
    $("#div_show_total").html(s).show();
})();

</script>
