<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MainView.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    SimMode3124

    <script type="text/javascript">
//    function BoxClick(id) {
//    }

//    function CreateDiv() {
//        var div = document.getElementById('wrapper');

//        var divCol = "";
//        for (var i = 0; i < 100; i++) {
//            for (var j = 0; j < 100; j++) {

//                divCol += '<div title="行' + (i + 1) + '列' + (j + 1) + '" class="box" id="id' + i + j + '"    onclick="BoxClick(\'id' + i + j + '\')" ></div>';
//            }
//        }
//        div.innerHTML = divCol;
//    }
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <input id="ModeIndex" type="hidden" value="3124" />
</asp:Content>
