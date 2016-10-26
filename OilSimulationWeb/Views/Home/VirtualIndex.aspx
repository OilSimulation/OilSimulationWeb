<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	重庆科技学院油藏仿真实验室
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#cmode").hide();
            $("#citem").hide();

            $("#NavVir").siblings("ul").css("display", "none");
            $("#NavVir").next().css("display", "block");
            $("#NavVir").siblings("ul").children("li").css("display", "none");
            $("#NavVir").next().children("li").slideDown(500);
        });
    </script>
    <div id="MainLayOut" style="width:100%">
        <div style="float:left;margin-right:315px;box-shadow: 0px 1px 3px rgba(0, 0, 0, 0.1);">
            <h2>虚拟实验</h2>
            <div class="bottom f-richEditorIndex"><p>&nbsp; &nbsp; &nbsp; &nbsp; 本部分主要包括非活塞式水驱油影响因素实验、水驱油效率实验、采收率实验三个模块。<br /><br />&nbsp; &nbsp; &nbsp; &nbsp; 在非活塞式水驱油影响因素实验中，可以开展不同毛细管力、不同油水比重差、不同油水粘度差对水驱油的影响模拟研究；在水驱油效率实验中，可以开展不同类型毛管压力、不同类型原油粘度级别、不同类型原油密度级别下的水驱油效率实验；在采收率实验中，可以开展不同类型束缚水饱和度、不同类型残余油饱和度、不同类型油水相渗曲线下的采收率实验。<br /><br />&nbsp; &nbsp; &nbsp; &nbsp; 教学目的及要求：理解非活塞式水驱油的物理含义；掌握非活塞式水驱油饱和度分布曲线；理解影响水驱油非活塞式驱油的因素；理解影响采收率的主要因素；掌握不同的采收率计算方法。</p>
            </div>
        </div>
        <div style="width: 300px; margin-left: -300px; position: relative;float: right;">
            <div style="padding:5px auto;"><img alt="" width="300px" src="../../Images/img11.png" /></div>
            <div style="padding:5px auto;"><img alt="" width="300px" src="../../Images/img12.png" /></div>
        </div>
    </div>
</asp:Content>

