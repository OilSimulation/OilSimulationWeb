<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MainView.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	BaseModeOne
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server"> 
    <script type="text/javascript">
        var modeIndex = 1;
    </script>
    <div id="controls_container_top"  style="display: inline-block; ">
				<ul> 
	<%--				<li>
						<div id="button_toolbar">
							<button id="button_reset" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-icon-only" role="button" aria-disabled="false" title="初始化"><span class="ui-button-icon-primary ui-icon ui-icon-seek-start"></span><span class="ui-button-text">初始化</span></button>
							<button id="button_play" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-icon-only" role="button" aria-disabled="false" title="播放"><span class="ui-button-icon-primary ui-icon ui-icon-play"></span><span class="ui-button-text">播放</span></button>
							<button id="button_backward" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-icon-only" role="button" aria-disabled="false" title="上一步"><span class="ui-button-icon-primary ui-icon ui-icon-seek-prev"></span><span class="ui-button-text">上一步</span></button>
							<button id="button_forward" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-icon-only" role="button" aria-disabled="false" title="下一步"><span class="ui-button-icon-primary ui-icon ui-icon-seek-next"></span><span class="ui-button-text">下一步</span></button>
						</div>
					</li>--%>

					<li>
						<div class="mini_label">井距：</div>
						<button id="btn100" onclick="changeWellDistance(100)" class="ui-button ui-widget ui-state-default ui-corner-all " role="button" aria-disabled="false" title="100m"><span class="ui-button-text">100m</span></button>
						<button id="btn200" onclick="changeWellDistance(200)" class="ui-button ui-widget ui-state-default ui-corner-all " role="button" aria-disabled="false" title="100m"><span class="ui-button-text">200m</span></button>
						<button id="btn300" onclick="changeWellDistance(300)" class="ui-button ui-widget ui-state-default ui-corner-all " role="button" aria-disabled="false" title="100m"><span class="ui-button-text">300m</span></button>
                        <input type="text" value="400" style="width:50px"/>
						<button id="btnOK" onclick="changeWellDistance(100)" class="ui-button ui-widget ui-state-default ui-corner-all" role="button" aria-disabled="false" title="确定"><span class="ui-button-text">确定</span></button>
					</li> 
					
					
					<li class="spacer"></li>

				</ul>
    </div>
</asp:Content>
