using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace arrow123
{
    internal class Arrow
    {
        private int startX;
        private int startY;
        private int endX;
        private int endY;
        private double Length;
        private double a_V;

        public Arrow(int startX, int startY, int endX, int endY, double AV)
        {
            this.startX = startX;
            this.startY = startY;
            this.endX = endX;
            this.endY = endY;
            this.Length = Math.Sqrt(Math.Pow(endX - startX, 2) + Math.Pow(endY - startY,2));
            this.a_V = AV;
        }

        public void SetStartX(int startX) { this.startX = startX; }
        public void SetStartY(int startY) { this.startY = startY; }
        public void SetEndY(int endY) { this.endY = endY; }
        public void SetEndX(int endX) { this.endX = endX; }
        public int GetStartX() { return this.startX; }
        public int GetStartY() { return this.startY; }
        public int GetEndX() { return  this.endX; }
        public int GetEndY() { return this.endY; }
        public double GetAngularVelocity() { return this.a_V; }

        public double GetLength() { return this.Length; }

        public void DrawArrow()
        {
            Raylib.DrawLine(startX, startY, endX, endY, Color.RayWhite);
        }

        public void RotateArrow(double theta)
        {
            this.endX = (int)(this.Length * Math.Cos(theta) + this.startX);
            this.endY = (int)(this.Length * Math.Sin(theta) + this.startY);
        }
    }
}
