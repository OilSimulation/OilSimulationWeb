<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	重庆科技学院油藏仿真实验室
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#cmode").hide();
            $("#citem").hide();

            $("#NavSim").siblings("ul").css("display", "none");
            $("#NavSim").next().css("display", "block");
            $("#NavSim").siblings("ul").children("li").css("display", "none");
            $("#NavSim").next().children("li").slideDown(500);
        });
    </script>
    <div id="MainLayOut" style="width:100%">
        <div style="float:left;margin-right:315px;box-shadow: 0px 1px 3px rgba(0, 0, 0, 0.1);">
            <h2>仿真实训</h2>
            <div class="bottom f-richEditorIndex"><p>&nbsp; &nbsp; &nbsp; &nbsp; 本部分主要包括井网井距方案设计与开发效果预测、注采系统方案设计与开发效果预测两个模块。<br /><br />&nbsp; &nbsp; &nbsp; &nbsp; 在井网井距方案设计与开发效果预测模块中，可以设计五点、七点、九点等不同类型井网以及不同井距的方案，模拟开发过程、预测开发指标；在注采系统方案设计与开发效果预测模块中，可以开展不同注水时机、不同注采比、不同最大井底注入压力、不同最低井底流压等条件下的方案设计与开发效果预测实验。<br /><br />&nbsp; &nbsp; &nbsp; &nbsp; 教学目的及要求：了解不同注水时机的特点及选择注水时机的基本原则；理解注水方式及各种面积注水的井网特征；掌握开发井网的部署原则；掌握井网密度的确定方法。</p>
            </div>
        </div>
        <div style="width: 300px; margin-left: -300px; position: relative;float: right;">
            <div style="padding:5px auto;"><img alt="" width="300px" src="../../Images/img8.png" /></div> 
        </div>
    </div>
</asp:Content>

