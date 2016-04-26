/**
 * @author mrdoob / http://mrdoob.com/
 */

THREE.MyLoader = function ( manager ) {

	this.manager = ( manager !== undefined ) ? manager : THREE.DefaultLoadingManager;

};

THREE.MyLoader.prototype = {

    constructor: THREE.MyLoader,

    load: function (url, pData, onLoad, onProgress, onError) {

        var scope = this;

        var loader = new THREE.XHRLoader(scope.manager);
        loader.setCrossOrigin(this.crossOrigin);
        loader.load(url, pData, function (text) {

            onLoad(scope.parse(text));

        }, onProgress, onError);

    },

    parse: function (text) {

        console.time('MyLoader');

        var vertices = [];

        var jsonData = JSON.parse(text);

        var container = new THREE.Object3D();

        var geometry = new THREE.Geometry();
        var cubeMaterial = new THREE.MeshNormalMaterial({ color: 0x0f00f1, transparent: true, opacity: 0.5 });
        var iLeive = 3;

        for (var i = 0; i < jsonData.Data.length; i++) {
            var zWidth = 20;
            if (i == 1000) {
                zWidth = 30;
            }
            if (i == 2000) {
                zWidth = 50;
            }
            var cubeMesh = addcube(jsonData.Data[i][0] - 400, jsonData.Data[i][1] - 500, jsonData.Data[i][2] - 3010, zWidth);
            cubeMesh.updateMatrix();
            geometry.merge(cubeMesh.geometry, cubeMesh.matrix);
        }

        function addcube(x, y, z, zw) {
            var cubeSize = 10.0;
            var cubeGeometry = new THREE.BoxGeometry(cubeSize, cubeSize, zw);

            var cube = new THREE.Mesh(cubeGeometry, cubeMaterial);
            cube.castShadow = true;

            // position the cube randomly in the scene
            cube.position.x = x;
            cube.position.y = y;
            cube.position.z = z;

            // add the cube to the scene
            return cube;
        }
        container.add(new THREE.Mesh(geometry, cubeMaterial));

        console.timeEnd('MyLoader');

        return container;
    }

};
