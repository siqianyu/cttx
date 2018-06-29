//--------------layer3.0新方法_start------------------
//(1)当前打开层的id;
var global_cur_layer_v3_id = null;

//(2)open
//open_div_panel_v3("标题","Test.aspx");
//open_div_panel_v3("标题","Test.aspx","['900px', '600px']");
//open_div_panel_v3("标题","Test.aspx","['300px', '300px']",null,0,true);
function open_div_panel_v3(_title, _url, _size, _offset, _closebtn, _tomax, _maxmin) {
    if (!_size) { _size = [$(window).width() - 20 + 'px', $(window).height() - 20 + 'px']; }
    if (!_offset) { _offset = ['0px', '']; }
    if (!_closebtn) { _closebtn = 1; }
    if (!_maxmin) { _maxmin = true; }
    global_cur_layer_v3_id = layer.open({
        type: 2,
        title: _title,  //标题
        content: _url,  //网页地址
        fixed: false,   //不固定
        closeBtn: _closebtn,
        maxmin: _maxmin,
        area: _size,
        cancel: function (index, layero) {
            if (window.grid_search) {
                grid_search(); //关闭时调用
            }
        }
    });
    if (_tomax) {
        layer.full(global_cur_layer_v3_id);    //最大化
    }
    //alert(global_cur_layer_v3_id);
}

//(3)close
function close_div_panel_v3() {
    if (global_cur_layer_v3_id) {
        layer.close(global_cur_layer_v3_id);
    }
}

//--------------layer3.0新方法_end------------------