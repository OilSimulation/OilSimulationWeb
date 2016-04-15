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
        		        //        		        renderer = new THREE.WebGLRenderer({
        		        //        		            antialias: true
        		        //        		        });//显卡渲染

        		        renderer = new THREE.CanvasRenderer({
        		            antialias: true
        		        });//软件渲染
        		        renderer.setSize(width, height);
        		        document.getElementById('canvas-frame').appendChild(renderer.domElement);
        		        //renderer.setClearColorHex(0xFFFFFF, 1.0);
        		    }

        		    var camera;
        		    function initCamera() {

        		        //                camera = new THREE.OrthographicCamera(-2, 2, 1.5, -1.5, 1, 10);
        		        //                camera.position.set(0, 0, 5);

        		        camera = new THREE.OrthographicCamera(-2, 200, 200, -1.5, 1, 3000);//正交投影，
        		        camera.position.set(4, -3, 3000);
        		        
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

        		        camera.lookAt(new THREE.Vector3(0, 0, 0));
        		    }

        		    var scene;
        		    function initScene() {
        		        scene = new THREE.Scene();
        		    }

        		    var light;
        		    function initLight() {
        		        light = new THREE.PointLight(0x00FF00);
        		        light.position.set(5, 5, 5);
        		        scene.add(light);
        		    }

        		    var cube;
        		    var mesh;
        		    var varCubeGeometry;

                    //加载模型
        		    function initObject(x, y, z, color) {
        		        varCubeGeometry = new THREE.CubeGeometry(10, 10, 10);
        		        //varCubeGeometry.position.x = 1 + a;
        		        cube = new THREE.Mesh(varCubeGeometry,
                                                    new THREE.MeshBasicMaterial({
                                                        color: 0xff0000 * Math.random()

                                                    }));
        		        cube.position.x = x;
        		        cube.position.z = y;
        		        cube.position.y = z;
        		        scene.add(cube);

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
        		                alert(list.length);
        		                for (var i = 0; i < list.length; i++) {

        		                    initObject(list[i].X, list[i].Y, list[i].Z, list[i].Color);
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
