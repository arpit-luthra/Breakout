using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Breakout
{
    internal class PlusPowerUp
    {
        //fields & properties
        public Vector Position { get; set; }
        public int Size { get; set; } = 15;
        public bool Active { get; set; } = true;
        
        private double speed = 3; 

        //Constructor
        public PlusPowerUp(Vector startPos)
        {
            Position = startPos;
        }

        //methods
        //Move the power-up down
        public void Update()
        {
            if (Active)
            {
                Position = new Vector(Position.X, Position.Y + speed);
            }
        }

        //Draw the + sign
        public void Draw(Graphics g)
        {
            if (!Active) return;

            int centerX = (int)Position.X;
            int centerY = (int)Position.Y;
            Pen pen = new Pen(Color.White, 2);
            g.DrawLine(pen, centerX - 5, centerY, centerX + 5, centerY);
            g.DrawLine(pen, centerX, centerY - 5, centerX, centerY + 5);
        }
    }
}
