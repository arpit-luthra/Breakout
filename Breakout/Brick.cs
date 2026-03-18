using System;
using System.Drawing;

namespace Breakout
{
    //*************************************************************
    //Enums
    //*************************************************************
    public enum BrickTypes { None, Regular, Special };
    public enum BrickColours { None, Yellow, Green, Orange, Red };

    internal class Brick
    {
        //*************************************************************
        //Fields
        //*************************************************************
        private BrickTypes mBrickType;
        private BrickColours mBrickColour;
        private bool mDestroyed;
        private int mHitsRemaining;

        //fields -- normal colors and darker colors to implement 2-hit brick feature
        private Brush NormalYellow;
        private Brush NormalGreen;
        private Brush NormalOrange;
        private Brush NormalRed;

        private Brush DarkYellow;
        private Brush DarkGreen;
        private Brush DarkOrange;
        private Brush DarkRed;

        //field -- for bricks that have a plus sign
        private bool mHasPlusPowerUp;

        //field & property-- for bricks that have ball inside
        public bool HasExtraBall { get; set; } = false;

        //field & property -- for bricks that have heart inside
        public bool HasHeart { get; set; } = false;

        //*************************************************************
        //Constructors
        //*************************************************************
        public Brick()
        {
            mBrickType = BrickTypes.None;
            mBrickColour = BrickColours.None;
            mDestroyed = false;
            mHitsRemaining = 1;

            //normal colors
            NormalYellow = Brushes.Yellow;
            NormalGreen = Brushes.LimeGreen;
            NormalOrange = Brushes.Orange;
            NormalRed = Brushes.Red;

            // Darker colours for 2-hit bricks
            DarkYellow = new SolidBrush(Color.DarkGoldenrod);
            DarkGreen = new SolidBrush(Color.DarkGreen);
            DarkOrange = new SolidBrush(Color.Chocolate);
            DarkRed = new SolidBrush(Color.DarkRed);
        }

        //*************************************************************
        //Properties
        //*************************************************************
        public BrickTypes BrickType
        {
            get { return mBrickType; }
            set { mBrickType = value; }
        }

        public BrickColours BrickColour
        {
            get { return mBrickColour; }
            set { mBrickColour = value; }
        }

        public bool Destroyed
        {
            get { return mDestroyed; }
            set { mDestroyed = value; }
        }

        public int HitsRemaining
        {
            get { return mHitsRemaining; }
            set { mHitsRemaining = value; }
        }

        public bool HasPlusPowerUp
        {
            get { return mHasPlusPowerUp; }
            set { mHasPlusPowerUp = value; }
        }

        //*************************************************************
        //Methods
        //*************************************************************

        //method that returns the darker 2-hit bricks to normal color
        private Brush GetNormalBrush()
        {
            if (mBrickColour == BrickColours.Yellow)
                return NormalYellow;

            if (mBrickColour == BrickColours.Green)
                return NormalGreen;

            if (mBrickColour == BrickColours.Orange)
                return NormalOrange;

            if (mBrickColour == BrickColours.Red)
                return NormalRed;

            return Brushes.White; 
        }

        //method that makes the brick the darker shade
        private Brush GetDarkBrush()
        {
            if (mBrickColour == BrickColours.Yellow)
                return DarkYellow;

            if (mBrickColour == BrickColours.Green)
                return DarkGreen;

            if (mBrickColour == BrickColours.Orange)
                return DarkOrange;

            if (mBrickColour == BrickColours.Red)
                return DarkRed;

            return Brushes.White;
        }
        public void Draw(Graphics g, int x, int y)
        {
            //this draws the Brick on the surface g at
            //the location x and y 
            if (mBrickType == BrickTypes.None || mDestroyed)
                return;

            // If this is a 2-hit brick AND it hasn't been hit yet -- dark colour
            Brush brush;
            if (mBrickType == BrickTypes.Special && mHitsRemaining > 1)
                brush = GetDarkBrush();
            else
                brush = GetNormalBrush();

            g.FillRectangle(brush, x, y, 60, 20);
            g.DrawRectangle(Pens.Black, x, y, 60, 20);

            // brick that has a plus sign in the middle of it
            if (mHasPlusPowerUp && !mDestroyed)
            {
                // Center of brick
                int centerX = x + 30; // half of brick width (60)
                int centerY = y + 10; // half of brick height (20)

                int thickness = 2;
                int outlineThickness = thickness + 2;
                int length = 5;

                //Black outline
                Pen outlinePen = new Pen(Color.Black, outlineThickness);
                g.DrawLine(outlinePen, centerX - length, centerY, centerX + length, centerY);
                g.DrawLine(outlinePen, centerX, centerY - length, centerX, centerY + length);

                //white color plus
                Pen plusPen = new Pen(Color.White, thickness);
                g.DrawLine(plusPen, centerX - length, centerY, centerX + length, centerY);
                g.DrawLine(plusPen, centerX, centerY - length, centerX, centerY + length);

                outlinePen.Dispose();
                plusPen.Dispose();
            }

            //brick that has ball inside
            if (HasExtraBall)
            {
                int ballSize = 10;

                int centerX = x + (60 / 2) - (ballSize / 2);
                int centerY = y + (20 / 2) - (ballSize / 2);

                Brush b = new SolidBrush(Color.LightBlue); // same color as static paddle
                g.FillEllipse(b, centerX, centerY, ballSize, ballSize);
                
                b.Dispose();
            }

            //brick that has heart inside
            if (HasHeart && !mDestroyed)
            {
                int centerX = x + 30;
                int centerY = y + 10;

                Brush heartBrush = Brushes.Pink;

                // simple heart using 2 circles + triangle illusion
                g.FillEllipse(heartBrush, centerX - 6, centerY - 4, 6, 6);
                g.FillEllipse(heartBrush, centerX, centerY - 4, 6, 6);
                
                //using the built in point struct that represents the x,y coordinated
                Point[] triangle =
                {
                    new Point(centerX - 7, centerY - 1),
                    new Point(centerX + 7, centerY - 1),
                    new Point(centerX, centerY + 6)
                };
                g.FillPolygon(heartBrush, triangle);
            }
        }

    }
}
