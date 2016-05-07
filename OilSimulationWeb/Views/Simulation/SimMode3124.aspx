<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MainView.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    SimMode3124

    <script type="text/javascript">
        var wellType = 0; //0生产井red,1注水井blue
        var wellP = [];//生产井列表(X:Y)
        var wellI = [];//注水井列表
        function BoxClick(x, y) {
            var box = document.getElementById('id' + i + j);
            if (wellType == 0) {
                if (box.style.backgroundColor == '#ff0000') {
                    //取消该生产井
                    box.style.backgroundColor = '#ff00ff'; //默认颜色
                    wellP.remove(i + ':' + j);

                }
                else {
                    if (box.style.backgroundColor == '#0000ff') {
                        //取消注水井
                        wellI.remove(i + ':' + j);    
                    }
                    //增加生产井
                    box.style.backgroundColor = '#ff0000'; //生产井颜色
                    wellP.push(i + ':' + j);
                }
            }
            else {

                if (box.style.backgroundColor == '#0000ff') {
                    //取消该注水井
                    box.style.backgroundColor = '#ff00ff'; //默认颜色
                    wellI.remove(i + ':' + j);   
                }
                else {
                    if (box.style.backgroundColor == '#ff0000') {
                        //取消注水井生产井
                        wellP.remove(i + ':' + j);
                    }
                    //增加注水井
                    box.style.backgroundColor = '#0000ff'; //注水井颜色
                    wellI.push(i + ':' + j);
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
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <input id="ModeIndex" type="hidden" value="3124" />
    <table border="1" style="text-align:center;color:White" cellspacing="0" cellpadding="0" width="150" >
	<tr>
		<td style="background-color:Red" onclick="SetWellType(0)">生产井</td><td style="background-color:Blue" onclick="SetWellType(1)">注水井</td>
	</tr>
    </table>
    <div id="wrapper" class="wrapper"> </div>
</asp:Content>
