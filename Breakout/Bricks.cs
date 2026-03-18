using System;
using System.Drawing;

namespace Breakout
{
    internal class Bricks
    {
        //fields
        private Brick[,] mBricks;
        private Random mRandom;

        //contructor
        public Bricks(int Level = 1)
        {
            mBricks = new Brick[8, 10];
            mRandom = new Random();

            //create base grid
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 10; c++)
                {
                    Brick brick = new Brick();
                    brick.BrickType = BrickTypes.Regular;

                    if (r < 2) brick.BrickColour = BrickColours.Red;
                    else if (r < 4) brick.BrickColour = BrickColours.Orange;
                    else if (r < 6) brick.BrickColour = BrickColours.Green;
                    else brick.BrickColour = BrickColours.Yellow;

                    mBricks[r, c] = brick;
                }
            }

            //2-hit bricks (Probability based)
            if (Level >= 2)
            {
                double specialChance = 0.12 + (Level - 2) * 0.06;

                for (int r = 0; r < 8; r++)
                {
                    for (int c = 0; c < 10; c++)
                    {
                        Brick brick = mBricks[r, c];

                        if (brick.BrickType == BrickTypes.Regular &&
                            mRandom.NextDouble() < specialChance)
                        {
                            brick.BrickType = BrickTypes.Special;
                            brick.HitsRemaining = 2;
                        }
                    }
                }
            }
            
            //Plus power-up bricks
            if (Level >= 2)
            {
                double plusChance = 0.08;

                for (int r = 0; r < 8; r++)
                {
                    for (int c = 0; c < 10; c++)
                    {
                        Brick brick = mBricks[r, c];

                        if (!brick.HasPlusPowerUp &&
                            !brick.HasExtraBall &&
                            !brick.HasHeart &&
                            mRandom.NextDouble() < plusChance) //using NextDouble which returns a random floating number
                        {
                            brick.HasPlusPowerUp = true;
                        }
                    }
                }
            }

            
            //Extra ball bricks
            if (Level >= 2)
            {
                int assigned = 0;
                int target = mRandom.Next(2, 5);

                while (assigned < target)
                {
                    int r = mRandom.Next(0, 8);
                    int c = mRandom.Next(0, 10);

                    Brick brick = mBricks[r, c];

                    if (!brick.HasExtraBall &&
                        !brick.HasPlusPowerUp &&
                        !brick.HasHeart)
                    {
                        brick.HasExtraBall = true;
                        assigned++;
                    }
                }
            }

            
            //Heart Bricks (Only 1 or 2)
            if (Level >= 2)
            {
                int heartCount = mRandom.Next(0, 2) == 0 ? 1 : 2;
                int assigned = 0;

                while (assigned < heartCount)
                {
                    int r = mRandom.Next(0, 8);
                    int c = mRandom.Next(0, 10);

                    Brick brick = mBricks[r, c];

                    if (!brick.HasHeart &&
                        !brick.HasPlusPowerUp &&
                        !brick.HasExtraBall)
                    {
                        brick.HasHeart = true;
                        assigned++;
                    }
                }
            }
        }

        //properties
        //read-only
        public Brick[,] BrickArray
        {
            get
            {
                return mBricks;
            }
        }

        public int TotalWidth
        {
            get
            {
                // 10 bricks wide, 60px each, 9 gaps of 5px
                return (10 * 60) + (9 * 5);
            }
        }

        public int TotalHeight
        {
            get
            {
                // 8 bricks tall, 20px each, 7 gaps of 5px
                return (8 * 20) + (7 * 5);
            }
        }

        //methods
        public void Draw(Graphics g, int x, int y)
        {
            int brickWidth = 60;
            int brickHeight = 20;
            int spacing = 5;

            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 10; c++)
                {
                    int drawX = x + c * (brickWidth + spacing);
                    int drawY = y + r * (brickHeight + spacing);

                    mBricks[r, c].Draw(g, drawX, drawY);
                }
            }
        }
    }
}
