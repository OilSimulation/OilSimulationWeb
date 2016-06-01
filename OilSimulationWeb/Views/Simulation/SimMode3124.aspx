<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MainView.Master" Inherits="System.Web.Mvc.ViewPage" %>
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
    //创建网格
    function CreateGrid(row,col) {
        var divWrapper = $("#wrapper");
        divWrapper.css("width", 1 + col * (5 + 1) + "px");
        divWrapper.css("height", 1+ row * (5 + 1) + "px");
        divWrapper.html("");
        var tab = $("<table></table>").appendTo(divWrapper);
        tab.addClass("tab"); 
        tab.attr("cellspacing", "0");
        tab.attr("cellpadding", "0");
        tab.attr("rowspacing", "0");
        tab.attr("rowpadding", "0");
        for (var i = row - 1; i >= 0; i--) {
            var tr = $("<tr></tr>").appendTo(tab);
            for (var j = 0; j < col; j++) {
                var td = $("<td></td>").appendTo(tr);
                td.attr("title", "行" + (i + 1) + "列" + (j + 1));
                td.attr("row", i);
                td.attr("col", j);
                td.addClass("td");
                td.click(function () { BoxClick(this); });
            }
        }
    }
      
    //修改油坐标
    function UpdateWellPoint() {
        parent.postMessage("ShowLoading()", "*");
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
            success: function (result) { LoadWebGLData(0) ; }
        };
        $.ajax(option);

    }
    //初始化
    $(document).ready(function () {
        CreateGrid(100, 100);
        $("#wrapper").addClass("ShowGrid");
        parent.postMessage("HideLoading()", "*");
        $(".ui-helper-hidden-accessible").change(function () {
            var $selectedID = $("input[name='transition']:checked").attr("id");
            if ($selectedID == "transition_0") {
                wellType = 0;
                $("label[for='transition_0']").addClass("ui-state-active").attr("aria-pressed", "true");
                $("label[for='transition_2']").removeClass("ui-state-active").attr("aria-pressed", "false");
            }
            else if ($selectedID == "transition_2") {
                wellType = 1;
                $("label[for='transition_0']").removeClass("ui-state-active").attr("aria-pressed", "false");
                $("label[for='transition_2']").addClass("ui-state-active").attr("aria-pressed", "true");
            }
        });
        //重新计算模型
        $("#edit_rules_button").click(function () {
            $("#controls_container_top").addClass("hidden");
            $("#controls_container_top").removeClass("blockLine");
            $("#wrapper").addClass("hidden");
            UpdateWellPoint();
        });
    }); 
</script>
    <input id="ModeIndex" type="hidden" value="3124" />
    <input id="DelayLoad" type="hidden" />
    <div id="controls_container_top" class="blockLine">
		<div id="controls_innercontainer"> 
            <div class="controls">
		        <legend>设置面板</legend> 
		        <ul>
                    <li> 
						<div class="control">
							<span id="transition" class="ui-buttonset">
							<input type="radio" id="transition_0" name="transition" checked="checked" class="ui-helper-hidden-accessible" /><label for="transition_0" class="ui-button ui-widget ui-state-default ui-button-text-only ui-corner-left ui-state-active" role="button" aria-disabled="false" aria-pressed="true"><span class="ui-button-text">油井</span></label>
							<!--input type="radio" id="transition_1" name="transition" /><label for="transition_1">Fade</label-->
							<input type="radio" id="transition_2" name="transition" class="ui-helper-hidden-accessible" /><label for="transition_2" class="ui-button ui-widget ui-state-default ui-button-text-only ui-corner-right" role="button" aria-disabled="false" aria-pressed="false"><span class="ui-button-text">水井</span></label>
							</span>
						</div>
					</li>
                    <li></li>
                    <li>
                        <div class="control">
							<button id="edit_rules_button" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" role="button" aria-disabled="false"><span class="ui-button-text">计算模型</span></button>
					    </div>
                    </li> 
                    <li></li> 
		        </ul>
	        </div>
        </div> 
    </div> 
    <div id="wrapper" class="wrapper"> </div>
</asp:Content>
