<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	重庆科技学院油藏仿真实验室
</asp:Content>

<asp:Content ID="Index" ContentPlaceHolderID="MainContent" runat="server"> 
    <div id="TopPanel">
        <div class="top-main">
            <div class="top-logo"> 
                <img alt="重庆科技学院油藏仿真" src="<%=Url.Content("~/Images/zx-logo.jpg")%>" width="382" height="85" border="0" /> 
            </div>
        </div>
    </div>
    
    <div class="main"> 
        <div class="zt">
            <div class="zt-tu"><img alt="" src="<%=Url.Content("~/Images/zt-tu.png")%>" width="12" height="12" /></div>
            <div class="zt-zi"> 在线虚拟实验  &gt; <span id="curMode">基础认知</span>  &gt; <span id="curitem">水驱油模拟</span>  </div>
        </div>
        <div class="greybg">
            <div class="main"> 
                <div id="LeftPanel"> 
                    <div class="zbar">
                        <div class="zbar-title">基础认知</div> 
		                <ul style="display: block;"> 
		                    <li class="liNav select"><a href="javascript:void(0);ShowMode('#BaseOne');" class="oneline"><span class="lmenu-ico"></span><span class="zh">水驱油模拟</span></a></li>
		                    <li class="liNav"><a href="javascript:void(0);ShowMode('#BaseTwo');" class="oneline"><span class="lmenu-ico"></span><span class="zh">渗流方式模拟</span></a></li> 
		                    <li class="liNav"><a href="javascript:void(0);ShowMode('#BaseThree');" class="oneline"><span class="lmenu-ico"></span><span class="zh">弹性不稳定渗流</span></a></li>
		                </ul>
                        <div class="zbar-title">虚拟实验</div> 
		                <ul style="display: none"> 
		                    <li class="liNav"><a href="javascript:void(0);ShowMode('#VirtualOne');" class="oneline"><span class="lmenu-ico"></span><span class="zh">非活塞式水驱油影响因素</span></a></li>
		                    <li class="liNav"><a href="javascript:void(0);ShowMode('#VirtualTwo');" class="oneline"><span class="lmenu-ico"></span><span class="zh">水驱油效率实验</span></a></li> 
		                    <li class="liNav"><a href="javascript:void(0);ShowMode('#VirtualThree');" class="oneline"><span class="lmenu-ico"></span><span class="zh">采收率实验</span></a></li>
		                </ul>
                        <div class="zbar-title">仿真实训</div> 
		                <ul style="display: none">
		                    <li class="liNav"><a href="javascript:void(0);ShowMode('#SimOne');" class="oneline"><span class="lmenu-ico"></span><span class="zh">井网井距方案开发设计</span></a></li>
		                    <li class="liNav"><a href="javascript:void(0);ShowMode('#SimTwo');" class="oneline"><span class="lmenu-ico"></span><span class="zh">注采系统方案开发设计</span></a></li> 
		                </ul>
                        <div class="zbar-title">创新实践</div> 
		                <ul style="display: none"> 
		                    <li class="liNav"><a href="javascript:void(0);ShowMode('#InnovateOne');" class="oneline"><span class="lmenu-ico"></span><span class="zh">油藏开发方案实践</span></a></li>
		                    <li class="liNav"><a href="javascript:void(0);ShowMode('#InnovateTwo');" class="oneline"><span class="lmenu-ico"></span><span class="zh">气藏开发方案实践</span></a></li> 
		                </ul>
	                </div> 
                </div>

                <div id="RightPanl"> 
                    <div id="divControl">
                        <!-- 基础认知 -->
                        <script type="text/javascript">
                            $(document).ready(function () {
                                $("#b1Btn").click(function () {
                                    var curIndex = parseInt($(".b1Mode").val());
                                    switch (curIndex) {
                                        case 11:
                                            LoadUrl("/Base/BaseModeOne");
                                            break;
                                        case 12:
                                            LoadUrl("/Base/BaseModeTwo");
                                            break; 
                                    }
                                });
                            });
                        </script>
                        <div id="BaseOne" class="navControl" style="display:inline;">
                            请选择：
                            <select class="DropDownList b1Mode">
                            <option value="11">活塞式水驱油</option>
                            <option value="12">非活塞式水驱油</option>
                            </select>
                            <button id="b1Btn" class="ui-corner-all btnOK"><span class="ui-button-text">加载模型</span></button>
                        </div>
                        <script type="text/javascript">
                            $(document).ready(function () {
                                $("#b2Btn").click(function () {
                                    var curIndex = parseInt($(".b2Mode").val());
                                    switch (curIndex) {
                                        case 13:
                                            LoadUrl("/Base/BaseModeThree");
                                            break;
                                        case 14:
                                            LoadUrl("/Base/BaseModeFour");
                                            break;
                                        case 15:
                                            LoadUrl("/Base/BaseModeFive");
                                            break;
                                    }
                                });
                            });
                        </script>
                        <div id="BaseTwo" class="navControl" style="display:none;">
                            请选择：
                            <select class="DropDownList b2Mode">
                            <option value="13">单向渗流</option>
                            <option value="14">平面径向渗流</option>
                            <option value="15">球面向心流</option>
                            </select>
                            <button id="b2Btn" class="ui-corner-all btnOK"><span class="ui-button-text">加载模型</span></button>
                         </div>
                        <script type="text/javascript">
                             $(document).ready(function () {
                                 $("#b3Btn").click(function () {
                                     var curIndex = parseInt($(".b3Mode").val());
                                     switch (curIndex) {
                                         case 16:
                                             LoadUrl("/Base/BaseModeSix");
                                             break;
                                         case 17:
                                             LoadUrl("/Base/BaseModeSeven");
                                             break;
                                     }
                                 });
                             });
                        </script>
                        <div id="BaseThree" class="navControl" style="display:none;">
                            请选择：
                            <select class="DropDownList b3Mode">
                            <option value="16">稳定水压弹性驱油</option>
                            <option value="17">封闭弹性驱油</option>
                            </select>
                            <button id="b3Btn" class="ui-corner-all btnOK"><span class="ui-button-text">加载模型</span></button>
                        </div>
                        <!-- 虚拟实验 -->
                        <script type="text/javascript">
                            $(document).ready(function () {
                                $("#v1Main").change(function () {
                                    $(".DropDownList.v1Mode").each(function (intdex, item) { $(item).css("display", "none"); });
                                    if ($(this).val() == "211") {
                                        $(".DropDownList.v1Mode").eq(0).css("display", "inline");
                                    }
                                    if ($(this).val() == "212") {
                                        $(".DropDownList.v1Mode").eq(1).css("display", "inline");
                                    }
                                    if ($(this).val() == "213") {
                                        $(".DropDownList.v1Mode").eq(2).css("display", "inline");
                                    }
                                });
                                $("#v1Btn").click(function () {
                                    var curIndex = parseInt($(".v1Mode:visible").val());
                                    switch (curIndex) {
                                        case 2111:
                                            LoadUrl("/VirExperiment/VirtualMode2111");
                                            break;
                                        case 2112:
                                            LoadUrl("/VirExperiment/VirtualMode2112");
                                            break;
                                        case 2113:
                                            LoadUrl("/VirExperiment/VirtualMode2113");
                                            break;
                                        case 2114:
                                            LoadUrl("/VirExperiment/VirtualMode2114");
                                            break;
                                        case 2121:
                                            LoadUrl("/VirExperiment/VirtualMode2121");
                                            break;
                                        case 2122:
                                            LoadUrl("/VirExperiment/VirtualMode2122");
                                            break;
                                        case 2123:
                                            LoadUrl("/VirExperiment/VirtualMode2123");
                                            break;
                                        case 2124:
                                            LoadUrl("/VirExperiment/VirtualMode2124");
                                            break;
                                        case 2131:
                                            LoadUrl("/VirExperiment/VirtualMode2131");
                                            break;
                                        case 2132:
                                            LoadUrl("/VirExperiment/VirtualMode2132");
                                            break;
                                        case 2133:
                                            LoadUrl("/VirExperiment/VirtualMode2133");
                                            break;
                                        case 2134:
                                            LoadUrl("/VirExperiment/VirtualMode2134");
                                            break;
                                    }
                                });
                            });
                        </script>
                        <div id="VirtualOne" class="navControl" style="display:none;">
                            请选择：
                            <select class="DropDownList" id="v1Main"> 
                            <option value="211">毛细管压力</option>
                            <option value="212">油水比重差</option>
                            <option value="213">油水粘度差</option>
                            </select>
                            <select class="DropDownList v1Mode" style="display:inline;">
                            <option value="2111">0.5</option>
                            <option value="2112">1</option>
                            <option value="2113">2</option>
                            <option value="2114">自定义</option>
                            </select>
                            <select class="DropDownList v1Mode" style="display:none;">
                            <option value="2121">0.7</option>
                            <option value="2122">0.8</option>
                            <option value="2123">0.9</option>
                            <option value="2124">自定义</option>
                            </select>
                            <select class="DropDownList v1Mode" style="display:none;">
                            <option value="2131">0.5</option>
                            <option value="2132">10</option>
                            <option value="2133">50</option>
                            <option value="2134">自定义</option>
                            </select>
                            <button id="v1Btn" class="ui-corner-all btnOK"><span class="ui-button-text">加载模型</span></button>
                        </div> 
                        <script type="text/javascript">
                            $(document).ready(function () {
                                $("#v2Main").change(function () {
                                    $(".DropDownList.v2Mode").each(function (intdex, item) { $(item).css("display", "none"); });
                                    if ($(this).val() == "221") {
                                        $(".DropDownList.v2Mode").eq(0).css("display", "inline");
                                    }
                                    if ($(this).val() == "222") {
                                        $(".DropDownList.v2Mode").eq(1).css("display", "inline");
                                    }
                                    if ($(this).val() == "223") {
                                        $(".DropDownList.v2Mode").eq(2).css("display", "inline");
                                    }
                                });
                                $("#v2Btn").click(function () {
                                    var curIndex = parseInt($(".v2Mode:visible").val());
                                    switch (curIndex) {
                                        case 2211:
                                            LoadUrl("/VirExperiment/VirtualMode2211");
                                            break;
                                        case 2212:
                                            LoadUrl("/VirExperiment/VirtualMode2212");
                                            break;
                                        case 2213:
                                            LoadUrl("/VirExperiment/VirtualMode2213");
                                            break;
                                        case 2221:
                                            LoadUrl("/VirExperiment/VirtualMode2221");
                                            break;
                                        case 2222:
                                            LoadUrl("/VirExperiment/VirtualMode2222");
                                            break;
                                        case 2223:
                                            LoadUrl("/VirExperiment/VirtualMode2223");
                                            break;
                                        case 2231:
                                            LoadUrl("/VirExperiment/VirtualMode2231");
                                            break;
                                        case 2232:
                                            LoadUrl("/VirExperiment/VirtualMode2232");
                                            break;
                                        case 2233:
                                            LoadUrl("/VirExperiment/VirtualMode2233");
                                            break;
                                    }
                                });
                            });
                            
                        </script>
                        <div id="VirtualTwo" class="navControl" style="display:none;">
                            请选择：
                            <select class="DropDownList" id="v2Main">
                            <option value="221">毛细管压力</option>
                            <option value="222">原油粘度级别</option>
                            <option value="223">原油密度级别</option>
                            </select>
                            <select class="DropDownList v2Mode" style="display:inline;">
                            <option value="2211">无毛管</option>
                            <option value="2212">低毛管</option>
                            <option value="2213">高毛管</option>
                            </select>
                            <select class="DropDownList v2Mode" style="display:none;">
                            <option value="2221">低粘度0.5</option>
                            <option value="2222">中粘度5</option>
                            <option value="2223">高粘度50</option>
                            </select>
                            <select class="DropDownList v2Mode" style="display:none;">
                            <option value="2231">低密度0.7</option>
                            <option value="2232">中密度0.9</option>
                            <option value="2233">高密度1.15</option>
                            </select>
                            <button id="v2Btn" class="ui-corner-all btnOK"><span class="ui-button-text">加载模型</span></button>
                        </div> 
                        
                        <script type="text/javascript">
                            $(document).ready(function () {
                                $("#v3Main").change(function () {
                                    $(".DropDownList.v3Mode").each(function (intdex, item) { $(item).css("display", "none"); });
                                    if ($(this).val() == "231") {
                                        $(".DropDownList.v3Mode").eq(0).css("display", "inline");
                                    }
                                    if ($(this).val() == "232") {
                                        $(".DropDownList.v3Mode").eq(1).css("display", "inline");
                                    }
                                    if ($(this).val() == "233") {
                                        $(".DropDownList.v3Mode").eq(2).css("display", "inline");
                                    }
                                });
                                $("#v3Btn").click(function () {
                                    var curIndex = parseInt($(".v3Mode:visible").val());
                                    switch (curIndex) {
                                        case 2311:
                                            LoadUrl("/VirExperiment/VirtualMode2311");
                                            break; 
                                        case 2312:
                                            LoadUrl("/VirExperiment/VirtualMode2312");
                                            break;
                                        case 2313:
                                            LoadUrl("/VirExperiment/VirtualMode2313");
                                            break;
                                        case 2321:
                                            LoadUrl("/VirExperiment/VirtualMode2321");
                                            break;
                                        case 2322:
                                            LoadUrl("/VirExperiment/VirtualMode2322");
                                            break;
                                        case 2323:
                                            LoadUrl("/VirExperiment/VirtualMode2323");
                                            break;
                                        case 2331:
                                            LoadUrl("/VirExperiment/VirtualMode2331");
                                            break;
                                        case 2332:
                                            LoadUrl("/VirExperiment/VirtualMode2332");
                                            break;
                                        case 2333:
                                            LoadUrl("/VirExperiment/VirtualMode2333");
                                            break;
                                    }
                                });
                            });
                            
                        </script>
                        <div id="VirtualThree" class="navControl" style="display:none;">
                            请选择：
                            <select class="DropDownList" id="v3Main">
                            <option value="231">束缚水饱和度</option>
                            <option value="232">残余油饱和度</option>
                            <option value="233">油水相渗曲线</option>
                            </select>
                            <select class="DropDownList v3Mode" style="display:inline;">
                            <option value="2311">低束缚水0</option>
                            <option value="2312">中束缚水0.3</option>
                            <option value="2313">高束缚水0.5</option>
                            </select>
                            <select class="DropDownList v3Mode" style="display:none;">
                            <option value="2321">低残余油0</option>
                            <option value="2322">中残余油0.3</option>
                            <option value="2323">高残余油0.5</option>
                            </select>
                            <select class="DropDownList v3Mode" style="display:none;">
                            <option value="2331">常型曲线</option>
                            <option value="2332">立型曲线</option>
                            <option value="2333">X型曲线</option>
                            </select>
                            <button id="v3Btn" class="ui-corner-all btnOK"><span class="ui-button-text">加载模型</span></button>
                        </div>
                        <!-- 仿真实训 -->
                        <script type="text/javascript">
                            $(document).ready(function () {
                                $("#txtWell").hide();
                                $("#s1Main").change(function () {
                                    $(".DropDownList.s1Mode").each(function (intdex, item) { $(item).css("display", "none"); });
                                    if ($(this).val() == "311") {
                                        $(".DropDownList.s1Mode").eq(0).css("display", "inline");
                                    }
                                    if ($(this).val() == "312") {
                                        $("#txtWell").hide();
                                        $(".DropDownList.s1Mode").eq(1).css("display", "inline");
                                    }
                                });
                                $(".DropDownList.s1Mode").change(function () {
                                    if ($(".s1Mode:visible").val() == "3114")
                                        $("#txtWell").show(); 
                                    else
                                        $("#txtWell").hide();   
                                });
                                $("#s1Btn").click(function () {
                                    var curIndex = parseInt($(".s1Mode:visible").val());
                                    switch (curIndex) {
                                        case 3111:
                                            LoadUrl("/Simulation/SimMode3111");
                                            break;
                                        case 3112:
                                            LoadUrl("/Simulation/SimMode3112");
                                            break;
                                        case 3113:
                                            LoadUrl("/Simulation/SimMode3113");
                                            break;
                                        case 3114:
                                            {
                                                LoadUrl("/Simulation/SimMode3114");
                                                $("#iframeId").unbind("load").load(function () {
                                                    var iDist = $("#txtWell").val();
                                                    $("#iframeId")[0].contentWindow.postMessage("$('#wellDist').val(" + iDist + ")", "*");
                                                    $("#iframeId")[0].contentWindow.postMessage("$('#wellDist').click()", "*");
                                                });
                                            }
                                            break;
                                        case 3121:
                                            LoadUrl("/Simulation/SimMode3121");
                                            break;
                                        case 3122:
                                            LoadUrl("/Simulation/SimMode3122");
                                            break;
                                        case 3123:
                                            LoadUrl("/Simulation/SimMode3123");
                                            break;
                                        case 3124:
                                            LoadUrl("/Simulation/SimMode3124");
                                            //CreateDiv();
                                            //$("#btnShow3124").click();
                                            break;
                                    }
                                });
                            });
                            
                        </script>
                        <div id="SimOne" class="navControl" style="display:none;">
                            请选择：
                            <select class="DropDownList" id="s1Main">
                            <option value="311">井距</option>
                            <option value="312">井网</option> 
                            </select>
                            <select class="DropDownList s1Mode" style="display:inline;">
                            <option value="3111">100</option>
                            <option value="3112">200</option>
                            <option value="3113">300</option>
                            <option value="3114">自定义</option>
                            </select>
                            <select class="DropDownList s1Mode" style="display:none;">
                            <option value="3121">5点</option>
                            <option value="3122">7点</option>
                            <option value="3123">9点</option>
                            <option value="3124">自定义</option>
                            </select>
                            <input type="text" id="txtWell" style="width:50px;" />
                            <button id="s1Btn" class="ui-corner-all btnOK"><span class="ui-button-text">加载模型</span></button>
                            <input id="btnShow3124"  type="hidden" />
                        </div>
                        <script type="text/javascript">
                            $(document).ready(function () {
                                $("#s2Main").change(function () {
                                    $(".DropDownList.s2Mode").each(function (intdex, item) { $(item).css("display", "none"); });
                                    if ($(this).val() == "321") {
                                        $(".DropDownList.s2Mode").eq(0).css("display", "inline");
                                    }
                                    else if ($(this).val() == "322") {
                                        $(".DropDownList.s2Mode").eq(1).css("display", "inline");
                                    }
                                    else if ($(this).val() == "323") {
                                        $(".DropDownList.s2Mode").eq(2).css("display", "inline");
                                    }
                                    else if ($(this).val() == "324") {
                                        $(".DropDownList.s2Mode").eq(3).css("display", "inline");
                                    }
                                });
                                $("#s2Btn").click(function () {
                                    var curIndex = parseInt($(".s2Mode:visible").val());
                                    switch (curIndex) {
                                        case 3211:
                                            LoadUrl("/Simulation/SimMode3211");
                                            break;
                                        case 3212:
                                            LoadUrl("/Simulation/SimMode3212");
                                            break;
                                        case 3213:
                                            LoadUrl("/Simulation/SimMode3213");
                                            break;
                                        case 3214:
                                            LoadUrl("/Simulation/SimMode3214");
                                            break;
                                        case 3221:
                                            LoadUrl("/Simulation/SimMode3221");
                                            break;
                                        case 3222:
                                            LoadUrl("/Simulation/SimMode3222");
                                            break;
                                        case 3223:
                                            LoadUrl("/Simulation/SimMode3223");
                                            break;
                                        case 3224:
                                            LoadUrl("/Simulation/SimMode3224");
                                            break;
                                        case 3231:
                                            LoadUrl("/Simulation/SimMode3231");
                                            break;
                                        case 3232:
                                            LoadUrl("/Simulation/SimMode3232");
                                            break;
                                        case 3233:
                                            LoadUrl("/Simulation/SimMode3233");
                                            break;
                                        case 3234:
                                            LoadUrl("/Simulation/SimMode3234");
                                            break;
                                        case 3241:
                                            LoadUrl("/Simulation/SimMode3241");
                                            break;
                                        case 3242:
                                            LoadUrl("/Simulation/SimMode3242");
                                            break;
                                        case 3243:
                                            LoadUrl("/Simulation/SimMode3243");
                                            break;
                                        case 3244:
                                            LoadUrl("/Simulation/SimMode3244");
                                            break;
                                    }
                                });
                            });
                            
                        </script>
                        <div id="SimTwo" class="navControl" style="display:none;">
                            请选择：
                            <select class="DropDownList" id="s2Main">
                            <option value="321">不同注水时机</option>
                            <option value="322">不同注采比</option> 
                            <option value="323">不同最大井底注入压力</option> 
                            <option value="324">不同最小井底流压</option>  
                            </select>
                            <select class="DropDownList s2Mode" style="display:inline;">
                            <option value="3211">早期（同采同注）</option>
                            <option value="3212">中期开发后半年</option>
                            <option value="3213">晚期开发后两年</option>
                            <option value="3214">自定义</option>
                            </select>
                            <select class="DropDownList s2Mode" style="display:none;">
                            <option value="3221">注采比0.8</option>
                            <option value="3222">注采比1</option>
                            <option value="3223">注采比1.2</option>
                            <option value="3224">自定义</option>
                            </select>
                            <select class="DropDownList s2Mode" style="display:none;">
                            <option value="3231">35MPA</option>
                            <option value="3232">40MPA</option>
                            <option value="3233">50MPA</option>
                            <option value="3234">自定义</option>
                            </select>
                            <select class="DropDownList s2Mode" style="display:none;">
                            <option value="3241">1MPA</option>
                            <option value="3242">5MPA</option>
                            <option value="3243">10MPA</option>
                            <option value="3244">自定义</option>
                            </select>
                            <button id="s2Btn" class="ui-corner-all btnOK"><span class="ui-button-text">加载模型</span></button>
                        </div>
                        <!-- 创新实践 -->
                        <script type="text/javascript">
                            $(document).ready(function () {
                                $("#i1Btn").click(function () {
                                    var curIndex = parseInt($("#i1Main").val());
                                    switch (curIndex) {
                                        case 411:
                                            LoadUrl("/Innovate/InnModeOne");
                                            break;
                                        case 412:
                                            LoadUrl("/Innovate/InnModeTwo");
                                            break;
                                    }
                                });
                                $("#i2Btn").click(function () {
                                    var curIndex = parseInt($("#i2Main").val());
                                    switch (curIndex) {
                                        case 421:
                                            LoadUrl("/Innovate/InnModeThree");
                                            break;
                                        case 422:
                                            LoadUrl("/Innovate/InnModeFour");
                                            break;
                                    }
                                });
                            });
                        </script>
                        <div id="InnovateOne" class="navControl" style="display:none;">
                            请选择：
                            <select class="DropDownList" id="i1Main">
                            <option value="411">均质油藏</option>
                            <option value="412">实际油藏</option> 
                            </select> 
                            <button id="i1Btn" class="ui-corner-all btnOK"><span class="ui-button-text">加载模型</span></button>
                        </div>
                        <div id="InnovateTwo" class="navControl" style="display:none;">
                            请选择：
                            <select class="DropDownList" id="i2Main">
                            <option value="421">均质气藏</option>
                            <option value="422">实际气藏</option> 
                            </select>
                            <button id="i2Btn" class="ui-corner-all btnOK"><span class="ui-button-text">加载模型</span></button>
                        </div>
                    </div>
                    <div>
                        <iframe id="iframeId" name="iframeId" scrolling="no" class="ScreenNomal"  src=""></iframe>
                    </div> 

                </div>
                <div class="clear"></div>
            </div> 
        </div> 
    </div> 

    <div class="foot2">
        <div class="main">
        COPYRIGHT 2013 (C) CHONGQING UNIVERSITY OF SCIENCE AND TECHNOLOGY ALL RIGHTS RESERVED<br>
        重庆沙坪坝区大学城东路20号 邮编：401331 备案号：渝ICP备13000511号 技术支持：<a href="javascript:void(0);" target="_blank">重庆昌禀科技</a>
        </div>
    </div>
    
</asp:Content>
