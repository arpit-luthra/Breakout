using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Breakout
{
    internal class FallingTriangle
    {
        //fields
        public Vector Position;
        public Vector Velocity;
        public int Size;
        public bool Active = true;

        //constructor
        public FallingTriangle(double x, double y)
        {
            Position = new Vector(x, y);
            Velocity = new Vector(0, 6);   
            Size = 18;
        }

        //methods
        public void Update()
        {
            if (!Active) return;
            Position += Velocity;
        }

        public void Draw(Graphics g)
        {
            if (!Active) return;    

            int x = (int)Position.X;
            int y = (int)Position.Y;

            //using built in Point struct again (added point with the namespace since assembly not supported)
            System.Drawing.Point[] triangle = 
            {
                new System.Drawing.Point(x, y + Size),
                new System.Drawing.Point(x + Size / 2, y),
                new System.Drawing.Point(x + Size, y + Size)
            };

            g.FillPolygon(Brushes.Purple, triangle);
        }
    }
}
