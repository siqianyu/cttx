function getCityList(parentid) {
    var dataInfo = '';
    $.ajax({
        type: "post",
        url: "/ServiceInterface/CityHandler.ashx",
        data: { flag: "list", parentid: parentid },
        dataType: "text",
        async: false,
        success: function (data) {
            dataInfo = data;
        }
    });
    return dataInfo;
}

function getAllCity() {
    var dataInfo = eval(getCityList('1000001'));
    var reg = /^\s+|\s+$/g;
    dataInfo.sort(function (a, b) {
        var af = a.city.replace(reg, "").charAt(0);
        var bf = b.city.replace(reg, "").charAt(0);
        if (af.localeCompare(bf) > 0) {
            return 1;
        } else if (af.localeCompare(bf) < 0) {
            return -1;
        } else {
            return 0;
        }
    });
    var outputHtml = '';
    var nowLetter = '';
    for (var i = 0; i < dataInfo.length; i++) {
        var tempLetter = GetLetter(dataInfo[i].city.replace(reg, "").charAt(0));
        if (nowLetter != tempLetter) {
            nowLetter = tempLetter;
            if (i != 0)
                outputHtml += '</ul>';

            outputHtml += '<p class="CityLetter Sizing">' + nowLetter + '</p><ul class="CityList">';
        }

        outputHtml += '<li class="Sizing" onclick="location.href=\'ChangeCity.html?aid=' + dataInfo[i].id + '&rootname=' + escape(dataInfo[i].city) + '&aname=' + escape(dataInfo[i].city) + '\'">' + dataInfo[i].city + '</li>';
    }
    outputHtml += '</ul>';
    $('#cityDiv').html(outputHtml);
}

function getSubCity() {
    var aid = getQueryString('aid');
    var aname = unescape(getQueryString('aname'));
    var rootname = unescape(getQueryString('rootname'));
    if (rootname == null || rootname == "null") { rootname = "广东省"; }
    if (aid) {
        var dataInfo = eval(getCityList(aid));
        if (dataInfo.length == 0) {
            location.href = 'FillResume.html?aid=' + aid + '&aname=' + escape(rootname);
        } else {
            var reg = /^\s+|\s+$/g;
            dataInfo.sort(function (a, b) {
                var af = a.city.replace(reg, "").charAt(0);
                var bf = b.city.replace(reg, "").charAt(0);
                if (af.localeCompare(bf) > 0) {
                    return 1;
                } else if (af.localeCompare(bf) < 0) {
                    return -1;
                } else {
                    return 0;
                }
            });
            var outputHtml = '';
            for (var i = 0; i < dataInfo.length; i++) {
                outputHtml += '<li class="Sizing" onclick="location.href=\'ChangeCity.html?aid=' + dataInfo[i].id + '&rootname=' + escape(rootname + dataInfo[i].city) + '&aname=' + escape(dataInfo[i].city) + '\'">' + dataInfo[i].city + '</li>';
            }

            $('#rootCity').html(aname);
            $('#cityUl').html(outputHtml);
        }
    }
}

function getCityStr(city) {
    if (!city) {
        city = $('#cuCity').html();
    }

    if (city) {
        $.ajax({
            type: "post",
            url: "/ServiceInterface/CityHandler.ashx",
            data: { flag: "citystr", city: city },
            dataType: "text",
            async: false,
            success: function (data) {
                var dataInfo = eval(data);
                if (dataInfo.length > 0) {
                    location.href = 'ChangeCity.html?aid=' + dataInfo[0].id + '&aname=' + escape(dataInfo[0].city);
                }
            }
        });
    }
}

function GetLetter(str) {
    if (str.localeCompare("吖") < 0) {
        return str;
    }
    if (str.localeCompare("八") < 0) {
        return "A";
    }

    if (str.localeCompare("嚓") < 0) {
        return "B";
    }

    if (str.localeCompare("咑") < 0) {
        return "C";
    }
    if (str.localeCompare("妸") < 0) {
        return "D";
    }
    if (str.localeCompare("发") < 0) {
        return "E";
    }
    if (str.localeCompare("旮") < 0) {
        return "F";
    }
    if (str.localeCompare("铪") < 0) {
        return "G";
    }
    if (str.localeCompare("讥") < 0) {
        return "H";
    }
    if (str.localeCompare("咔") < 0) {
        return "J";
    }
    if (str.localeCompare("垃") < 0) {
        return "K";
    }
    if (str.localeCompare("呒") < 0) {
        return "L";
    }
    if (str.localeCompare("拏") < 0) {
        return "M";
    }
    if (str.localeCompare("噢") < 0) {
        return "N";
    }
    if (str.localeCompare("妑") < 0) {
        return "O";
    }
    if (str.localeCompare("七") < 0) {
        return "P";
    }
    if (str.localeCompare("亽") < 0) {
        return "Q";
    }
    if (str.localeCompare("仨") < 0) {
        return "R";
    }
    if (str.localeCompare("他") < 0) {
        return "S";
    }
    if (str.localeCompare("哇") < 0) {
        return "T";
    }
    if (str.localeCompare("夕") < 0) {
        return "W";
    }
    if (str.localeCompare("丫") < 0) {
        return "X";
    }
    if (str.localeCompare("帀") < 0) {
        return "Y";
    }
    if (str.localeCompare("咗") < 0) {
        return "Z";
    }
    return str;
}