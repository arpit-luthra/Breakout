using System;
using System.Drawing;


namespace Breakout
{
    internal class Paddle
    {
        //*************************************************************
        //Fields
        //*************************************************************
        private int mWidth;
        private int mHeight;

        //fields -- for the paddle to draw 2 corner bullet firing teeth 
        private bool mHasCornerShots = false;

        //*************************************************************
        //Constructors
        //*************************************************************
        public Paddle()
        {
            mWidth = 100;
            mHeight = 10;
        }

        //*************************************************************
        //Properties
        //*************************************************************
        public int Width
        {
            get { return mWidth; }
            set { mWidth = value; }
        }

        public int Height
        {
            get { return mHeight; }
        }
        public bool HasCornerShots
        {
            get { return mHasCornerShots; }
            set { mHasCornerShots = value; }
        }

        //*************************************************************
        //Methods
        //*************************************************************
        public void Draw(Graphics g, int x, int y, int screenWidth)
        {
            //this draws the Paddle on the surface g at
            //the location x and y 

            // Clamp x so paddle never draws outside screen
            int drawX = Math.Max(0, Math.Min(x, screenWidth - mWidth));

            g.FillRectangle(Brushes.White, x, y, mWidth, mHeight);

            // draw corner small vertical 'tooths' if corner shots active
            if (mHasCornerShots)
            {
                Pen p = new Pen(Color.White, 2);
                int toothHeight = 6;
                int toothInset = 6; // distance from corner
                // left tooth
                int lx = drawX + toothInset;
                g.DrawLine(p, lx, y, lx, y - toothHeight);
                // right tooth
                int rx = drawX + mWidth - toothInset;
                g.DrawLine(p, rx, y, rx, y - toothHeight);
            }
        }

    }
}
