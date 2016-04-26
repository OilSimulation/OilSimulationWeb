<%@ Control Language="C#"  Inherits="System.Web.Mvc.ViewUserControl" %>
    <style type="text/css">
			div#canvas-frame {
				border: none;
				cursor: pointer;
				width: 100%;
				height: 400px;
				background-color: #EEEEEE;
			}

		</style>
        		<script type="text/javascript">
        		    
        		    function List() {
        		        this.value = [];
        		        /* 添加 */
        		        this.add = function (obj) {
        		            return this.value.push(obj);
        		        };
        		        /* 大小 */
        		        this.size = function () {
        		            return this.value.length;
        		        };
        		        /* 返回指定索引的值 */
        		        this.get = function (index) {
        		            return this.value[index];
        		        };
        		        /* 删除指定索引的值 */
        		        this.remove = function (index) {
        		            this.value.splice(index, 1);
        		            return this.value;
        		        };
        		        /* 删除全部值 */
        		        this.removeAll = function () {
        		            return this.value = [];
        		        };
        		        /* 是否包含某个对象 */
        		        this.constains = function (obj) {
        		            for (var i in this.value) {
        		                if (obj == this.value[i]) {
        		                    return true;
        		                } else {
        		                    continue;
        		                }
        		            }
        		            return false;
        		        };

        		        /* 是否包含某个对象 */
        		        this.getAll = function () {
        		            var allInfos = '';
        		            for (var i in this.value) {
        		                if (i != (value.length - 1)) {
        		                    allInfos += this.value[i] + ",";
        		                } else {
        		                    allInfos += this.value[i];
        		                }
        		            }
        		            alert(allInfos);
        		            return allInfos += this.value[i] + ","; ;
        		        };

        		    } 


        		    var renderer, stats, frame;
        		    function initThree() {
        		        frame = document.getElementById('canvas-frame');
        		        width = document.getElementById('canvas-frame').clientWidth;
        		        height = document.getElementById('canvas-frame').clientHeight;
        		                		        renderer = new THREE.WebGLRenderer({
        		                		            antialias: true
        		                		        });//显卡渲染

//        		        renderer = new THREE.CanvasRenderer({
//        		            antialias: true
//        		        });//软件渲染
        		        renderer.setSize(width, height);
        		        document.getElementById('canvas-frame').appendChild(renderer.domElement);
        		        //renderer.setClearColorHex(0xFFFFFF, 1.0);


        		        window.addEventListener('resize', onWindowResize, false);
        		    }

        		    var camera;
        		    function initCamera() {
                        
        		        camera = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 1, 10000);//透视
        		        camera.position.z = 1500;

//        		        camera = new THREE.OrthographicCamera(0, 600, 3000, 0, 1, 5000);//正交投影，
//        		        camera.position.set(500, 3000, 4000);
//        		        

        		        camera.lookAt(new THREE.Vector3(0, 0, 0));//视野中心坐标
        		    }

        		    var scene;
        		    function initScene() {
        		        scene = new THREE.Scene();
        		    }

        		    var light;
        		    function initLight() {
        		        light = new THREE.PointLight(0x00FF00);
        		    }

        		    var cube;
        		    var mesh;
        		    var varCubeGeometry;
        		    var geometry = new THREE.Geometry();
        		    var colors = new THREE.Color();
                    //加载模型
        		    function initObject(x, y, z,xWidth,yWidth,zWidth, color) {
        		        varCubeGeometry = new THREE.CubeGeometry(100, 100, 100);
        		        cube = new THREE.Mesh(varCubeGeometry,
                                                    new THREE.MeshBasicMaterial({
                                                         vertexColors: THREE.VertexColors

                                                    }));
        		        cube.position.x = x;
        		        cube.position.z = y;
        		        cube.position.y = z;
        		        cube.updateMatrix();
        		        applyVertexColors(varCubeGeometry, colors.setHex(color));//设置面颜色
        		        geometry.merge(cube.geometry, cube.matrix);
                    }


                    //计算当前方格颜色.
                    //selMaxColor用户选的颜色最大值,
                    function CaculateColor(selMaxColorR,selMaxColorG,selMaxColorB, selMinColorR,selMinColorG,selMinColorB, curColor, maxColor, minColor) {
                            var h, s, l, H, S, L;
                            var minInfo = RGB2HSL(selMinColorR,selMinColorG,selMinColorB);
                            var maxInfo = RGB2HSL(selMaxColorR,selMaxColorG,selMaxColorB);
                            h = minInfo["H"];
                            s = minInfo["S"];
                            l = minInfo["L"];

                            H = maxInfo["H"];
                            S = maxInfo["S"];
                            L = maxInfo["L"];

                            var info;

                            if (0.0 == maxColor || curColor == maxColor)
                            {
                                info ={"R":selMaxColorR,"G":selMaxColorG,"B":selMaxColorB};
                                return info;
                            }
                            else
                            {
                                if (minColor == curColor)
                                {
                                    info ={"R":selMinColorR,"G":selMinColorG,"B":selMinColorB};
                                    return info;
                                }
                                var fStep = (curColor - minColor) / (maxColor - minColor) * (H - h) + h;

                                if (fStep < 0.0)
                                {
                                    fStep += 1;
                                }
                                return HSL2RGB(fStep, s, l);

                            }
                    }

                    //返回 R:VALUE,G:VALUE,B:VALUE
                    function HSL2RGB(h,sl,l){
                        var v;
                        var r, g, b;
                        r = l;   // default to gray
                        g = l;
                        b = l;
                        v = (l <= 0.5) ? (l * (1.0 + sl)) : (l + sl - l * sl);
                        if (v > 0)
                        {
                            var m;
                            var sv;
                            var sextant;
                            var fract, vsf, mid1, mid2;
                            m = l + l - v;
                            sv = (v - m) / v;
                            h *= 6.0;
                            sextant = parseInt(h);
                            fract = h - sextant;
                            vsf = v * sv * fract;
                            mid1 = m + vsf;
                            mid2 = v - vsf;
                            switch (sextant)
                            {

                                case 0:
                                    r = v;
                                    g = mid1;
                                    b = m;
                                    break;
                                case 1:
                                    r = mid2;
                                    g = v;
                                    b = m;
                                    break;
                                case 2:
                                    r = m;
                                    g = v;
                                    b = mid1;
                                    break;
                                case 3:
                                    r = m;
                                    g = mid2;
                                    b = v;
                                    break;
                                case 4:
                                    r = mid1;
                                    g = m;
                                    b = v;
                                    break;
                                case 5:
                                    r = v;
                                    g = m;
                                    b = mid2;
                                    break;
                            }
                        }
                        var info = {"R":parseInt(r*255),"G":parseInt(g*255),"B":parseInt(b*255)};
                        return info;
                    }

                    //返回H:VALUE,S:VALUE,L:VALUE
                    function RGB2HSL(ColorR, ColorG, ColorB) {
                        var r = ColorR / 255.0;
                        var g = ColorG / 255.0;
                        var b = ColorB / 255.0;
                        var v;
                        var m;
                        var vm;
                        var r2, g2, b2;
                        var h = 0; // default to black
                        var s = 0;
                        var l = 0;
                        v = r > g ? r : g;
                        v = v > b ? v : b;
                        m = r > b ? b : r;
                        m = m > b ? b : m;
                        l = (m + v) / 2.0;
                        if (l <= 0.0) {
                            return;
                        }
                        vm = v - m;
                        s = vm;
                        if (s > 0.0) {
                            s /= (l <= 0.5) ? (v + m) : (2.0 - v - m);
                        }
                        else {
                            return;
                        }
                        r2 = (v - r) / vm;
                        g2 = (v - g) / vm;
                        b2 = (v - b) / vm;
                        if (r == v) {
                            h = (g == m ? 5.0 + b2 : 1.0 - g2);
                        }
                        else if (g == v) {
                            h = (b == m ? 1.0 + r2 : 3.0 - b2);
                        }
                        else {
                            h = (r == m ? 3.0 + g2 : 5.0 - r2);
                        }
                        h /= 6.0;
                        var info = { "H": h, "S": s, "L": l };
                        return info;
                    }

                     

                    //帧情况
                    function InitStats() {
                        stats = new Stats();
//                        stats.domElement.style.position = 'absolute';
//                        stats.domElement.style.top = '0px';
//                        frame.appendChild(stats.domElement);

                    }

        		    var controls;
        		    function InitControl() {
        		        controls = new THREE.OrbitControls(camera, renderer.domElement);
        		    }
        		    var cubeMaterial = new THREE.MeshBasicMaterial({ vertexColors: THREE.VertexColors });
        		    //var cubeMaterial = new THREE.MeshBasicMaterial({ vertexColors: THREE.FaceColors });

        		    function funGetModelData() {
        		        debugger
        		        $.ajax({
        		            type: "post",
        		            url: "/Business/GetModelData",
        		            dataType: "json",
        		            data: { "Step": 0 },
        		            success: function (data) {
        		                var list = data.list; //模型列表数据
        		                var info, color;
        		                //alert(list.length);
        		                for (var i = 0; i < list.length; i++) {
        		                    info = CaculateColor(255, 0, 0, 0, 0, 255, list[i].Color, list[i].MaxColor, list[i].MinColor);
        		                    if (info) {
        		                        color = (info["R"] << 16) | (info["G"] << 8) | info["B"];
        		                    }
        		                    initObject(list[i].X, list[i].Y, list[i].Z,list[i].xWidth,list[i].yWidth,list[i].zWidth, color);
        		                }
        		                if (mesh)
        		                    scene.remove(mesh);
        		                mesh = new THREE.Mesh(geometry, cubeMaterial)
        		                scene.add(mesh);
        		            }
        		        });
        		    }

                    //获取颜色 数据
        		    function funGetColorData(step) {
        		        $.ajax({
        		            type: "post",
        		            url: "/Business/GetModelData",
        		            dataType: "json",
        		            data: { "Step": step },
        		            success: function (data) {
        		                var list = data.list; //模型列表数据
        		                var info, color;
        		                var colors = [];
        		                //alert(list.length);
        		                for (var i = 0; i < list.length; i++) {
        		                    info = CaculateColor(255, 0, 0, 0, 0, 255, list[i].Color, list[i].MaxColor, list[i].MinColor);
        		                    if (info) {
        		                        color = (info["R"] << 16) | (info["G"] << 8) | info["B"];
        		                        colors[i] = color;
        		                    }
        		                }
        		                //修改颜色 
        		                setFacesVertexColors(geometry, colors);

        		            }
        		        });
        		    }



        		    function threeStart() {
        		        
        		        initThree();
        		        initCamera();
        		        initScene();
        		        //initLight();
        		        //initObject();
        		        funGetModelData();
        		        InitStats();
        		        //InitControl();



        		        animation();
        		        //TrackballControls,OrbitControls
        		        controls = new THREE.OrbitControls(camera, renderer.domElement);
        		        //controls = new THREE.TrackballControls(camera);
        		        controls.target.set(0, 0, 0);
        		        
        		        controls.update();

        		    }
        		    function onWindowResize() {
        		        camera.aspect = window.innerWidth / window.innerHeight;
        		        camera.updateProjectionMatrix();
        		        renderer.setSize(window.innerWidth, window.innerHeight);
        		    }

        		    function animation() {

        		        renderer.render(scene, camera);
        		        requestAnimationFrame(animation);
        		        stats.update(); //帧情况


        		        //controls.update();
        		    }

        		    //设置颜色 g:geometry对象，c:颜色(0xff0000)
        		    function applyVertexColors(g, c) {

        		        g.faces.forEach(function (f) {
        		            var n = (f instanceof THREE.Face3) ? 3 : 4;
        		            if (f.vertexColors.length <= 0) {
        		                for (var j = 0; j < n; j++) {
        		                    f.vertexColors[j] = c;
        		                }
        		            }
        		            else {
        		                for (var j = 0; j < n; j++) {
        		                    f.vertexColors[j].setHex(c);
        		                }
        		            }
        		        });
        		    }

        		    //设置 面颜色 g:geometry对象，c:颜色json列表
        		    function setFacesVertexColors(g, c) {
        		        //每十二个面表示一个立方体。
        		        var faceColor = new THREE.Color;
        		        var facesLen = g.faces.length;
        		        var colorLen = c.length;
        		        var colorIndex = 0;
        		        for (var i = 0; i < facesLen; i += 12) {
        		            if (i / 12 > colorLen) {
        		                colorIndex = colorLen - 1;
        		            }
        		            else {
        		                colorIndex = i / 12;
        		            }
        		            for (var j = 0; j < 12; j++) {
        		                if (g.faces[i + j].vertexColors.length <= 0) {
        		                    g.faces[i + j].vertexColors[0] = faceColor.setHex(c[colorIndex]);
        		                    g.faces[i + j].vertexColors[1] = faceColor.setHex(c[colorIndex]);
        		                    g.faces[i + j].vertexColors[2] = faceColor.setHex(c[colorIndex]);
        		                }
        		                else {
        		                    g.faces[i + j].vertexColors[0].setHex(c[colorIndex]);
        		                    g.faces[i + j].vertexColors[1].setHex( c[colorIndex]);
        		                    g.faces[i + j].vertexColors[2].setHex( c[colorIndex]);
        		                }
        		            }
        		        }
        		        g.colorsNeedUpdate = true;
        		    }

        		    
        		    function ChangeColor() {
        		        var cccc = new THREE.Color();
        		        applyVertexColors(geometry, 0xff0000);

        		        geometry.colorsNeedUpdate = true;

        		    }

        		    //暂停 numberMillis毫秒
        		    function sleep(numberMillis) {
        		        var now = new Date();
        		        var exitTime = now.getTime() + numberMillis;
        		        while (true) { now = new Date(); if (now.getTime() > exitTime) return; } 
                     }

                    var curStep = 0;
                    function UpStep() {
                        if (curStep <= 0) {
                            $("#upstep").attr("disabled", true);

                        }
                        else {
                            $("#upstep").removeAttr("disabled");
                            curStep--;
                            funGetColorData(curStep);

                        }
                        ShowStep(curStep);
                        
                    }

                    function DownStep() {
                        if (curStep >=99) {
                            $("#downstep").attr("disabled", true);

                        }
                        else {
                            $("#downstep").removeAttr("disabled");
                            curStep++;
                            funGetColorData(curStep);

                        }
                        ShowStep(curStep);
                    }
                    var intervalId;
                    function Play() {
                        intervalId = setInterval(start, 500);
                    }

                    function start() {
                        if (curStep >= 99) {
                            clearInterval(intervalId);
                            $("#downstep").attr("disabled", true);

                        }
                        curStep++;
                        funGetColorData(curStep);
                        ShowStep(curStep);
                    }

                    function Stop() {
                        clearInterval(intervalId);
                        curStep = 0;
                        ShowStep(curStep);
                    }

                    function Pause() {
                        clearInterval(intervalId);
                        curStep = 0;
                        ShowStep(curStep);
                    }

                    function ShowStep(step) {
                        $("#curStep").text(step);
                    }


                </script>
                <div>
                <button id="upstep" onclick="UpStep()">上一步</button>
                <button id="stop" onclick="Stop()">停止</button>
                <button id="play" onclick="Play()">播放</button>
                <button id="pause" onclick="Pause()">暂停</button>
                <button id="downstep" onclick="DownStep()">下一步</button>
                <label id="curStep"></label>
                </div>
                <div id="canvas-frame"></div>
