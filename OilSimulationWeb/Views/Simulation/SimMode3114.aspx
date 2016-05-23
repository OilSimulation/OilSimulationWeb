<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MainView.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    SimMode3114
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript" >
    function UpdateWellDist() {
        var wellDist = $("#wellDist").val();
        var jsonData = {};
        jsonData.Mode = 3114;
        jsonData.Step = wellDist; //距离
        var option = {
            url: '<%:Url.Action("UpdateWellDistance","Business") %>',
            type: 'POST',
            data: JSON.stringify(jsonData),
            dataType: 'html',
            contentType: 'application/json',
            success: function (result) { ExcutBatCommand(); }
        };
        $.ajax(option);

    }
    $(document).ready(function () {
        $("#wellDist").unbind('click').click(function () {
            UpdateWellDist();
        });
    });
</script>
    <input id="DelayLoad" type="hidden" />
    <input id="ModeIndex" type="hidden" value="3114" />
    <input id="wellDist" type="hidden" value="" />
</asp:Content>
