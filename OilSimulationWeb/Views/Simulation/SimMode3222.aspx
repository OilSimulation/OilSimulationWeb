<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MainView.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SimMode3222
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <input id="ModeIndex" type="hidden" value="3222" />
    <script type="text/javascript" >
        $(document).ready(function () {
            $("#lineChart").removeClass("hidden");
        });
    </script>
</asp:Content>
