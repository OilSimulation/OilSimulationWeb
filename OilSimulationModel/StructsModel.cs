using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Threading;


namespace OilSimulationModel
{
    /// <summary>
    /// RGB STRUCT
    /// </summary>
    public struct ColorRGB
    {

        public byte R;

        public byte G;

        public byte B;

        public ColorRGB(Color value)
        {

            this.R = value.R;

            this.G = value.G;

            this.B = value.B;

        }

        public static implicit operator Color(ColorRGB rgb)
        {

            Color c = Color.FromArgb(rgb.R, rgb.G, rgb.B);

            return c;

        }

        public static explicit operator ColorRGB(Color c)
        {

            return new ColorRGB(c);

        }

    }

    /// <summary>
    /// 模型数据
    /// </summary>
    public struct ModeData
    {
        public float[] mm;
        public float[] xyz;
        public List<float[]> Data;
    }

    /// <summary>
    /// 颜色数据
    /// </summary>
    public struct ColorData
    {
        public float[] mm;
        public float[] xyz;
        public List<float> Data;
    }


    /// <summary>
    /// 线程结构
    /// </summary>
    public struct stMultiTread
    {
        public string szUserMac;
        public string szFilePath;
        public bool   bThreadBusy;
        public Thread thread;
    }

    public struct stCubeInfo
    { 
        public float[] ct { get; set; } 
    }

    public struct stDrawInfo
    {
        public float[] a { get; set; }
        public float[] b { get; set; }
        public float[] c { get; set; }
        public float[] d { get; set; }
        public float[] e { get; set; }
        public float[] f { get; set; }
        public float[] g { get; set; }
        public float[] h { get; set; }
        public float v { get; set; }
    }

    public class View3DPoint
    {
        private float x;

        public float X
        {
            get { return x; }
            set { x = value; }
        }
        private float y;

        public float Y
        {
            get { return y; }
            set { y = value; }
        }
        private float z;

        public float Z
        {
            get { return z; }
            set { z = value; }
        }

        private float xWidth;

        public float XWidth
        {
            get { return xWidth; }
            set { xWidth = value; }
        }
        private float yWidth;

        public float YWidth
        {
            get { return yWidth; }
            set { yWidth = value; }
        }
        private float zWidth;

        public float ZWidth
        {
            get { return zWidth; }
            set { zWidth = value; }
        }

        private float color;

        public float Color
        {
            get { return color; }
            set { color = value; }
        }

        private float minColor;

        public float MinColor
        {
            get { return minColor; }
            set { minColor = value; }
        }
        private float maxColor;

        public float MaxColor
        {
            get { return maxColor; }
            set { maxColor = value; }
        }
    }
     
}
