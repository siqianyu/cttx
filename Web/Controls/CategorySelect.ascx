<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategorySelect.ascx.cs" Inherits="Controls_CategorySelect" %>

<%=selecthtml %>
<asp:HiddenField ID="hfNowValue" ClientIDMode="Static" runat="server" />
<script src="/js/jquery-1.9.0.min.js" type="text/javascript"></script>
<script type="text/javascript">

    if (jQuery("#hfNowValue").val() != undefined && jQuery("#hfNowValue").val() != "") {
    
        jQuery.ajax({
            url: "/Controls/GetCategory.ashx",
            data: { "index": "update", "strPath": jQuery("#hfNowValue").val() },
            dataType: "text",
            success: function (data) {
                jQuery("#cateDIV").append(data);
                //jQuery(".cateSelect").find("option[checked]").attr("checked","checked");
            }
        });
    }


    jQuery("body").on("change",".cateSelect", function () {
        var index = jQuery(".cateSelect").index(jQuery(this));
        jQuery(".cateSelect").each(function (i) {
            if (i > index)
                jQuery(this).remove();
        });

        if (jQuery(this).val() == "-1") {
            GetNowCategoryID();
            return;
        }
        var categoryID = jQuery(this).val();
        jQuery.ajax({
            url: "/Controls/GetCategory.ashx?r="+Math.random(),
            data: { "categoryID": categoryID },
            dataType: "text",
            success: function (data) {
                GetNowCategoryID();
                jQuery("#cateDIV").append(data);
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
        jQuery(".cateSelect").each(function (i) {
            if (jQuery(this).val() != "-1") {
                if (i > 0)
                    categoryValue += "|";
                categoryValue += jQuery(this).val();
            }
        });
        jQuery("#hfNowValue").val(categoryValue);
    }

</script>
