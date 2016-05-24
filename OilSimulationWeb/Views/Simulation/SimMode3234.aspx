<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MainView.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SimMode3234
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        //初始化
        $(document).ready(function () {
            parent.postMessage("HideLoading()", "*");
            //重新计算模型
            //$("#edit_rules_button").click(function () {
            //$("#controls_container_top").addClass("hidden");
            //$("#controls_container_top").removeClass("blockLine");  
            //});
        }); 
    </script>
    <input id="DelayLoad" type="hidden" />
    <input id="ModeIndex" type="hidden" value="3234" />
</asp:Content>
