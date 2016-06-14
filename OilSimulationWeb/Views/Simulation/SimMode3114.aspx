<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MainView.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    SimMode3114
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript" >
    $(document).ready(function () {
        $("#lineChart").removeClass("hidden");
        parent.postMessage("HideLoading()", "*");
        ShowCenterPosition($("#controls_container_top"));
        $("#edit_rules_button").click(function () {
            var userData = $("#UserData").val(); //
            //判断数据有效性
            if (false) {
                return;
            }
            var jsonData = {};
            jsonData.Mode = 3114;
            jsonData.Para = userData; //距离
            CommonDataUpdate(jsonData);
        });
    });
</script>
    <input id="DelayLoad" type="hidden" />
    <input id="ModeIndex" type="hidden" value="3114" />
    <input id="wellDist" type="hidden" value="" />
    <div id="controls_container_top" class="blockLine">
		<div id="controls_innercontainer"> 
            <div class="controls">
		        <legend></legend> 
		        <ul>
                    <li> 
                        <div class="label">井距设置:</div>
						<div class="control"> 
                            <input id="UserData" type="text" />
						</div>
					</li>
					<li class="spacer"></li>
                    <li class="explanation_parameter" style="display: block;">
						<span class="explanation">平面网格100*100，每格为5，请在上方输入合适在值!</span>
					</li>
					<li class="spacer"></li>
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
</asp:Content>
