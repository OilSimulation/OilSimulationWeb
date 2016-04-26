
//计算当前方格颜色.
//selMaxColor用户选的颜色最大值,
function CaculateColor(selMaxColorR, selMaxColorG, selMaxColorB, selMinColorR, selMinColorG, selMinColorB, curColor, maxColor, minColor) {
    var h, s, l, H, S, L;
    var minInfo = RGB2HSL(selMinColorR, selMinColorG, selMinColorB);
    var maxInfo = RGB2HSL(selMaxColorR, selMaxColorG, selMaxColorB);
    h = minInfo["H"];
    s = minInfo["S"];
    l = minInfo["L"];

    H = maxInfo["H"];
    S = maxInfo["S"];
    L = maxInfo["L"];

    var info;

    if (0.0 == maxColor || curColor == maxColor) {
        info = { "R": selMaxColorR, "G": selMaxColorG, "B": selMaxColorB };
        return info;
    }
    else {
        if (minColor == curColor) {
            info = { "R": selMinColorR, "G": selMinColorG, "B": selMinColorB };
            return info;
        }
        var fStep = (curColor - minColor) / (maxColor - minColor) * (H - h) + h;

        if (fStep < 0.0) {
            fStep += 1;
        }
        return HSL2RGB(fStep, s, l);

    }
}

//返回 R:VALUE,G:VALUE,B:VALUE
function HSL2RGB(h, sl, l) {
    var v;
    var r, g, b;
    r = l;   // default to gray
    g = l;
    b = l;
    v = (l <= 0.5) ? (l * (1.0 + sl)) : (l + sl - l * sl);
    if (v > 0) {
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
        switch (sextant) {

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
    var info = { "R": parseInt(r * 255), "G": parseInt(g * 255), "B": parseInt(b * 255) };
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
                g.faces[i + j].vertexColors[1].setHex(c[colorIndex]);
                g.faces[i + j].vertexColors[2].setHex(c[colorIndex]);
            }
        }
    }
    g.colorsNeedUpdate = true;
}