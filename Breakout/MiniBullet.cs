using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Breakout
{
    internal class MiniBullet
    {
        //fields and properties
        //could be public -- no need to encapsulate
        public Vector Position { get; set; }
        public Vector Velocity { get; set; }
        public int Size { get; set; } = 6;
        public bool Active { get; set; } = true;

        //contructor
        public MiniBullet(double startX, double startY, double velX = 0, double velY = -6)
        {
            Position = new Vector(startX, startY);
            Velocity = new Vector(velX, velY);
        }

        //methods
        public void Update()
        {
            if (!Active) return;
            Position = new Vector(Position.X + Velocity.X, Position.Y + Velocity.Y);
        }

        public void Draw(Graphics g)
        {
            if (!Active) return;
            
            // draw small circle
            //ellipse requires conversion to float type
            g.FillEllipse(Brushes.White, (float)Position.X - Size / 2f, (float)Position.Y - Size / 2f, Size, Size);
        }

    }
}
