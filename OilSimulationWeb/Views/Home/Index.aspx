<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	重庆科技学院油藏仿真实验室
</asp:Content>

<asp:Content ID="Index" ContentPlaceHolderID="MainContent" runat="server"> 
    <div id="TopPanel">
        <div class="top-main">
            <div class="top-logo"> 
                <img alt="重庆科技学院油藏仿真" src="<%=Url.Content("~/Images/zx-logo.jpg")%>" width="235" height="85" border="0" /> 
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
		                    <li class="liNav"><a href="javascript:void(0);" class="oneline"><span class="lmenu-ico"></span><span class="zh">井网井距方案开发设计</span></a></li>
		                    <li class="liNav"><a href="javascript:void(0);" class="oneline"><span class="lmenu-ico"></span><span class="zh">注采系统方案开发设计</span></a></li> 
		                </ul>
                        <div class="zbar-title">创新实践</div> 
		                <ul style="display: none"> 
		                    <li class="liNav"><a href="javascript:void(0);" class="oneline"><span class="lmenu-ico"></span><span class="zh">油藏开发方案实践</span></a></li>
		                    <li class="liNav"><a href="javascript:void(0);" class="oneline"><span class="lmenu-ico"></span><span class="zh">气藏开发方案实践</span></a></li> 
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
                                $("#v1Btn").click(function () {
                                    var curIndex = parseInt($(".v1Mode").val());
                                    switch (curIndex) {
                                        case 211:
                                            LoadUrl("/Base/BaseModeOne");
                                            break;
                                        case 212:
                                            LoadUrl("/Base/BaseModeOne");
                                            break;
                                        case 213:
                                            LoadUrl("/Base/BaseModeOne");
                                            break;
                                        case 214:
                                            LoadUrl("/Base/BaseModeOne");
                                            break;
                                    }
                                });
                            });
                        </script>
                        <div id="VirtualOne" class="navControl" style="display:none;">
                            请选择：
                            <select class="DropDownList v1Mode">
                            <option value="211">默认参数非活塞驱油</option>
                            <option value="212">改变毛细管压力</option>
                            <option value="213">改变油水比重</option>
                            <option value="214">改变油水粘度</option>
                            </select>
                            <button id="v1Btn" class="ui-corner-all btnOK"><span class="ui-button-text">加载模型</span></button>
                        </div> 
                        <script type="text/javascript">
                            $(document).ready(function () {
                                $("#v2Main").change(function () {
                                    $(".v2Mode").each(function (intdex, item) { $(item).css("display", "none"); });
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
                                            LoadUrl("/Base/BaseModeOne");
                                            break;
                                            LoadUrl("/Base/BaseModeOne");
                                        case 2212:
                                            LoadUrl("/Base/BaseModeOne");
                                            break;
                                        case 2213:
                                            LoadUrl("/Base/BaseModeOne");
                                            break;
                                        case 2221:
                                            LoadUrl("/Base/BaseModeOne");
                                            break;
                                        case 2222:
                                            LoadUrl("/Base/BaseModeOne");
                                            break;
                                        case 2223:
                                            LoadUrl("/Base/BaseModeOne");
                                            break;
                                        case 2231:
                                            LoadUrl("/Base/BaseModeOne");
                                            break;
                                        case 2232:
                                            LoadUrl("/Base/BaseModeOne");
                                            break;
                                        case 2233:
                                            LoadUrl("/Base/BaseModeOne");
                                            break;
                                    }
                                });
                            });
                            
                        </script>
                        <div id="VirtualTwo" class="navControl" style="display:none;">
                            请选择：
                            <select class="DropDownList" id="v2Main" style="display:inline;">
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
                                    $(".v3Mode").each(function (intdex, item) { $(item).css("display", "none"); });
                                    if ($(this).val() == "231") {
                                        $(".DropDownList.v2Mode").eq(0).css("display", "inline");
                                    }
                                    if ($(this).val() == "232") {
                                        $(".DropDownList.v2Mode").eq(1).css("display", "inline");
                                    }
                                    if ($(this).val() == "233") {
                                        $(".DropDownList.v2Mode").eq(2).css("display", "inline");
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
                            <button class="ui-corner-all btnOK"><span class="ui-button-text">加载模型</span></button>
                        </div>
                        <!-- 仿真实训 -->
                    </div>
                    <div>
                        <iframe id="iframeId" name="iframeId" scrolling="no" style="height: 600px; width: 100%; border:0;" src=""></iframe>
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
