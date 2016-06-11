<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MainView.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SimMode3231
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <input id="ModeIndex" type="hidden" value="3231" />
    <script type="text/javascript" >
        $(document).ready(function () {
            $("#lineChart").removeClass("hidden");
        });
    </script>
</asp:Content>
