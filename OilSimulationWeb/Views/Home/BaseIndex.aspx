<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	重庆科技学院油藏仿真实验室
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#cmode").hide();
            $("#citem").hide();

            $("#NavBase").siblings("ul").css("display", "none");
            $("#NavBase").next().css("display", "block");
            $("#NavBase").siblings("ul").children("li").css("display", "none");
            $("#NavBase").next().children("li").slideDown(500);
        });
    </script>
    <div id="MainLayOut" style="width:100%">
        <div style="float:left;margin-right:315px;box-shadow: 0px 1px 3px rgba(0, 0, 0, 0.1);">
            <h2>基础认知</h2>
            <div class="bottom f-richEditorIndex"><p>&nbsp; &nbsp; &nbsp; &nbsp; 本部分主要包括水驱油模拟、渗流方式模拟、弹性不稳定渗流模拟三个模块。<br /><br />&nbsp; &nbsp; &nbsp; &nbsp; 在水驱油模拟实验中，可以开展活塞式水驱油模拟、非活塞式水驱油模拟研究；在渗流方式模拟实验中，可以开展单向渗流模拟、平面径向渗流模拟、球面向心流模拟研究；在弹性不稳定渗流模拟实验中，可以开展稳定水压弹性驱动模拟、封闭弹性驱动模拟等实验。<br /><br />&nbsp; &nbsp; &nbsp; &nbsp; 教学目的及要求：掌握单向流、平面径向流的渗流特征和基本规律；掌握弹性不稳定渗流的特性、基本渗流规律和分析方法；掌握水驱油的影响因素以及油水两相区含水饱和度分布规律。</p>
            </div>
        </div>
        <div style="width: 300px; margin-left: -300px; position: relative;float: right;">
            <div style="padding:5px auto;"><img alt="" width="300px" src="../Images/img11.png" /></div>
            <div style="padding:5px auto;"><img alt="" width="300px" src="../Images/img12.png" /></div>
        </div>
    </div>
</asp:Content>
