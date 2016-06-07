<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MainView.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	VirtualMode2322
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <input id="ModeIndex" type="hidden" value="2322" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("#fGetSpacer").css("display", "inline ");
            $("#fGetContent").css("display", "inline ");
            $("#fGetLabel").html("采收率:");
        });
    </script>
</asp:Content>
