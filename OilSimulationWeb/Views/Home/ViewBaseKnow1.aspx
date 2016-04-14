<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/View3DMasterPage.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div>
    <% Html.RenderPartial("View3DControl"); %>
</div>
</asp:Content>
