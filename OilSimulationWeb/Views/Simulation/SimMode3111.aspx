<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MainView.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SimMode3111
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <input id="ModeIndex" type="hidden" value="3111" />
    <div class="popin_example" id="popin_example_line_8" style="left: 15px; top: 15px; width: 630px; height: 340px; overflow: hidden; display: block; position: absolute; z-index: 760; opacity: 1;">
	    <div id="popin_example_line_8_titlebar" style="text-align: left;"> </div>
    <div id="graph">Loading graph...</div>
    <div class="popin_example_xbutton" id="popin_example_line_8_xbutton" style="left: 510px; top: 10px; width: 120px; text-align: center; text-decoration:underline;position: absolute; z-index: 762; cursor: pointer;">关闭</div>
    </div>

    <script type="text/javascript">
        //left:($(document).width() - this.popupLayer.width())/2,
		//top:(document.documentElement.clientHeight - this.popupLayer.height())/2 + $(document).scrollTop(),
        var myData = new Array([10, 2], [15, 0], [18, 3], [19, 6], [20, 8.5], [25, 10], [30, 9], [35, 8], [40, 5], [45, 6], [50, 2.5]);
        var myChart = new JSChart('graph', 'line');
        myChart.setDataArray(myData);
        myChart.setAxisNameFontSize(10);
        myChart.setAxisNameX('驱油时间（天）');
        myChart.setAxisNameY('采油采出程度');
        myChart.setAxisNameColor('#787878');
        myChart.setAxisValuesNumberX(10);
        myChart.setAxisValuesNumberY(10);
        myChart.setAxisValuesColor('#38a4d9');
        myChart.setAxisColor('#38a4d9');
        myChart.setLineColor('#C71112');
        myChart.setTitle('驱油曲线（百分比）');
        myChart.setTitleColor('#383838');
        myChart.setGraphExtend(true);
        myChart.setGridColor('#38a4d9');
        myChart.setSize(616, 321);
        myChart.setAxisPaddingLeft(100);
        myChart.setAxisPaddingRight(50);
        myChart.setAxisPaddingTop(60);
        myChart.setAxisPaddingBottom(55);
        myChart.setTextPaddingLeft(45);
        myChart.setTextPaddingBottom(12);
        myChart.setBackgroundImage('../Images/chart_bg.jpg');
        myChart.draw();
	
    </script>
</asp:Content>
