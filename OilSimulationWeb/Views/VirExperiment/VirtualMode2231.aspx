﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MainView.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	VirtualMode2231
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <input id="ModeIndex" type="hidden" value="2231" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("#fGetSpacer").css("display", "inline ");
            $("#fGetContent").css("display", "inline ");
        });
    </script>
</asp:Content>
