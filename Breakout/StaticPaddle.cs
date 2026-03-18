using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout
{
    internal class StaticPaddle
    {
        //fields and properties
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; } = 50; 
        public int Height { get; set; } = 10;
        public bool Active { get; set; } = true;

        public void Draw(Graphics g)
        {
            if (Active)
                g.FillRectangle(Brushes.LightBlue, X, Y, Width, Height);
        }
    }

}