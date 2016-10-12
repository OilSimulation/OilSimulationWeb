<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	重庆科技学院油藏仿真实验室
</asp:Content>

<asp:Content ID="Index" ContentPlaceHolderID="MainContent" runat="server">  
    <script type="text/javascript">
        $(document).ready(function () {
            $("#cmode").hide();
            $("#citem").hide();  
        });
    </script>
    <div id="MainLayOut" style="width:100%">
        <div style="float:left;margin-right:515px;box-shadow: 0px 1px 3px rgba(0, 0, 0, 0.1);">
            <h2>油藏工程虚拟仿真实验教学中心简介</h2>
            <div class="bottom f-richEditorText"><p>&nbsp; &nbsp; &nbsp; &nbsp; 油藏工程是石油工程专业的一门主要专业课，它是研究油气藏动态特征及规律和油气田开发方法及决策的工程学科，以油气藏（田）为研究对象，以地球物理、油藏地质、油层物理、渗流力学等学科为理论基础，以数学、计算机科学、经济学等学科为研究工具，以高效开发油气资源为目的的一门综合性学科。<br />&nbsp; &nbsp; &nbsp; &nbsp; 油藏是一个深埋地下而无法进行直接观察和描述的地质实体，是根据各种间接资料所描绘出来的概念模型。为了正确评价和合理开发油气藏,利用油藏工程虚拟仿真实验软件开展基础认知、虚拟实验、仿真实训和创新实践四个层次的培训。培养学生的基本技能、专业技能、工程应用能力和创新能力；同时依托开放课题、教师科研课题、石油工程设计大赛等项目，强化学生的工程应用能力，并培养学生的创新实践能力。</p>
            </div>
        </div>
        <div style="width: 500px; margin-left: -500px; position: relative;float: right;">
            <div style="margin:5px auto;"><img alt="" width="490px" src="../../Images/img2.jpg" /></div>
        </div>
    </div>
</asp:Content>
