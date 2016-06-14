<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MainView.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	VirtualMode2134
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        //初始化
        $(document).ready(function () {
            parent.postMessage("HideLoading()", "*");
            ShowCenterPosition($("#controls_container_top"));
            $("#edit_rules_button").click(function () {
                var userData = $("#UserData").val(); 
                //判断数据有效性
                if (false) {
                    return;
                }
                var jsonData = {};
                jsonData.Mode = 2134;
                jsonData.Para = userData; //距离
                CommonDataUpdate(jsonData);
            });
        }); 
    </script>
    <input id="ModeIndex" type="hidden" value="2134" />
    <input id="DelayLoad" type="hidden" />
        <div id="controls_container_top" class="blockLine">
		<div id="controls_innercontainer"> 
            <div class="controls">
		        <legend></legend> 
		        <ul>
                    <li> 
                        <div class="label">油水粘度比:</div>
						<div class="control"> 
                            <input id="UserData" type="text" />
						</div>
					</li>
					<li class="spacer"></li>
                    <li class="explanation_parameter" style="display: block;">
						<span class="explanation">水定为0.5，油取值范围为0.5~300. 请在上方输入油水粘度比值范围1-600!</span>
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
