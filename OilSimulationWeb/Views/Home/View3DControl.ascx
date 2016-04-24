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
        		<script>
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

        		        //                camera = new THREE.OrthographicCamera(-2, 2, 1.5, -1.5, 1, 10);
        		        //                camera.position.set(0, 0, 5);
        		        camera = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 1, 10000);//透视
        		        camera.position.z = 1000;

//        		        camera = new THREE.OrthographicCamera(0, 600, 3000, 0, 1, 5000);//正交投影，
//        		        camera.position.set(500, 3000, 4000);
//        		        
        		        //scene.add(camera);

        		        //                //camera = new THREE.PerspectiveCamera(45, width / height, 1, 10000);//透
        		        //                camera = new THREE.OrthographicCamera(-1, 4, 2, -1,1,10); //正交
        		        //              camera.position.x = 0;
        		        //                camera.position.y = 1;
        		        //                camera.position.z = 5;
        		        ////                camera.up.x = 4;
        		        ////                camera.up.y = 3;
        		        ////                camera.up.z = 3;
        		        //                camera.lookat({
        		        //                    x : 0,
        		        //                    y : 0,
        		        //                    z : 0
        		        //                });

        		        camera.lookAt(new THREE.Vector3(0, 0, 0));//视野中心坐标
        		    }

        		    var scene;
        		    function initScene() {
        		        scene = new THREE.Scene();
        		    }

        		    var light;
        		    function initLight() {
        		        light = new THREE.PointLight(0x00FF00);
        		        //light.position.set(5, 5, 5);
        		        //scene.add(light);
        		    }

        		    var cube;
        		    var mesh;
        		    var varCubeGeometry;

                    //加载模型
        		    function initObject(x, y, z, color) {
        		        varCubeGeometry = new THREE.CubeGeometry(100, 100, 100);
        		        //group = new THREE.Group();
        		        //varCubeGeometry.position.x = 1 + a;
        		        cube = new THREE.Mesh(varCubeGeometry,
                                                    new THREE.MeshBasicMaterial({
                                                        color: color

                                                    }));
        		        cube.position.x = x;
        		        cube.position.z = y;
        		        cube.position.y = z;
        		        //group.add(cube);
        		        scene.add(cube);

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

                     

//                    function initObject() {

//                        //CubeGeometry  BoxGeometry
//                        for (var a = 0; a < 10; a++) {
//                            for (var b = 0; b < 10; b++) {
//                                for (var c = 0; c < 10; c++) {

//                                    varCubeGeometry = new THREE.CubeGeometry(0.01, 0.01, 0.01);
//                                    //varCubeGeometry.position.x = 1 + a;
//                                    cube = new THREE.Mesh(varCubeGeometry,
//                                                    new THREE.MeshBasicMaterial({
//                                                        color: 0xff0000 * Math.random()

//                                                    }));
//                                    cube.position.x = a * 0.01;
//                                    cube.position.z = b * 0.01;
//                                    cube.position.y = c * 0.01;
//                                    scene.add(cube);
//                                }
//                            }
//                        }
//                    }

                    //帧情况
                    function InitStats() {
                        stats = new Stats();
                        stats.domElement.style.position = 'absolute';
                        stats.domElement.style.top = '0px';
                        frame.appendChild(stats.domElement);

                    }

        		    var controls;
        		    function InitControl() {
        		        controls = new THREE.OrbitControls(camera, renderer.domElement);
        		    }


        		    function funGetModelData() {
        		        $.ajax({
        		            type: "post",
        		            url: "/Business/GetModelData",
        		            dataType: "json",
        		            success: function (data) {
        		                var list = data.list; //模型列表数据
        		                var info, color;
        		                //alert(list.length);
        		                for (var i = 0; i < list.length; i++) {
        		                    info = CaculateColor(255, 0, 0, 0, 0, 255, list[i].Color, list[i].MaxColor, list[i].MinColor);
        		                    if (info) {
        		                        color = (info["R"] << 16) | (info["G"] << 8) | info["B"];
        		                    }
        		                    initObject(list[i].X, list[i].Y, list[i].Z, color);
        		                }
        		            }
        		        });
        		    }




        		    function threeStart() {
        		        
        		        initThree();
        		        initCamera();
        		        initScene();
        		        initLight();
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
        		        //camera.toTopView();

        		    }
        		    function onWindowResize() {
        		        camera.aspect = window.innerWidth / window.innerHeight;
        		        camera.updateProjectionMatrix();
        		        renderer.setSize(window.innerWidth, window.innerHeight);
        		    }

        		    function animation() {

        		        //                cube.rotation.y += 0.01;
        		        //                if (cube.rotation.y > Math.PI * 2) {
        		        //                    cube.rotation.y -= Math.PI * 2;
        		        //                }
        		        renderer.render(scene, camera);
        		        requestAnimationFrame(animation);
        		        stats.update(); //帧情况


        		        //controls.update();
        		    }



                </script>
                <div><button onclick="funGetModelData">sdf</button></div>
                <div id="canvas-frame"></div>
