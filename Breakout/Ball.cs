using System;
using System.Drawing;
using System.Windows;


namespace Breakout
{
    //*************************************************************
    //Enums
    //*************************************************************

    internal class Ball
    {
        //*************************************************************
        //Fields
        //*************************************************************
        private Vector mPosition;
        private Vector mVelocity;
        private int Speed;
        private int mSize;
        private double mCurrentSpeed;

        //*************************************************************
        //Constructors
        //*************************************************************
        public Ball()
        {
            mPosition = new Vector(0, 0);
            mVelocity = new Vector(0, 0);
            Speed = 4;
            mSize = 18;
            mCurrentSpeed = Speed;
        }

        //*************************************************************
        //Properties
        //*************************************************************
        public Vector Position
        {
            get { return mPosition; }
            set { mPosition = value; }
        }

        public Vector Velocity
        {
            get { return mVelocity; }
            set { mVelocity = value; }
        }

        public int BallSpeed
        {
            get { return Speed; }
        }
        public int Size
        {
            get { return mSize; }
        }
        public double CurrentSpeed
        {
            get { return mCurrentSpeed; }
            set { mCurrentSpeed = value; }
        }

        //*************************************************************
        //Methods
        //*************************************************************

        
        public void Draw(Graphics g, int x, int y)
        {
            //this draws the Ball on the surface g at
            //the location x and y 
            g.FillEllipse(Brushes.White, x, y, mSize, mSize);
        }

    }
}
