using System.Drawing;
using System.Windows;

namespace Breakout
{
    public class FallingHeart
    {
        //Fields
        public Vector Position;
        public Vector Velocity;

        public int Size = 16;
        public int BounceCount = 0;
        public bool Active = true;
        public int ScreenWidth;

        public double Gravity = 0.03;

        //Constructor
        public FallingHeart(double x, double y)
        {
            Position = new Vector(x, y);
            Velocity = new Vector(0, 1); // start falling
        }

        //Methods
        public void Update()
        {
            if (!Active) return;

            // gravity
            Velocity = new Vector(Velocity.X, Velocity.Y + Gravity);

            // limit fall speed
            if (Velocity.Y > 3)
                Velocity = new Vector(Velocity.X, 3);

            // move
            Position += Velocity;

            // left wall
            if (Position.X <= 0)
            {
                Position = new Vector(0, Position.Y);
                Velocity = new Vector(-Velocity.X, Velocity.Y);
            }

            // right wall
            if (Position.X + Size >= ScreenWidth)
            {
                Position = new Vector(ScreenWidth - Size, Position.Y);
                Velocity = new Vector(-Velocity.X, Velocity.Y);
            }
        }


        //paddle bounce
        public void BounceOffPaddle(double paddleCenterX)
        {
            if (!Active) return;

            // Horizontal angle based on where it hits the paddle
            double dx = (Position.X - paddleCenterX) * 0.08;

            //upward bounce
            Velocity = new Vector(dx, -3.5);

            BounceCount++;

            // After 4 bounces -- give a life to user
            if (BounceCount >= 4)
            {
                Active = false;
            }
        }


        //draw
        public void Draw(Graphics g)
        {
            if (!Active) return;

            int x = (int)Position.X;
            int y = (int)Position.Y;

            // Top left heart circle
            g.FillEllipse(Brushes.Red, x, y, Size / 2, Size / 2);

            // Top right heart circle
            g.FillEllipse(Brushes.Red, x + Size / 2, y, Size / 2, Size / 2);

            // Bottom triangle
            g.FillPolygon(
                Brushes.Red,
                new System.Drawing.Point[]
                {
                    new System.Drawing.Point(x, y + Size / 4),
                    new System.Drawing.Point(x + Size, y + Size / 4),
                    new System.Drawing.Point(x + Size / 2, y + Size)
                }
            );
        }
    }
}
