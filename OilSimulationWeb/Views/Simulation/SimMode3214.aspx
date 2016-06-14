<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MainView.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SimMode3214
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript"> 
        //初始化
        $(document).ready(function () {
            $("#lineChart").removeClass("hidden");
            parent.postMessage("HideLoading()", "*");
            ShowCenterPosition($("#controls_container_top"));
            $("#edit_rules_button").click(function () {
                var userData = $("#UserData").val(); //
                var iData = parseInt(userData);
                if (iData < 0 || iData > 60) {
                    $("#error").text("请输入0-60之前的整数！");
                    return;
                }

                var jsonData = {};
                jsonData.Mode = 3214;
                jsonData.Para = userData; //距离
                CommonDataUpdate(jsonData);
            });
        }); 
    </script>
    <input id="DelayLoad" type="hidden" />
    <input id="ModeIndex" type="hidden" value="3214" />
        <div id="controls_container_top" class="blockLine">
		<div id="controls_innercontainer"> 
            <div class="controls">
		        <legend></legend> 
		        <ul>
                    <li> 
                        <div class="label">注水时机:</div>
						<div class="control"> 
                            <input id="UserData" type="text" />
						</div>
                        <div class="label" id="error"></div>
					</li>
					<li class="spacer"></li>
                    <li class="explanation_parameter" style="display: block;">
						<span class="explanation">请在上方输入注水时机范围1-60，单位月!</span>
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
