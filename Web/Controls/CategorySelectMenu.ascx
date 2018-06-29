<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategorySelectMenu.ascx.cs" Inherits="Controls_CategorySelect" %>

<%=selecthtml %>
<asp:HiddenField ID="hfNowValue" ClientIDMode="Static" runat="server" />
<script src="/js/jquery-1.9.0.min.js" type="text/javascript"></script>
<script type="text/javascript">

    if ($("#hfNowValue").val() != undefined && $("#hfNowValue").val() != "") {

        $.ajax({
            url: "/Controls/GetCategoryMenu.ashx",
            data: { "index": "update", "strPath": $("#hfNowValue").val() },
            dataType: "text",
            success: function (data) {
                $("#cateDIV").append(data);
                //$(".cateSelect").find("option[checked]").attr("checked","checked");
            }
        });
    }


    $("body").on("change",".cateSelect", function () {
        var index = $(".cateSelect").index($(this));
        $(".cateSelect").each(function (i) {
            if (i > index)
                $(this).remove();
        });

        if ($(this).val() == "-1") {
            GetNowCategoryID();
            return;
        }
        var categoryID = $(this).val();
        $.ajax({
            url: "/Controls/GetCategoryMenu.ashx?r="+Math.random(),
            data: { "categoryID": categoryID },
            dataType: "text",
            success: function (data) {
                GetNowCategoryID();
                $("#cateDIV").append(data);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(XMLHttpRequest.status);
                alert(XMLHttpRequest.readyState);
                alert(textStatus);
            }
        });
    });

    function GetNowCategoryID() {
        var categoryValue = "";
        $(".cateSelect").each(function (i) {
            if ($(this).val() != "-1") {
                if (i > 0)
                    categoryValue += "|";
                categoryValue += $(this).val();
            }
        });
        $("#hfNowValue").val(categoryValue);
    }

</script>
