<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MainView.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    SimMode3124

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
    var wellType = 0; //0生产井red,1注水井blue
    var wellP = []; //生产井列表(X:Y)
    var wellI = []; //注水井列表
    function BoxClick(x, y) {
        var box = document.getElementById('id' + x + y);
        var point = {};
        //point.push(x);
        //point.push(y); 
        point.x = x;
        point.y = y;
        if (wellType == 0) {
            if (box.style.backgroundColor == '#ff0000') {
                //取消该生产井
                box.style.backgroundColor = '#ff00ff'; //默认颜色
                wellP.remove(point); 
            }
            else {
                if (box.style.backgroundColor == '#0000ff') {
                    //取消注水井
                    wellI.remove(point);
                }
                //增加生产井
                box.style.backgroundColor = '#ff0000'; //生产井颜色
                wellP.push(point);
            }
        }
        else {

            if (box.style.backgroundColor == '#0000ff') {
                //取消该注水井
                box.style.backgroundColor = '#ff00ff'; //默认颜色
                wellI.remove(point);
            }
            else {
                if (box.style.backgroundColor == '#ff0000') {
                    //取消注水井生产井
                    wellP.remove(point);
                }
                //增加注水井
                box.style.backgroundColor = '#0000ff'; //注水井颜色
                wellI.push(point);
            }

        }


        //box.style.backgroundColor = '#ff0000';
    }

    function CreateDiv() {
        var div = document.getElementById('wrapper');

        var divCol = "";
        for (var i = 0; i < 100; i++) {
            for (var j = 0; j < 100; j++) {

                divCol += '<div title="行' + (i + 1) + '列' + (j + 1) + '" class="box" id="id' + i + j + '"    onclick="BoxClick(' + i + ',' + j + ')" ></div>';
            }
        }
        div.innerHTML = divCol;
    }
    //0生产井,1注水井
    function SetWellType(type) {
        wellType = type;
    }
    //修改油坐标
    function UpdateWellPoint() {
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

        //var mIndex = $("#ModeIndex").val();
        //$.post(url:'<%:Url.Action("UpdateWellPoint","Business") %>', JSON.stringify(jsonData),
        //            function (data) {
        //                ExcutBatCommand();
        //            }); 
           var option = {
                url: '<%:Url.Action("UpdateWellPoint","Business") %>',
                type: 'POST',
                data:JSON.stringify(jsonData),
                dataType: 'html',
                contentType: 'application/json',
                success: function (result) { alert(result); }
                }; 
            $.ajax(option);
    }
    window.onload = CreateDiv;
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
