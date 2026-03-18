using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

//Arpit Luthra
//January 16, 2026
//BreakOut -- Modified
//*************************************************************
//TO SKIP TO LEVEL 2 TO TEST POWERUPS, SCROLL DOWN TO FORM1-KEYDOWN AND UNCOMMENT THE FIRST BLOCK OF CODE
//*************************************************************
namespace Breakout
{
    //*************************************************************
    //enums
    //*************************************************************
    //plus sign - power up type
    public enum PowerUpType
    {
        None,
        DoublePaddle,
        CornerShots,
        ExtraPaddles

    }
    public partial class Form1 : Form
    {
        //*************************************************************
        //Fields
        //*************************************************************
        Paddle paddle;
        Ball ball;
        Bricks bricks;
        Menu menu;

        //fields -- for power-up plus sign
        private int originalPaddleWidth;
        private bool paddlePowered = false;
        private Timer paddlePowerTimer;
        private Timer staticPaddleTimer;
        private Timer slowPaddleTimer;
        private int normalPaddleSpeed;
        private Random mRandom = new Random();
        private PowerUpType currentPowerUp = PowerUpType.None;
        
        //Lists to keep track of features and items
        private List<PlusPowerUp> activePlusSigns = new List<PlusPowerUp>(); //keep track of plus signs
        private List<MiniBullet> activeBullets = new List<MiniBullet>(); //keep track of bullets
        private List<StaticPaddle> activeStaticPaddles = new List<StaticPaddle>(); //keep track of static paddles
        private List<Ball> activeExtraBalls = new List<Ball>(); //keep track of extra balls
        private List<FallingHeart> fallingHearts = new List<FallingHeart>(); //keep track of hearts
        private List<FallingTriangle> fallingTriangles = new List<FallingTriangle>(); //keep track of triangles

        //other necessary fields
        bool redRowBroken = false;
        bool paddleShrink = false;
        bool moveLeft;
        bool moveRight;
        bool mouseMode = false;
        bool orangeSpeedBoostApplied = false;
        bool redSpeedBoostApplied = false;
        int paddleX;
        int paddleY;
        int paddleSpeed;
        int score;
        int lives;
        int ballHitCount;
        int currentLevel = 1;
        int currentScreen = 1;
        int screensPerLevel = 2;
        

        //*************************************************************
        //Constructors
        //*************************************************************
        public Form1()
        {
            InitializeComponent();

            //Show only the Main Menu at start
            pnlMainMenu.Visible = true;
            pnlInstructions.Visible = false;
            lblInsTitle.Text = "Welcome to Breakout!";
            lblInstructions.Text = "Objective:\n" +
                                    "- Break all bricks to win.\n\n" +

                                    "Gameplay:\n" +
                                    "- Ball speed increases during play.\n" +
                                    "- Balls automatically get released after lost life and next level.\n" +
                                    "- Need to clear 2 screens to finish level 1\n" +
                                    "- On 4th and 12 hit, as well as when ball touches orange or red brick,the\n" +
                                    "speed increments by a bit.\n" +
                                    "- Breaking the red row and hitting the top shrinks the paddle in level 1.\n\n" +

                                    "Scoring:\n" +
                                    "- Yellow: 1  Green: 3  Orange: 5  Red: 7\n\n" +

                                    "Additional Features: (Level 2+)\n" +
                                    "- Plus sign can double the size of paddle, give 3 supportive paddles,\n" +
                                    "and give firing bullets ability for 20 seconds each.\n" +
                                    "Power-Ups get replaced when multiple plus signs interact with paddle\n" +
                                    "- Bounce heart on paddle 4 time and get an extra life.\n" +
                                    "- Darker bricks require 2-hits and always drop a triangle which can slow\n" +
                                    "mobility for 3 seconds\n" +
                                    "- Bricks with balls inside release extra balls user can interact with\n\n" +

                                    "BACKSPACE to return.";
            pnlControls.Visible = false;

            lblControls.Text = "Controls:\n\n" +
                                "- LEFT Arrow → Move the paddle left\n" +
                                "- RIGHT Arrow → Move the paddle right\n" +
                                "- Up-Arrow → Launch the ball (from above the paddle)\n" +
                                "- BACKSPACE → Return to the Main Menu (from any sub-menu)\n" +
                                "- ESC → Quit the game (only from Main Menu)\n\n" +
                                "Gameplay Tips:\n\n" +
                                "- Hit the ball with the paddle center to maintain normal speed.\n" +
                                "- Hit the sides of the paddle to slightly increase ball speed.\n" +
                                "- Watch out for the Red row: breaking it will shrink your paddle\n" + 
                                "in level 1!\n" +
                                "- If the ball falls below the paddle, you lose a life.";

            pnlModeSelect.Visible = false;
            lblModeTitle.Text = "Select Game Mode";
            lblMode.Text = "Choose your control mode:\n\n" +
                            "- Press M → Play in Mouse Mode (move paddle with the mouse)\n" +
                            "- Press K → Play in Keyboard Mode (use LEFT/RIGHT arrow keys)";

            // Hide game elements until game starts
            lblScore.Visible = false;
            lblLives.Visible = false;

            //initializing
            paddle = new Paddle();
            originalPaddleWidth = paddle.Width;

            paddlePowerTimer = new Timer();
            paddlePowerTimer.Interval = 20000; // 20 seconds
            

            staticPaddleTimer = new Timer();
            staticPaddleTimer.Interval = 20000; // 20 seconds
            staticPaddleTimer.Tick += (s, ev) => //using special parameters to pass in objects in timer
            {
                // Remove all static paddles after 20 seconds
                activeStaticPaddles.Clear();
                staticPaddleTimer.Stop();
            };

            slowPaddleTimer = new Timer();
            slowPaddleTimer.Interval = 3000; // 3 seconds
            slowPaddleTimer.Tick += (s, e) =>
            {
                paddleSpeed = normalPaddleSpeed;
                slowPaddleTimer.Stop();
            };

            ball = new Ball();
            bricks = new Bricks(currentLevel);
            menu = new Menu();

            paddleSpeed = 10;
            normalPaddleSpeed = paddleSpeed;
            moveLeft = false;
            moveRight = false;


            score = 0;
            lblScore.Text = "Score: 0";

            lives = 3;
            lblLives.Text = "Lives: " + lives;

            ballHitCount = 0;

            paddleX = (ClientSize.Width - paddle.Width) / 2;
            paddleY = ClientSize.Height - 50;

            this.Paint += Form1_Paint;
            this.KeyDown += Form1_KeyDown;
            this.KeyUp += Form1_KeyUp;
            this.MouseMove += Form1_MouseMove;
        }


        //*************************************************************
        //Methods
        //*************************************************************
        private void tmrGame_Tick(object sender, EventArgs e)
        {

            // Paddle Movement

            if (!mouseMode && (menu.MenuState == MenuStates.Start || menu.MenuState == MenuStates.Playing))
            {
                if (moveLeft)
                    paddleX -= paddleSpeed;
                if (moveRight)
                    paddleX += paddleSpeed;

                // keep inside screen
                if (paddleX < 0) paddleX = 0;
                if (paddleX + paddle.Width > ClientSize.Width) paddleX = ClientSize.Width - paddle.Width;
            }


            // Ball Behavior

            if (menu.MenuState == MenuStates.Start)
            {
                // Ball is attached above paddle at initial position
                ball.Position = new Vector(
                    paddleX + paddle.Width / 2 - ball.Size / 2,
                    paddleY - ball.Size);
            }
            else if (menu.MenuState == MenuStates.Playing)
            {

                // Move ball
                ball.Position += ball.Velocity;

                // detect red row broken (level 1 only)
                if (currentLevel == 1 && !redRowBroken)
                {
                    int brickHeight = 20;
                    int spacing = 5;
                    int bricksTopY = 50;

                    // Two red rows -- bottom of row 1
                    int redRowBottomY = bricksTopY + (2 * brickHeight) + spacing;

                    // Ball fully above red row
                    if (ball.Position.Y + ball.Size < redRowBottomY)
                    {
                        redRowBroken = true;
                    }
                }

                // Ball bounds 
                double ballLeft = ball.Position.X;
                double ballRight = ball.Position.X + ball.Size;
                double ballTop = ball.Position.Y;
                double ballBottom = ball.Position.Y + ball.Size;

                for (int i = activeExtraBalls.Count - 1; i >= 0; i--)
                {
                    Ball b = activeExtraBalls[i];

                    // Move the ball
                    b.Position += b.Velocity;

                    // Wall collision (left/right)
                    if (b.Position.X <= 0 || b.Position.X + b.Size >= ClientSize.Width)
                    {
                        // Keep inside bounds and reverse X
                        if (b.Position.X <= 0) b.Position = new Vector(0, b.Position.Y);
                        if (b.Position.X + b.Size >= ClientSize.Width) b.Position = new Vector(ClientSize.Width - b.Size, b.Position.Y);
                        b.Velocity = new Vector(-b.Velocity.X, b.Velocity.Y);
                    }

                    if (b.Position.Y <= 0)
                    {
                        b.Velocity = new Vector(b.Velocity.X, -b.Velocity.Y);

                        //level 2+ - permanent speed increase
                        if (currentLevel >= 2)
                        {
                            ApplyTopWallSpeedIncrease(b);
                        }
                    }

                    // Paddle collision — apply same angle/speed logic as main ball
                    if (b.Position.Y + b.Size >= paddleY &&
                        b.Position.Y <= paddleY + paddle.Height &&
                        b.Position.X + b.Size >= paddleX &&
                        b.Position.X <= paddleX + paddle.Width &&
                        b.Velocity.Y > 0)
                    {
                        double ballCenterX = b.Position.X + b.Size / 2.0;
                        double paddleCenterX = paddleX + paddle.Width / 2.0;

                        double hitOffset = ballCenterX - paddleCenterX;
                        double normalizedOffset = hitOffset / (paddle.Width / 2.0);
                        normalizedOffset = Math.Max(-0.9, Math.Min(0.9, normalizedOffset));

                        double speedToApply = b.CurrentSpeed;

                        if (Math.Abs(normalizedOffset) > 0.5)
                        {
                            speedToApply = b.CurrentSpeed * 1.15;
                        }

                        double newVelX = normalizedOffset * speedToApply;
                        double newVelY = -Math.Sqrt(Math.Abs(speedToApply * speedToApply - newVelX * newVelX));

                        b.Velocity = new Vector(newVelX, newVelY);

                        // Position correction
                        b.Position = new Vector(b.Position.X, paddleY - b.Size);

                        // If CornerShots power active, spawn two mini bullets from corners (same as main ball)
                        if (currentPowerUp == PowerUpType.CornerShots)
                        {
                            double leftX = paddleX + 6;
                            double rightX = paddleX + paddle.Width - 6;
                            double spawnY = paddleY - 4;

                            MiniBullet bLeft = new MiniBullet(leftX, spawnY, 0, -6);
                            MiniBullet bRight = new MiniBullet(rightX, spawnY, 0, -6);

                            activeBullets.Add(bLeft);
                            activeBullets.Add(bRight);
                        }
                    }

                    // Static paddle collision (same as main ball)
                    foreach (StaticPaddle sp in activeStaticPaddles)
                    {
                        if (!sp.Active) continue;

                        double BallLeft = b.Position.X;
                        double BallRight = b.Position.X + b.Size;
                        double BallTop = b.Position.Y;
                        double BallBottom = b.Position.Y + b.Size;

                        double spLeft = sp.X;
                        double spRight = sp.X + sp.Width;
                        double spTop = sp.Y;
                        double spBottom = sp.Y + sp.Height;

                        if (BallRight > spLeft && BallLeft < spRight &&
                            BallBottom > spTop && BallTop < spBottom &&
                            b.Velocity.Y > 0)
                        {
                            b.Velocity = new Vector(b.Velocity.X, -b.Velocity.Y);
                            sp.Active = false;
                        }
                    }

                    // Brick collisions for this extra ball 
                    HandleBallBrickCollisions(b);

                    // Remove extra ball if it falls off screen
                    if (b.Position.Y > ClientSize.Height)
                    {
                        activeExtraBalls.RemoveAt(i);
                    }
                }


                // Wall Collisions
                if (ball.Position.X <= 0 || ball.Position.X + ball.Size >= ClientSize.Width)
                {
                    // Prevent the ball from getting stuck at the edge
                    if (ball.Position.X <= 0)
                        ball.Position = new Vector(0, ball.Position.Y);
                    if (ball.Position.X + ball.Size >= ClientSize.Width)
                        ball.Position = new Vector(ClientSize.Width - ball.Size, ball.Position.Y);

                    // Reverse the X velocity
                    ball.Velocity = new Vector(-ball.Velocity.X, ball.Velocity.Y);
                }

                if (ball.Position.Y <= 0)
                {
                    ball.Velocity = new Vector(ball.Velocity.X, -ball.Velocity.Y);

                    //level 2+ permanent speed increase on top-wall hit for main ball
                    if (currentLevel >= 2)
                    {
                        ApplyTopWallSpeedIncrease(ball);
                    }

                    //level 1 only -- shrink paddle rule
                    if (currentLevel == 1 && redRowBroken && !paddleShrink)
                    {
                        if (!paddlePowered)
                        {
                            paddle.Width = originalPaddleWidth / 2;

                            if (paddleX + paddle.Width > ClientSize.Width)
                                paddleX = ClientSize.Width - paddle.Width;
                        }

                        paddleShrink = true;
                    }
                }


                // Paddle Collision

                if (ballBottom >= paddleY &&
                    ballTop <= paddleY + paddle.Height &&
                    ballRight >= paddleX &&
                    ballLeft <= paddleX + paddle.Width &&
                    ball.Velocity.Y > 0)
                {
                    double ballCenterX = ball.Position.X + ball.Size / 2.0;
                    double paddleCenterX = paddleX + paddle.Width / 2.0;

                    double hitOffset = ballCenterX - paddleCenterX;
                    double normalizedOffset = hitOffset / (paddle.Width / 2.0);
                    normalizedOffset = Math.Max(-0.9, Math.Min(0.9, normalizedOffset));


                    double speedToApply = ball.CurrentSpeed;

                    //side of the paddle -- speed should increase a bit
                    if (Math.Abs(normalizedOffset) > 0.5)
                    {
                        speedToApply = ball.CurrentSpeed * 1.15; // temporary boost
                    }

                    //Apply the new Velocity
                    // Calculate X and Y based on the angle and the chosen speed
                    double newVelX = normalizedOffset * speedToApply;

                    // using Pythagorean theorem concept 
                    double newVelY = -Math.Sqrt(Math.Abs(speedToApply * speedToApply - newVelX * newVelX));

                    ball.Velocity = new Vector(newVelX, newVelY);

                    //Position correction 
                    ball.Position = new Vector(ball.Position.X, paddleY - ball.Size);

                    // If CornerShots power active, spawn two mini bullets from corners
                    if (currentPowerUp == PowerUpType.CornerShots)
                    {
                        // left corner
                        double leftX = paddleX + 6; 
                        double rightX = paddleX + paddle.Width - 6;
                        double spawnY = paddleY - 4;

                        // create two bullets going straight up
                        MiniBullet bLeft = new MiniBullet(leftX, spawnY, 0, -6); //using class I made
                        MiniBullet bRight = new MiniBullet(rightX, spawnY, 0, -6);

                        activeBullets.Add(bLeft);
                        activeBullets.Add(bRight);
                    }

                }

                //static paddle collision
                foreach (StaticPaddle sp in activeStaticPaddles)
                {
                    if (!sp.Active) continue; //using continue to skip iteration (special keyword, also used many times in code)

                    double BallLeft = ball.Position.X;
                    double BallRight = ball.Position.X + ball.Size;
                    double BallTop = ball.Position.Y;
                    double BallBottom = ball.Position.Y + ball.Size;

                    double spLeft = sp.X;
                    double spRight = sp.X + sp.Width;
                    double spTop = sp.Y;
                    double spBottom = sp.Y + sp.Height;

                    if (BallRight > spLeft && BallLeft < spRight &&
                        BallBottom > spTop && BallTop < spBottom &&
                        ball.Velocity.Y > 0)
                    {
                        // Reverse Y velocity
                        ball.Velocity = new Vector(ball.Velocity.X, -ball.Velocity.Y);

                        // Destroy this static paddle after 1 hit
                        sp.Active = false;
                    }
                }

                // Brick collision for the main ball 
                HandleBallBrickCollisions(ball);


                if (ball.Position.Y > ClientSize.Height)
                {
                    // If there is at least one active extra ball, do not lose a life or pause.
                    // Instead promote the first extra ball to be the main ball so gameplay continues.
                    if (activeExtraBalls.Count > 0)
                    {
                        Ball promoted = activeExtraBalls[0];
                        
                        ball.Position = promoted.Position;
                        ball.Velocity = promoted.Velocity;
                        ball.CurrentSpeed = promoted.CurrentSpeed;
                      
                        activeExtraBalls.RemoveAt(0);
                  
                    }
                    else
                    {
                        // No extra balls left -- lose a life as before
                        lives--;
                        lblLives.Text = "Lives: " + lives;

                        if (lives > 0)
                        {
                            // Reset ball above paddle before launching
                            ball.Position = new Vector(
                                paddleX + paddle.Width / 2 - ball.Size / 2,
                                paddleY - ball.Size
                            );

                            //automatically launch
                            LaunchBall();

                            paddleShrink = false;

                            // Reset speed boost 
                            orangeSpeedBoostApplied = false;
                            redSpeedBoostApplied = false;
                        }
                        else
                        {
                            menu.MenuState = MenuStates.GameOver;

                            lblFinalScore.Text = "Final Score: " + score;

                            UpdateGameUI();
                            UpdateMenuUI();
                        }
                    }
                }
            }


            // Check if all bricks are destroyed

            bool allBricksDestroyed = true;

            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 10; c++)
                {
                    if (!bricks.BrickArray[r, c].Destroyed)
                    {
                        allBricksDestroyed = false;
                        break;
                    }
                }
                if (!allBricksDestroyed) break;
            }

            if (allBricksDestroyed)
            {
                if (currentLevel == 1)
                {
                    //Level 1 -- has the screens
                    currentScreen++;

                    if (currentScreen > screensPerLevel)
                    {
                        currentLevel = 2;
                        currentScreen = 1; // reset
                    }
                }
                else
                {
                    //Level 2 -- no screens after this
                    currentLevel++;
                }

                // Reset bricks and other elements
                bricks = new Bricks(currentLevel);

                //reset all gameplay states
                redRowBroken = false;
                paddleShrink = false;
                orangeSpeedBoostApplied = false;
                redSpeedBoostApplied = false;
                ballHitCount = 0;

                //clear all active objects
                activeBullets.Clear();
                activePlusSigns.Clear();
                activeStaticPaddles.Clear();
                activeExtraBalls.Clear();

                //Reset paddle
                paddle.Width = originalPaddleWidth;
                paddle.HasCornerShots = false;
                paddlePowered = false;
                paddlePowerTimer.Stop();

                //auto start next level
                StartNextLevelAutomatically();

                UpdateGameUI();
                UpdateMenuUI();

            }

            for (int i = activePlusSigns.Count - 1; i >= 0; i--)
            {
                PlusPowerUp plus = activePlusSigns[i];
                plus.Update();

                // Check if it hits the paddle
                if (plus.Position.Y + plus.Size >= paddleY &&
                    plus.Position.X >= paddleX &&
                    plus.Position.X <= paddleX + paddle.Width)
                {
                    //apply power
                    paddlePowered = true;
                    int newWidth = Math.Min(originalPaddleWidth * 2, ClientSize.Width);
                    paddle.Width = newWidth;

                    if (paddleX + paddle.Width > ClientSize.Width)
                        paddleX = ClientSize.Width - paddle.Width;

                    // Restart timer
                    paddlePowerTimer.Stop();
                    paddlePowerTimer.Start();

                    //remove from list
                    activePlusSigns.RemoveAt(i);

                    // Randomly choose power-up
                    int pick = new Random().Next(0, 3); //using .Next -- returns random int
                    if (pick == 0)
                        currentPowerUp = PowerUpType.DoublePaddle;
                    else if (pick == 1)
                        currentPowerUp = PowerUpType.CornerShots;
                    else
                        currentPowerUp = PowerUpType.ExtraPaddles;

                    //apply power
                    if (currentPowerUp == PowerUpType.DoublePaddle)
                    {
                        paddlePowered = true;
                        paddle.HasCornerShots = false;

                        int NewWidth = Math.Min(originalPaddleWidth * 2, ClientSize.Width);
                        paddle.Width = NewWidth;

                        if (paddleX + paddle.Width > ClientSize.Width)
                            paddleX = ClientSize.Width - paddle.Width;

                        // restart timer for 20s
                        paddlePowerTimer.Stop();
                        paddlePowerTimer.Start();
                    }
                    else if (currentPowerUp == PowerUpType.CornerShots)
                    {
                        paddlePowered = false;
                        paddle.HasCornerShots = true;

                        // reset width
                        paddle.Width = originalPaddleWidth;
                        if (paddleX + paddle.Width > ClientSize.Width)
                            paddleX = ClientSize.Width - paddle.Width;

                       
                        paddlePowerTimer.Stop();
                        paddlePowerTimer.Start();
                    }
                    else if (currentPowerUp == PowerUpType.ExtraPaddles)
                    {
                        // Clear other powers
                        paddle.HasCornerShots = false;
                        paddle.Width = originalPaddleWidth;
                        paddlePowered = false;

                        activeStaticPaddles.Clear();

                        int numStaticPaddles = 3;       // number of static paddles
                        int gap = 80;                     // spacing between paddles
                        int totalGap = (numStaticPaddles - 1) * gap;
                        int paddleWidth = (ClientSize.Width - totalGap) / numStaticPaddles; //width
                        int staticY = paddleY + paddle.Height + 5; // place it below main paddle

                        // Create 3 static paddles across the entire form width
                        for (int i2 = 0; i2 < numStaticPaddles; i2++)
                        {
                            StaticPaddle sp = new StaticPaddle();
                            sp.Width = paddleWidth;
                            sp.X = i2 * (paddleWidth + gap); // position across form
                            sp.Y = staticY;
                            sp.Active = true;               // make sure it's active
                            activeStaticPaddles.Add(sp);
                        }

                        // start and restart 20-second timer
                        staticPaddleTimer.Stop();
                        staticPaddleTimer.Start();
                    }
                    continue;
                }

                // Remove if it falls below screen
                if (plus.Position.Y > ClientSize.Height)
                {
                    activePlusSigns.RemoveAt(i);
                }
            }

            for (int i = fallingHearts.Count - 1; i >= 0; i--)
            {
                FallingHeart heart = fallingHearts[i];
                heart.Update();

                // Paddle collision
                if (heart.Position.Y + heart.Size >= paddleY &&
                    heart.Position.Y <= paddleY + paddle.Height &&
                    heart.Position.X + heart.Size >= paddleX &&
                    heart.Position.X <= paddleX + paddle.Width &&
                    heart.Velocity.Y > 0)
                {
                    double paddleCenterX = paddleX + paddle.Width / 2.0;
                    heart.BounceOffPaddle(paddleCenterX);
                }

                // Heart finished -- give life
                if (!heart.Active)
                {
                    lives++;
                    lblLives.Text = "Lives: " + lives;
                    fallingHearts.RemoveAt(i);
                    continue;
                }

                // Fell off screen
                if (heart.Position.Y > ClientSize.Height)
                {
                    fallingHearts.RemoveAt(i);
                }
            }

            for (int i = fallingTriangles.Count - 1; i >= 0; i--)
            {
                FallingTriangle tri = fallingTriangles[i];
                tri.Update();

                // Paddle collision
                if (tri.Position.Y + tri.Size >= paddleY &&
                    tri.Position.X + tri.Size >= paddleX &&
                    tri.Position.X <= paddleX + paddle.Width)
                {
                    // Apply slow paddle effect
                    paddleSpeed = 3;   
                    slowPaddleTimer.Stop();
                    slowPaddleTimer.Start();

                    fallingTriangles.RemoveAt(i);
                    continue;
                }

                // Remove if off screen
                if (tri.Position.Y > ClientSize.Height)
                {
                    fallingTriangles.RemoveAt(i);
                }
            }

            // Update bullets, from end to start so we can remove safely
            for (int i = activeBullets.Count - 1; i >= 0; i--)
            {
                MiniBullet bullet = activeBullets[i];
                bullet.Update();

                // Remove off-screen bullets
                if (bullet.Position.Y + bullet.Size < 0)
                {
                    activeBullets.RemoveAt(i);
                    continue;
                }

                // Check collision with bricks
                bool bulletHit = false;
                for (int r = 0; r < 8 && !bulletHit; r++)
                {
                    for (int c = 0; c < 10 && !bulletHit; c++)
                    {
                        Brick brick = bricks.BrickArray[r, c];
                        if (brick.Destroyed || brick.BrickType == BrickTypes.None)
                            continue;

                        int brickX = (ClientSize.Width - bricks.TotalWidth) / 2 + c * (60 + 5);
                        int brickY = 50 + r * (20 + 5);

                        double brickLeft = brickX;
                        double brickRight = brickX + 60;
                        double brickTop = brickY;
                        double brickBottom = brickY + 20;

                        double bulletLeft = bullet.Position.X - bullet.Size / 2.0;
                        double bulletRight = bullet.Position.X + bullet.Size / 2.0;
                        double bulletTop = bullet.Position.Y - bullet.Size / 2.0;
                        double bulletBottom = bullet.Position.Y + bullet.Size / 2.0;

                        if (bulletRight > brickLeft &&
                            bulletLeft < brickRight &&
                            bulletBottom > brickTop &&
                            bulletTop < brickBottom)
                        {
                            ballHitCount++;

                            // Speed boosts
                            if (!orangeSpeedBoostApplied && brick.BrickColour == BrickColours.Orange)
                            {
                                IncreaseSpeedForAllBalls(0.20);
                                orangeSpeedBoostApplied = true;
                            }
                            else if (!redSpeedBoostApplied && brick.BrickColour == BrickColours.Red)
                            {
                                IncreaseSpeedForAllBalls(0.20);
                                redSpeedBoostApplied = true;
                            }

                            if (brick.HitsRemaining > 1)
                            {
                                brick.HitsRemaining--;
                            }
                            else
                            {
                                brick.Destroyed = true;

                                
                                ResolveBrickPayload(brick, brickX, brickY);

                                // Score
                                if (brick.BrickColour == BrickColours.Yellow) score += 1;
                                else if (brick.BrickColour == BrickColours.Green) score += 3;
                                else if (brick.BrickColour == BrickColours.Orange) score += 5;
                                else if (brick.BrickColour == BrickColours.Red) score += 7;

                                lblScore.Text = "Score: " + score;
                            }

                            if (ballHitCount == 4 || ballHitCount == 12)
                            {
                                IncreaseSpeedForAllBalls(0.30);
                            }

                            //bullet always disappears
                            activeBullets.RemoveAt(i);
                            bulletHit = true;
                        }   
                    }
                }
            }
            // Redraw
            Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            ////You can enable this to skip to Level 2 for testing
            //if (e.KeyCode == Keys.F2)
            //{
            //    currentLevel = 2;
            //    currentScreen = 1;

            //    // Reset bricks for level 2
            //    bricks = new Bricks(currentLevel);

            //    // Reset ball
            //    menu.MenuState = MenuStates.Start;
            //    ball.Velocity = new Vector(0, 0);
            //    ball.Position = new Vector(
            //        paddleX + paddle.Width / 2 - ball.Size / 2,
            //        paddleY - ball.Size
            //    );

            //    // Reset level-based flags
            //    redRowBroken = false;
            //    orangeSpeedBoostApplied = false;
            //    redSpeedBoostApplied = false;
            //    ballHitCount = 0;

            //    UpdateGameUI();
            //    UpdateMenuUI();
            //    Invalidate();
            //    return;
            //}
            
            //game over input
            if (menu.MenuState == MenuStates.GameOver)
            {
                if (e.KeyCode == Keys.Back)
                {
                    RestartGame();
                }
                return;
            }

            //Backpace from sub menu to main menu
            if (menu.MenuState == MenuStates.Instructions || menu.MenuState == MenuStates.Controls)
            {
                if (e.KeyCode == Keys.Back)
                {
                    menu.MenuState = MenuStates.MainMenu;
                    UpdateMenuUI();
                    UpdateGameUI();
                    Invalidate();
                }
                return;
            }


            // Mode select (mouse vs keyboard)

            if (menu.MenuState == MenuStates.ModeSelect)
            {
                if (e.KeyCode == Keys.M)
                {
                    mouseMode = true;
                    lblModeText.Text = "Mouse Mode selected! Press BACKSPACE to return.\nThen press space and click left mouse click to play!";
                    UpdateMenuUI();
                    UpdateGameUI();
                    Invalidate();
                    return;
                }
                else if (e.KeyCode == Keys.K)
                {
                    mouseMode = false;
                    lblModeText.Text = "Keyboard Mode selected! Press BACKSPACE to return.\nThen press space and press Up-Arrow Key to play!";
                    UpdateMenuUI();
                    UpdateGameUI();
                    Invalidate();
                    return;
                }
                else if (e.KeyCode == Keys.Back)
                {
                    menu.MenuState = MenuStates.MainMenu;
                    UpdateMenuUI();
                    UpdateGameUI();
                    Invalidate();
                    return;
                }
            }


            // Main menu input 

            if (menu.MenuState == MenuStates.MainMenu)
            {
                if (e.KeyCode == Keys.D1)
                    menu.MenuState = MenuStates.Instructions;
                else if (e.KeyCode == Keys.D2)
                    menu.MenuState = MenuStates.Controls;
                else if (e.KeyCode == Keys.D3)
                    menu.MenuState = MenuStates.ModeSelect;
                else if (e.KeyCode == Keys.Space)
                {
                    menu.MenuState = MenuStates.Start;

                    // Reset ball + paddle
                    ball.Velocity = new Vector(0, 0);
                    ball.Position = new Vector(
                        paddleX + paddle.Width / 2 - ball.Size / 2,
                        paddleY - ball.Size
                    );
                }
                else if (e.KeyCode == Keys.Escape)
                    Application.Exit();

                UpdateMenuUI();
                UpdateGameUI();
                Invalidate();
                return;
            }


            //start start (launch up)

            if (menu.MenuState == MenuStates.Start)
            {
                if (!mouseMode) // Keyboard controls
                {
                    if (e.KeyCode == Keys.Left)
                        moveLeft = true;
                    if (e.KeyCode == Keys.Right)
                        moveRight = true;
                    if (e.KeyCode == Keys.Up) // Launch the ball
                        LaunchBall();
                }
            }

            //Gameplay input
            if (menu.MenuState == MenuStates.Playing)
            {
                if (!mouseMode) // Only enable keyboard movement if not in mouse mode
                {
                    if (e.KeyCode == Keys.Left)
                        moveLeft = true;
                    if (e.KeyCode == Keys.Right)
                        moveRight = true;
                }
            }
        }

        private void RestartGame()
        {
            // Reset state
            menu.MenuState = MenuStates.MainMenu;

            score = 0;
            lives = 3;
            ballHitCount = 0;

            currentLevel = 1;
            currentScreen = 1;

            redRowBroken = false;
            paddleShrink = false;
            orangeSpeedBoostApplied = false;
            redSpeedBoostApplied = false;

            // Reset UI
            lblScore.Text = "Score: 0";
            lblLives.Text = "Lives: 3";
            lblFinalScore.Text = "";

            // Reset objects
            paddle = new Paddle();
            ball = new Ball();
            bricks = new Bricks(currentLevel);

            paddleX = (ClientSize.Width - paddle.Width) / 2;
            paddleY = ClientSize.Height - 50;

            ball.Position = new Vector(
                paddleX + paddle.Width / 2 - ball.Size / 2,
                paddleY - ball.Size
            );

            ball.Velocity = new Vector(0, 0);

            //power up/ clean up bullet
            activeBullets.Clear();          // remove any bullets from corner-shots
            activePlusSigns.Clear();        // remove any falling pluses
            currentPowerUp = PowerUpType.DoublePaddle; 
            paddle.HasCornerShots = false;  // remove corner-shots 
            paddle.Width = originalPaddleWidth;
            paddlePowerTimer.Stop();
            paddlePowered = false;
            activeStaticPaddles.Clear();

            UpdateMenuUI();
            UpdateGameUI();
            Invalidate();
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            if (menu.MenuState == MenuStates.Playing || menu.MenuState == MenuStates.Start)
            {

                Graphics g = e.Graphics;

                // Draw bricks
                int bricksX = (ClientSize.Width - bricks.TotalWidth) / 2;
                int bricksY = 50;

                bricks.Draw(g, bricksX, bricksY);

                // Draw paddle
                paddle.Draw(g, paddleX, paddleY, ClientSize.Width);

                // Draw ball
                ball.Draw(g, (int)ball.Position.X, (int)ball.Position.Y);

                //Draw plus signs on bricks
                foreach (PlusPowerUp plus in activePlusSigns)
                {
                    plus.Draw(e.Graphics);
                }

                //Draw active bullets 
                foreach (MiniBullet b in activeBullets)
                    b.Draw(g);

                //Draw static paddles
                foreach (StaticPaddle sp in activeStaticPaddles)
                {
                    sp.Draw(g);
                }

                //draw the extra balls
                foreach (Ball b in activeExtraBalls)
                {
                    b.Draw(g, (int)b.Position.X, (int)b.Position.Y);
                }

                //draw the hearts in the brick
                foreach (FallingHeart heart in fallingHearts)
                {
                    heart.Draw(g);
                }

                //draw falling triangle
                foreach (FallingTriangle tri in fallingTriangles)
                {
                    tri.Draw(g);
                }

            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                moveLeft = false;

            if (e.KeyCode == Keys.Right)
                moveRight = false;
        }

        private void UpdateGameUI()
        {
            bool inGame = (menu.MenuState == MenuStates.Start || menu.MenuState == MenuStates.Playing);

            lblScore.Visible = inGame;
            lblLives.Visible = inGame;
            lblLevel.Visible = inGame;

            if (inGame)
            {
                lblLevel.Text = "Level: " + currentLevel;

                if (currentLevel == 1)
                {
                    lblScreen.Visible = true;
                    lblScreen.Text = "Screen: " + currentScreen + "/" + screensPerLevel;
                }
                else
                {
                    lblScreen.Visible = false; // 
                }
            }
            else
            {
                lblLevel.Visible = false;
                lblScreen.Visible = false;
            }
        }

        private void UpdateMenuUI()
        {
            pnlMainMenu.Visible = (menu.MenuState == MenuStates.MainMenu);
            pnlInstructions.Visible = (menu.MenuState == MenuStates.Instructions);
            pnlControls.Visible = (menu.MenuState == MenuStates.Controls);
            pnlModeSelect.Visible = (menu.MenuState == MenuStates.ModeSelect);
            pnlGameOver.Visible = (menu.MenuState == MenuStates.GameOver);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseMode && (menu.MenuState == MenuStates.Start || menu.MenuState == MenuStates.Playing))
            {
                paddleX = e.X - paddle.Width / 2;

                // Keep paddle inside screen
                if (paddleX < 0) paddleX = 0;
                if (paddleX + paddle.Width > ClientSize.Width) paddleX = ClientSize.Width - paddle.Width;
            }
        }

        private void LaunchBall()
        {
            menu.MenuState = MenuStates.Playing;

            double baseSpeed = 3.6;
            double speedMultiplier = 1.0 + (currentLevel - 1) * 0.08;
            double finalSpeed = baseSpeed * speedMultiplier;

            ball.Velocity = new Vector(2, -3);
            ball.CurrentSpeed = finalSpeed;

            // Accurate direction
            double length = Math.Sqrt(ball.Velocity.X * ball.Velocity.X +
                                      ball.Velocity.Y * ball.Velocity.Y);

            ball.Velocity = new Vector(
                ball.Velocity.X / length * finalSpeed,
                ball.Velocity.Y / length * finalSpeed
            );

            lblStart.Visible = false;
            UpdateMenuUI();
            UpdateGameUI();
            Invalidate();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (mouseMode && menu.MenuState == MenuStates.Start && e.Button == MouseButtons.Left)
            {
                LaunchBall();
            }
        }

        private bool HandleBallBrickCollisions(Ball b)
        {

            // returns true if a brick was hit 
            int brickWidth = 60;
            int brickHeight = 20;
            int spacing = 5;

            int bricksX = (ClientSize.Width - bricks.TotalWidth) / 2;
            int bricksY = 50;

            double ballLeft = b.Position.X;
            double ballRight = b.Position.X + b.Size;
            double ballTop = b.Position.Y;
            double ballBottom = b.Position.Y + b.Size;

            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 10; c++)
                {
                    Brick brick = bricks.BrickArray[r, c];

                    if (brick.Destroyed || brick.BrickType == BrickTypes.None)
                        continue;

                    int brickX = bricksX + c * (brickWidth + spacing);
                    int brickY = bricksY + r * (brickHeight + spacing);

                    double brickLeft = brickX;
                    double brickRight = brickX + brickWidth;
                    double brickTop = brickY;
                    double brickBottom = brickY + brickHeight;

                    if (ballRight > brickLeft &&
                        ballLeft < brickRight &&
                        ballBottom > brickTop &&
                        ballTop < brickBottom)
                    {
                        
                        ballHitCount++;

                        if (!orangeSpeedBoostApplied && brick.BrickColour == BrickColours.Orange)
                        {
                            IncreaseSpeedForAllBalls(0.20);
                            orangeSpeedBoostApplied = true;
                        }
                        else if (!redSpeedBoostApplied && brick.BrickColour == BrickColours.Red)
                        {
                            IncreaseSpeedForAllBalls(0.20);
                            redSpeedBoostApplied = true;
                        }

                        if (brick.HitsRemaining > 1)
                        {
                            brick.HitsRemaining--;
                        }
                        else
                        {
                            brick.Destroyed = true;

                            if (brick.HasHeart)
                            {
                                FallingHeart heart = new FallingHeart(
                                    brickX + brickWidth / 2 - 8,
                                    brickY + brickHeight / 2 - 8
                                );
                                heart.ScreenWidth = ClientSize.Width;
                                fallingHearts.Add(heart);
                                brick.HasHeart = false;
                            }

                            // spawn plus-powerup if present
                            if (brick.HasPlusPowerUp)
                            {
                                PlusPowerUp plus = new PlusPowerUp(new Vector(brickX + brickWidth / 2, brickY + brickHeight / 2));
                                activePlusSigns.Add(plus);
                                brick.HasPlusPowerUp = false;
                            }

                            // Spawn slow-paddle triangle only from 2-hit bricks
                            if (brick.BrickType == BrickTypes.Special)
                            {
                                FallingTriangle tri = new FallingTriangle(
                                    brickX + brickWidth / 2 - 9,
                                    brickY + brickHeight / 2 - 9
                                );

                                fallingTriangles.Add(tri);
                            }

                            // spawn extra ball if present
                            if (brick.HasExtraBall)
                            {
                                Ball extra = new Ball();
                                extra.Position = new Vector(brickX + brickWidth / 2 - extra.Size / 2,
                                                            brickY + brickHeight / 2 - extra.Size / 2);

                                double angle = mRandom.NextDouble() * Math.PI / 2 + Math.PI / 4; // 45° to 135°
                                double speed = b.CurrentSpeed > 0 ? b.CurrentSpeed : ball.CurrentSpeed;
                                extra.Velocity = new Vector(speed * Math.Cos(angle), -speed * Math.Sin(angle));
                                extra.CurrentSpeed = speed;

                                activeExtraBalls.Add(extra);
                                brick.HasExtraBall = false;
                            }



                            // scoring (only when destroyed)
                            if (brick.BrickColour == BrickColours.Yellow) score += 1;
                            else if (brick.BrickColour == BrickColours.Green) score += 3;
                            else if (brick.BrickColour == BrickColours.Orange) score += 5;
                            else if (brick.BrickColour == BrickColours.Red) score += 7;

                            lblScore.Text = "Score: " + score;
                        }

                        if (ballHitCount == 4 || ballHitCount == 12)
                        {
                            IncreaseSpeedForAllBalls(0.30);
                        }


                        //Collision response (did the overlap test to make collision adequete)
                        double overlapLeft = ballRight - brickLeft;
                        double overlapRight = brickRight - ballLeft;
                        double overlapTop = ballBottom - brickTop;
                        double overlapBottom = brickBottom - ballTop;

                        double minOverlapX = Math.Min(overlapLeft, overlapRight);
                        double minOverlapY = Math.Min(overlapTop, overlapBottom);

                        if (minOverlapX < minOverlapY)
                        {
                            // side hit -- reverse X
                            b.Velocity = new Vector(-b.Velocity.X, b.Velocity.Y);
                        }
                        else
                        {
                            // top/bottom -- reverse Y
                            b.Velocity = new Vector(b.Velocity.X, -b.Velocity.Y);
                        }
                        
                        return true;
                    }
                }
            }

            return false;
        }

        private void IncreasePermanentSpeed(Ball b, double percent)
        {
            b.CurrentSpeed *= (1.0 + percent);

            // normalize velocity to new permanent speed
            double length = Math.Sqrt(b.Velocity.X * b.Velocity.X +
                                      b.Velocity.Y * b.Velocity.Y);

            if (length > 0)
            {
                b.Velocity = new Vector(
                    b.Velocity.X / length * b.CurrentSpeed,
                    b.Velocity.Y / length * b.CurrentSpeed
                );
            }
        }

        private void IncreaseSpeedForAllBalls(double percent)
        {
            // Main ball
            IncreasePermanentSpeed(ball, percent);

            // Extra balls
            foreach (Ball extra in activeExtraBalls)
            {
                IncreasePermanentSpeed(extra, percent);
            }
        }


        private void StartNextLevelAutomatically()
        {
            //remove all extra balls
            activeExtraBalls.Clear();

            // Reset main ball position above paddle
            ball.Position = new Vector(
                paddleX + paddle.Width / 2 - ball.Size / 2,
                paddleY - ball.Size
            );

            // Calculate level-based speed (same logic as LaunchBall)
            double baseSpeed = 3.6;
            double speedMultiplier = 1.0 + (currentLevel - 1) * 0.08;
            double finalSpeed = baseSpeed * speedMultiplier;

            // Initial upward direction
            ball.Velocity = new Vector(2, -3);
            ball.CurrentSpeed = finalSpeed;

            // Normalize velocity
            double length = Math.Sqrt(
                ball.Velocity.X * ball.Velocity.X +
                ball.Velocity.Y * ball.Velocity.Y
            );

            ball.Velocity = new Vector(
                ball.Velocity.X / length * finalSpeed,
                ball.Velocity.Y / length * finalSpeed
            );

            //don't pause
            menu.MenuState = MenuStates.Playing;
        }

        private void ResolveBrickPayload(Brick brick, int brickX, int brickY)
        {
            int brickWidth = 60;
            int brickHeight = 20;

            // Heart
            if (brick.HasHeart)
            {
                FallingHeart heart = new FallingHeart(
                    brickX + brickWidth / 2 - 8,
                    brickY + brickHeight / 2 - 8
                );
                heart.ScreenWidth = ClientSize.Width;
                fallingHearts.Add(heart);
                brick.HasHeart = false;
            }

            // Plus power-up
            if (brick.HasPlusPowerUp)
            {
                PlusPowerUp plus = new PlusPowerUp(
                    new Vector(brickX + brickWidth / 2, brickY + brickHeight / 2)
                );
                activePlusSigns.Add(plus);
                brick.HasPlusPowerUp = false;
            }

            // Slow paddle triangle (2-hit bricks)
            if (brick.BrickType == BrickTypes.Special)
            {
                FallingTriangle tri = new FallingTriangle(
                    brickX + brickWidth / 2 - 9,
                    brickY + brickHeight / 2 - 9
                );
                fallingTriangles.Add(tri);
            }

            // Extra ball
            if (brick.HasExtraBall)
            {
                Ball extra = new Ball();
                extra.Position = new Vector(
                    brickX + brickWidth / 2 - extra.Size / 2,
                    brickY + brickHeight / 2 - extra.Size / 2
                );

                double angle = mRandom.NextDouble() * Math.PI / 2 + Math.PI / 4;
                double speed = ball.CurrentSpeed;

                extra.Velocity = new Vector(
                    speed * Math.Cos(angle),
                    -speed * Math.Sin(angle)
                );

                extra.CurrentSpeed = speed;
                activeExtraBalls.Add(extra);
                brick.HasExtraBall = false;
            }


        }

        private void ApplyTopWallSpeedIncrease(Ball b)
        {
            double percent = 0.05; // 5%

            b.CurrentSpeed *= (1.0 + percent);

            // Normalize velocity to new speed
            double length = Math.Sqrt(
                b.Velocity.X * b.Velocity.X +
                b.Velocity.Y * b.Velocity.Y
            );

            if (length > 0)
            {
                b.Velocity = new Vector(
                    b.Velocity.X / length * b.CurrentSpeed,
                    b.Velocity.Y / length * b.CurrentSpeed
                );
            }
        }

    }
}




