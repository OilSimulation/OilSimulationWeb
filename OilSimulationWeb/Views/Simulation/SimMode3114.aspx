<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MainView.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    SimMode3114
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript" > 
    $(document).ready(function () {
        $("#wellDist").unbind('click').click(function () {
            var jsonData = {};
            jsonData.Mode = 3114;
            jsonData.Step = $("#wellDist").val(); //距离
            CommonDataUpdate(jsonData);
        });
    });
</script>
    <input id="DelayLoad" type="hidden" />
    <input id="ModeIndex" type="hidden" value="3114" />
    <input id="wellDist" type="hidden" value="" />
</asp:Content>
