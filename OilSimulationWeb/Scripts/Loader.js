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


        for (var i = 0; i < jsonData.Data.length; i++) {

            var xw, yw, zw;
            xw = jsonData.xyz[i / (jsonData.Data.length / 3)][0];
            yw = jsonData.xyz[i / (jsonData.Data.length / 3)][1];
            zw = jsonData.xyz[i / (jsonData.Data.length / 3)][2];
            var cubeMesh = addcube(jsonData.Data[i][0] - 400, jsonData.Data[i][1] - 500, jsonData.Data[i][2] - 3010, xw, yw, zw);
            cubeMesh.updateMatrix();
            geometry.merge(cubeMesh.geometry, cubeMesh.matrix);
        }

        function addcube(x, y, z, xw, yw, zw) {
            var cubeGeometry = new THREE.BoxGeometry(xw, yw, zw);

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
