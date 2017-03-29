<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	试用期结束
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#LeftPanel").hide();
            $("#RightPanl").width($(".greybg .main").width());
            $("#cmode").hide();
            $("#citem").hide();
            });
     </script>

     
    <div style="margin:5px auto; text-align:center;">
        <img alt=""  src="../Images/img2.jpg" />
        <h2>很遗憾您的试用期已经结束！</h2>
    </div>
    


</asp:Content>
