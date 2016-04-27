
// once everything is loaded, we run our Three.js stuff.
function init() {

    // 创建和设置渲染器
    webGLRenderer = new THREE.WebGLRenderer({ antialias: true });  //抗锯齿
    webGLRenderer.setClearColor(new THREE.Color(0x666666, 1.0));
    webGLRenderer.setSize(window.innerWidth, window.innerHeight);
    webGLRenderer.shadowMapEnabled = true;

    // 创建场景, that will hold all our elements such as objects, cameras and lights.
    scene = new THREE.Scene();

    var axes = new THREE.AxisHelper(5);
    scene.add(axes);

    // 创建和设置相机
    camera = new THREE.PerspectiveCamera(45, window.innerWidth / window.innerHeight, 0.1, 50000);
    // position and point the camera to the center of the scene
    camera.position.set(200, 0, 0);
    // 相机的上方向设置为(0,1,0)即y轴方向;
    camera.up.set(0, 1, 0);
    // 相机的视野中心坐标
    //camera.lookAt(new THREE.Vector3(0, 0, 0));
    //camera.lookAt({x:0, y:0, z:0});
    camera.lookAt(scene.position);
    //scene.add(camera);
     
    //OrbitControls
    var controls = new THREE.OrbitControls(camera, webGLRenderer.domElement);
    //controls.target.set(0, 0, 0); 

    var stats = initStats();

    // 设置环境光源
    var ambi = new THREE.AmbientLight(0x33ccff);
    scene.add(ambi);

    // 设置聚光灯
    var spotLight = new THREE.DirectionalLight(0xffffff);
    spotLight.position.set(0, 0, 0);
    spotLight.intensity = 0.5;
    scene.add(spotLight);

    // 导入模型并添加到scene中
    var mesh;

    var loader = new THREE.MyLoader();

    // add the output of the renderer to the html element
    document.getElementById("WebGL-output").appendChild(webGLRenderer.domElement);
         
    // call the render function
    var step = 0;
    var clock = new THREE.Clock(); 
         
    render();

    var reload = true;

    function render() {
        stats.update();
        var delta = clock.getDelta();

        if (reload) {
            reload = false; 
            var sUrl = "<%:Url.Action("GetData","Business") %>";
            loader.load(sUrl, JSON.stringify({Para:"PRESSURE",Mode:1,Step:7}) ,function (loadedMesh) {
                // loadedMesh is a group of meshes. For
                // each mesh set the material, and compute the information
                // three.js needs for rendering.
                loadedMesh.children.forEach(function (child) {
                    //child.material = material;
                    //child.geometry.computeFaceNormals();
                    //child.geometry.computeVertexNormals();
                });

                mesh = loadedMesh;

                var scale = 0.1;
                console.log("scale =", scale);
                loadedMesh.scale.set(scale, scale, scale);
                //loadedMesh.rotation.x = -0.3;
                scene.add(mesh);
            });
        }
            
        controls.update();
        //trackballControls.update(delta);
        // render using requestAnimationFrame
        camera.lookAt(new THREE.Vector3(0, 0, 0));

        requestAnimationFrame(render);
        webGLRenderer.render(scene, camera);

    }

    function initStats() {

        var stats = new Stats();

        stats.setMode(0); // 0: fps, 1: ms

        // Align top-left
        stats.domElement.style.position = 'absolute';
        stats.domElement.style.left = '0px';
        stats.domElement.style.top = '0px';

        document.getElementById("Stats-output").appendChild(stats.domElement);

        return stats;
    }

}

function onResize() {
    camera.aspect = window.innerWidth / window.innerHeight;
    camera.updateProjectionMatrix();
    webGLRenderer.setSize(window.innerWidth, window.innerHeight);
}