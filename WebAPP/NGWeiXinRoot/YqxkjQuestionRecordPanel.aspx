<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YqxkjQuestionRecordPanel.aspx.cs" Inherits="NGWeiXinRoot_YqxkjQuestionRecordPanel" %>

<%@ Register src="Control/Footer.ascx" tagname="Footer" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta charset="UTF-8">
	<meta name="viewport" content="width=device-width,initial-scale=1.0,maximum-scale=1.0,minimum-scale=1.0,user-scalable=no" />
    <title>一起学会计吧</title>
	<link href="style/common.css?v=0410" rel="stylesheet">
	<link href="style/index.css?v=0410" rel="stylesheet">
	<link href="style/iconfont.css?v=0410" rel="stylesheet" />
	<script src="js/jquery-3.2.1.min.js"></script>
	<script src="js/iconfont.js"></script>
    <script src="js/layer-v3.0.3/layer/layer.js" type="text/javascript"></script>
    <script language="javascript">
        function to_restart() {
            location.href = location.href + Math.random();
            return false;
        }
        function to_backcourse() {
            if ($("#hidFromFree").val() == "free") {
                location.href = "YqxkjCourseFreeTest.aspx?id=" + $("#hidCourseId").val();
            } else {
                location.href = "YqxkjMemberCoursePlay.aspx?id=" + $("#hidCourseId").val();
            }
            return false;
        }
        function to_show(id) {
            location.href = "YqxkjQuestionView.aspx?memberId=" + $("#hidMemberId").val() + "&fromfree=" + $("#hidFromFree").val() + "&source=" + $("#hidSource").val() + "&courseId=" + $("#hidCourseId").val() + "&curId=" + id + "";
            return false;
        }
    </script>
</head>
<body class="bottom-pd">
    <form id="form1" runat="server">
    <asp:HiddenField ID="hidMemberId" runat="server" />
    <asp:HiddenField ID="hidCourseId" runat="server" />
    <asp:HiddenField ID="hidQuestionType" runat="server" />
    <asp:HiddenField ID="hidAnswer" runat="server" />
    <asp:HiddenField ID="hidSelectAnswer" runat="server" />
    <asp:HiddenField ID="hidAllQuestions" runat="server" />
    <asp:HiddenField ID="hidCurId" runat="server" />
    <asp:HiddenField ID="hidFromFree" runat="server" />
    <asp:HiddenField ID="hidSource" runat="server" />
        <!--答题卡开始-->			
        
		<div class="answer-card">
        <p class="card-title">一、判断题<a>（每小题1分，每错1题倒扣0.5分）</a></p>
			<ul>
				<%=this.ListRecord("判断题")%>
			</ul>
			<p class="card-title">二、单选题<a>（每小题1.5分）</a></p>
			<ul>
				<%=this.ListRecord("单选题")%>
			</ul>
			<p class="card-title">三、多选题<a>（每小题2分）</a></p>
			<ul>
				<%=this.ListRecord("多选题")%>
			</ul>

            <p class="card-title">四、不定项选择题<a>（每小题2分）</a></p>
			<ul>
				<%=this.ListRecord("不定项选择题")%>
			</ul>
			<p class="marks-sum"><!--<i></i>-->总成绩：<b><%=ComputerAll()%></b>分</p>
		</div>
		<!--答题卡结束-->

		<!--底部菜单开始-->
		<uc1:Footer ID="Footer1" runat="server" />
		<!--底部菜单结束-->
    </form>
</body>
</html>

