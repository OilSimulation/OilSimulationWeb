﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MainView.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    SimMode3124

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
    var wellType = 0; //0生产井red,1注水井blue
    var wellP = []; //生产井列表(X:Y)
    var wellI = []; //注水井列表 

    function BoxClick(curItem) {
        var item = $(curItem);
        var row = item.attr("row");
        var col = item.attr("col");
        var point = {};
        point.x = row;
        point.y = col;
        if (wellType == 0) {
            if (item.hasClass("red")) {
                //取消该生产井 
                item.removeClass("red");
                wellP.remove(point);
            }
            else {
                if (item.hasClass("blue")) {
                    //取消注水井
                    wellI.remove(point);
                }
                //增加生产井 
                item.removeClass("blue");
                item.addClass("red");
                wellP.push(point);
            }
        }
        else {

            if (item.hasClass("blue")) {
                //取消该注水井 
                item.removeClass("blue");
                wellI.remove(point);
            }
            else {
                if (item.hasClass("red")) {
                    //取消注水井生产井
                    wellP.remove(point);
                }
                //增加注水井 
                item.removeClass("red");
                item.addClass("blue");
                wellI.push(point);
            }

        }

    }

    function CreateDiv(row,col) {
        var divWrapper = $("#wrapper");
        divWrapper.html("");
        var divCol = "";
        for (var i = 0; i < row; i++) {
            for (var j = 0; j < col; j++) { 
                var div = $("<div></div>").appendTo(divWrapper);
                div.attr("title", "行" + (i + 1) + "列" + (j + 1)); 
                div.attr("row",  i);
                div.attr("col",  j);
                div.addClass("box");
                div.css("width","5px");
                div.css("height", "5px");
                div.click(function () { BoxClick(this); }); 
            }
        }
        //divWrapper.html(divCol);
    }
    //0生产井,1注水井
    function SetWellType(type) {
        wellType = type;
    }
    //修改油坐标
    function UpdateWellPoint() {
//        //简单JSON结构传值
//        $.post('<%:Url.Action("UpdateWellPoint2","Business") %>', { Mode: 1 },
//                    function (data) {
//                       //ExcutBatCommand();
//                    }); 
//                    return;
        
        //构造JSON
        var jsonData = {};
        jsonData.modelId = 3124;
        jsonData.P = wellP;
        jsonData.I = wellI;
        //复杂结构JSON传值方法
        var option = {
            url: '<%:Url.Action("UpdateWellPoint","Business") %>',
            type: 'POST',
            data: JSON.stringify(jsonData),
            dataType: 'html',
            contentType: 'application/json',
            success: function (result) { ExcutBatCommand(); }
        };
        $.ajax(option);

    }
    $(document).ready(function () { CreateDiv(100,100);}); 
</script>
    <input id="ModeIndex" type="hidden" value="3124" />
    <table border="1" style="text-align:center;color:White" cellspacing="0" cellpadding="0" width="150" >
	<tr>
		<td style="background-color:Red" onclick="SetWellType(0)">生产井</td><td style="background-color:Blue" onclick="SetWellType(1)">注水井</td>
        <td><button onclick="UpdateWellPoint()">确定</button></td>
	</tr>
    </table>
    <div id="wrapper" class="wrapper"> </div>
</asp:Content>
