<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	重庆科技学院油藏仿真实验室
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#cmode").hide();
            $("#citem").hide();

            $("#NavInv").siblings("ul").css("display", "none");
            $("#NavInv").next().css("display", "block");
            $("#NavInv").siblings("ul").children("li").css("display", "none");
            $("#NavInv").next().children("li").slideDown(500);
        });
    </script>
    <div id="MainLayOut" style="width:100%">
        <div style="float:left;margin-right:315px;box-shadow: 0px 1px 3px rgba(0, 0, 0, 0.1);">
            <h2>创新实践</h2>
            <div class="bottom f-richEditorIndex"><p>&nbsp; &nbsp; &nbsp; &nbsp; 本部分主要包括油藏开发方案实践、气藏开发方案实践两个模块。<br /><br />&nbsp; &nbsp; &nbsp; &nbsp; 在油藏开发方案实践模块中，可以依据虚拟系统提供的均质油藏及实际油藏的基础数据，设计油藏开发方案并预测开发效果；在气藏开发方案实践模块中，可以依据虚拟系统提供的均质气藏及实际气藏的基础数据，设计气藏开发方案并预测开发效果。<br /><br />&nbsp; &nbsp; &nbsp; &nbsp; 教学目的及要求：要求学生进一步加深对所学油藏工程基本理论与方法的理解和掌握；具备独立开展油藏工程设计的能力；按照行业规范，能够独立完成油藏工程设计报告的撰写。</p>
            </div>
        </div>
        <div style="width: 300px; margin-left: -300px; position: relative;float: right;">
            <div style="padding:5px auto;"><img alt="" width="300px" src="../Images/img10.jpg" /></div> 
            <div style="padding:5px auto;"><img alt="" width="300px" src="../Images/img15.jpg" /></div> 
        </div>
    </div>
</asp:Content>
