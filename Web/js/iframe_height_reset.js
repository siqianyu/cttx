/*iframe页面必须引用,自动调整高度*/
function set_parent_iframeheight() {
    var h = $(document).height();
    if (!h || h < 500) { h = 500; }
    //alert(h);
    if (window.parent.setHeight) {
        window.parent.setHeight(h + 20);
    }
}

$(document).ready(
    function () {
        set_parent_iframeheight();
    }
);

window.setTimeout("set_parent_iframeheight()", 3000);