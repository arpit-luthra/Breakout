namespace Breakout
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tmrGame = new System.Windows.Forms.Timer(this.components);
            this.lblStart = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            this.lblLives = new System.Windows.Forms.Label();
            this.pnlMainMenu = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblMenuExit = new System.Windows.Forms.Label();
            this.lblMenuMode = new System.Windows.Forms.Label();
            this.lblMenuControls = new System.Windows.Forms.Label();
            this.lblMenuInstructions = new System.Windows.Forms.Label();
            this.lblMenuStart = new System.Windows.Forms.Label();
            this.pnlInstructions = new System.Windows.Forms.Panel();
            this.lblInsTitle = new System.Windows.Forms.Label();
            this.lblInstructions = new System.Windows.Forms.Label();
            this.pnlControls = new System.Windows.Forms.Panel();
            this.lblControls = new System.Windows.Forms.Label();
            this.lblLevel = new System.Windows.Forms.Label();
            this.lblScreen = new System.Windows.Forms.Label();
            this.pnlModeSelect = new System.Windows.Forms.Panel();
            this.lblModeText = new System.Windows.Forms.Label();
            this.lblModeTitle = new System.Windows.Forms.Label();
            this.lblMode = new System.Windows.Forms.Label();
            this.pnlGameOver = new System.Windows.Forms.Panel();
            this.lblBackToMenu = new System.Windows.Forms.Label();
            this.lblFinalScore = new System.Windows.Forms.Label();
            this.lblGameOver = new System.Windows.Forms.Label();
            this.pnlMainMenu.SuspendLayout();
            this.pnlInstructions.SuspendLayout();
            this.pnlControls.SuspendLayout();
            this.pnlModeSelect.SuspendLayout();
            this.pnlGameOver.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmrGame
            // 
            this.tmrGame.Enabled = true;
            this.tmrGame.Interval = 5;
            this.tmrGame.Tick += new System.EventHandler(this.tmrGame_Tick);
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(32, 9);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(34, 16);
            this.lblStart.TabIndex = 0;
            this.lblStart.Text = "Start";
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScore.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblScore.Location = new System.Drawing.Point(23, 25);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(57, 22);
            this.lblScore.TabIndex = 1;
            this.lblScore.Text = "Score";
            this.lblScore.Visible = false;
            // 
            // lblLives
            // 
            this.lblLives.AutoSize = true;
            this.lblLives.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLives.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblLives.Location = new System.Drawing.Point(131, 24);
            this.lblLives.Name = "lblLives";
            this.lblLives.Size = new System.Drawing.Size(52, 22);
            this.lblLives.TabIndex = 2;
            this.lblLives.Text = "Lives";
            this.lblLives.Visible = false;
            // 
            // pnlMainMenu
            // 
            this.pnlMainMenu.Controls.Add(this.lblTitle);
            this.pnlMainMenu.Controls.Add(this.lblMenuExit);
            this.pnlMainMenu.Controls.Add(this.lblMenuMode);
            this.pnlMainMenu.Controls.Add(this.lblMenuControls);
            this.pnlMainMenu.Controls.Add(this.lblMenuInstructions);
            this.pnlMainMenu.Controls.Add(this.lblMenuStart);
            this.pnlMainMenu.Location = new System.Drawing.Point(-7, 66);
            this.pnlMainMenu.Name = "pnlMainMenu";
            this.pnlMainMenu.Size = new System.Drawing.Size(857, 651);
            this.pnlMainMenu.TabIndex = 10;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Georgia", 72F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblTitle.Location = new System.Drawing.Point(26, 28);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(800, 136);
            this.lblTitle.TabIndex = 15;
            this.lblTitle.Text = "BREAKOUT";
            // 
            // lblMenuExit
            // 
            this.lblMenuExit.AutoSize = true;
            this.lblMenuExit.Font = new System.Drawing.Font("Georgia", 28.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMenuExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblMenuExit.Location = new System.Drawing.Point(235, 544);
            this.lblMenuExit.Name = "lblMenuExit";
            this.lblMenuExit.Size = new System.Drawing.Size(359, 54);
            this.lblMenuExit.TabIndex = 14;
            this.lblMenuExit.Text = "Press ESC - Exit";
            // 
            // lblMenuMode
            // 
            this.lblMenuMode.AutoSize = true;
            this.lblMenuMode.Font = new System.Drawing.Font("Georgia", 28.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMenuMode.ForeColor = System.Drawing.Color.Lime;
            this.lblMenuMode.Location = new System.Drawing.Point(181, 459);
            this.lblMenuMode.Name = "lblMenuMode";
            this.lblMenuMode.Size = new System.Drawing.Size(490, 54);
            this.lblMenuMode.TabIndex = 13;
            this.lblMenuMode.Text = "Press 3 - Choose Mode";
            // 
            // lblMenuControls
            // 
            this.lblMenuControls.AutoSize = true;
            this.lblMenuControls.Font = new System.Drawing.Font("Georgia", 28.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMenuControls.ForeColor = System.Drawing.Color.Yellow;
            this.lblMenuControls.Location = new System.Drawing.Point(220, 372);
            this.lblMenuControls.Name = "lblMenuControls";
            this.lblMenuControls.Size = new System.Drawing.Size(392, 54);
            this.lblMenuControls.TabIndex = 12;
            this.lblMenuControls.Text = "Press 2 - Controls";
            // 
            // lblMenuInstructions
            // 
            this.lblMenuInstructions.AutoSize = true;
            this.lblMenuInstructions.Font = new System.Drawing.Font("Georgia", 28.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMenuInstructions.ForeColor = System.Drawing.Color.Blue;
            this.lblMenuInstructions.Location = new System.Drawing.Point(192, 291);
            this.lblMenuInstructions.Name = "lblMenuInstructions";
            this.lblMenuInstructions.Size = new System.Drawing.Size(462, 54);
            this.lblMenuInstructions.TabIndex = 11;
            this.lblMenuInstructions.Text = "Press 1 - Instructions";
            // 
            // lblMenuStart
            // 
            this.lblMenuStart.AutoSize = true;
            this.lblMenuStart.Font = new System.Drawing.Font("Georgia", 28.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMenuStart.ForeColor = System.Drawing.Color.Red;
            this.lblMenuStart.Location = new System.Drawing.Point(192, 208);
            this.lblMenuStart.Name = "lblMenuStart";
            this.lblMenuStart.Size = new System.Drawing.Size(465, 54);
            this.lblMenuStart.TabIndex = 10;
            this.lblMenuStart.Text = "Press SPACE to Start";
            // 
            // pnlInstructions
            // 
            this.pnlInstructions.Controls.Add(this.lblInsTitle);
            this.pnlInstructions.Controls.Add(this.lblInstructions);
            this.pnlInstructions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInstructions.Location = new System.Drawing.Point(0, 0);
            this.pnlInstructions.Name = "pnlInstructions";
            this.pnlInstructions.Size = new System.Drawing.Size(874, 835);
            this.pnlInstructions.TabIndex = 11;
            // 
            // lblInsTitle
            // 
            this.lblInsTitle.AutoSize = true;
            this.lblInsTitle.Font = new System.Drawing.Font("Georgia", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsTitle.ForeColor = System.Drawing.Color.Blue;
            this.lblInsTitle.Location = new System.Drawing.Point(20, 79);
            this.lblInsTitle.Name = "lblInsTitle";
            this.lblInsTitle.Size = new System.Drawing.Size(95, 38);
            this.lblInsTitle.TabIndex = 1;
            this.lblInsTitle.Text = "Title";
            // 
            // lblInstructions
            // 
            this.lblInstructions.AutoSize = true;
            this.lblInstructions.Font = new System.Drawing.Font("Georgia", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstructions.ForeColor = System.Drawing.Color.Blue;
            this.lblInstructions.Location = new System.Drawing.Point(21, 114);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(136, 27);
            this.lblInstructions.TabIndex = 0;
            this.lblInstructions.Text = "Instructions";
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.lblControls);
            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlControls.Font = new System.Drawing.Font("Georgia", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlControls.Location = new System.Drawing.Point(0, 0);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(874, 835);
            this.pnlControls.TabIndex = 12;
            // 
            // lblControls
            // 
            this.lblControls.AutoSize = true;
            this.lblControls.Font = new System.Drawing.Font("Georgia", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblControls.ForeColor = System.Drawing.Color.Yellow;
            this.lblControls.Location = new System.Drawing.Point(22, 135);
            this.lblControls.Name = "lblControls";
            this.lblControls.Size = new System.Drawing.Size(120, 32);
            this.lblControls.TabIndex = 0;
            this.lblControls.Text = "Controls";
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLevel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblLevel.Location = new System.Drawing.Point(254, 24);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(53, 22);
            this.lblLevel.TabIndex = 3;
            this.lblLevel.Text = "Level";
            this.lblLevel.Visible = false;
            // 
            // lblScreen
            // 
            this.lblScreen.AutoSize = true;
            this.lblScreen.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScreen.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblScreen.Location = new System.Drawing.Point(376, 24);
            this.lblScreen.Name = "lblScreen";
            this.lblScreen.Size = new System.Drawing.Size(67, 22);
            this.lblScreen.TabIndex = 4;
            this.lblScreen.Text = "Screen";
            this.lblScreen.Visible = false;
            // 
            // pnlModeSelect
            // 
            this.pnlModeSelect.Controls.Add(this.lblModeText);
            this.pnlModeSelect.Controls.Add(this.lblModeTitle);
            this.pnlModeSelect.Controls.Add(this.lblMode);
            this.pnlModeSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlModeSelect.Location = new System.Drawing.Point(0, 0);
            this.pnlModeSelect.Name = "pnlModeSelect";
            this.pnlModeSelect.Size = new System.Drawing.Size(874, 835);
            this.pnlModeSelect.TabIndex = 14;
            // 
            // lblModeText
            // 
            this.lblModeText.AutoSize = true;
            this.lblModeText.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModeText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblModeText.Location = new System.Drawing.Point(74, 438);
            this.lblModeText.Name = "lblModeText";
            this.lblModeText.Size = new System.Drawing.Size(111, 20);
            this.lblModeText.TabIndex = 2;
            this.lblModeText.Text = "Chosen Mode";
            // 
            // lblModeTitle
            // 
            this.lblModeTitle.AutoSize = true;
            this.lblModeTitle.Font = new System.Drawing.Font("Georgia", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModeTitle.ForeColor = System.Drawing.Color.Lime;
            this.lblModeTitle.Location = new System.Drawing.Point(74, 230);
            this.lblModeTitle.Name = "lblModeTitle";
            this.lblModeTitle.Size = new System.Drawing.Size(86, 35);
            this.lblModeTitle.TabIndex = 1;
            this.lblModeTitle.Text = "Title";
            // 
            // lblMode
            // 
            this.lblMode.AutoSize = true;
            this.lblMode.Font = new System.Drawing.Font("Georgia", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMode.ForeColor = System.Drawing.Color.Lime;
            this.lblMode.Location = new System.Drawing.Point(73, 297);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(69, 27);
            this.lblMode.TabIndex = 0;
            this.lblMode.Text = "Mode";
            // 
            // pnlGameOver
            // 
            this.pnlGameOver.Controls.Add(this.lblBackToMenu);
            this.pnlGameOver.Controls.Add(this.lblFinalScore);
            this.pnlGameOver.Controls.Add(this.lblGameOver);
            this.pnlGameOver.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGameOver.Location = new System.Drawing.Point(0, 0);
            this.pnlGameOver.Name = "pnlGameOver";
            this.pnlGameOver.Size = new System.Drawing.Size(874, 835);
            this.pnlGameOver.TabIndex = 15;
            this.pnlGameOver.Visible = false;
            // 
            // lblBackToMenu
            // 
            this.lblBackToMenu.AutoSize = true;
            this.lblBackToMenu.Font = new System.Drawing.Font("Georgia", 22.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBackToMenu.ForeColor = System.Drawing.Color.Red;
            this.lblBackToMenu.Location = new System.Drawing.Point(70, 469);
            this.lblBackToMenu.Name = "lblBackToMenu";
            this.lblBackToMenu.Size = new System.Drawing.Size(733, 43);
            this.lblBackToMenu.TabIndex = 2;
            this.lblBackToMenu.Text = "Press BACKSPACE to return to Main Menu";
            // 
            // lblFinalScore
            // 
            this.lblFinalScore.AutoSize = true;
            this.lblFinalScore.Font = new System.Drawing.Font("Georgia", 22.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFinalScore.ForeColor = System.Drawing.Color.Cyan;
            this.lblFinalScore.Location = new System.Drawing.Point(303, 385);
            this.lblFinalScore.Name = "lblFinalScore";
            this.lblFinalScore.Size = new System.Drawing.Size(230, 43);
            this.lblFinalScore.TabIndex = 1;
            this.lblFinalScore.Text = "Final Score: ";
            // 
            // lblGameOver
            // 
            this.lblGameOver.AutoSize = true;
            this.lblGameOver.Font = new System.Drawing.Font("Georgia", 60F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGameOver.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblGameOver.Location = new System.Drawing.Point(70, 236);
            this.lblGameOver.Name = "lblGameOver";
            this.lblGameOver.Size = new System.Drawing.Size(715, 114);
            this.lblGameOver.TabIndex = 0;
            this.lblGameOver.Text = "GAME OVER";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(874, 835);
            this.Controls.Add(this.lblLives);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.lblLevel);
            this.Controls.Add(this.lblScreen);
            this.Controls.Add(this.lblStart);
            this.Controls.Add(this.pnlMainMenu);
            this.Controls.Add(this.pnlGameOver);
            this.Controls.Add(this.pnlModeSelect);
            this.Controls.Add(this.pnlControls);
            this.Controls.Add(this.pnlInstructions);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.KeyPreview = true;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Breakout";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.pnlMainMenu.ResumeLayout(false);
            this.pnlMainMenu.PerformLayout();
            this.pnlInstructions.ResumeLayout(false);
            this.pnlInstructions.PerformLayout();
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            this.pnlModeSelect.ResumeLayout(false);
            this.pnlModeSelect.PerformLayout();
            this.pnlGameOver.ResumeLayout(false);
            this.pnlGameOver.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmrGame;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label lblLives;
        private System.Windows.Forms.Panel pnlMainMenu;
        private System.Windows.Forms.Label lblMenuExit;
        private System.Windows.Forms.Label lblMenuMode;
        private System.Windows.Forms.Label lblMenuControls;
        private System.Windows.Forms.Label lblMenuInstructions;
        private System.Windows.Forms.Label lblMenuStart;
        private System.Windows.Forms.Panel pnlInstructions;
        private System.Windows.Forms.Label lblInstructions;
        private System.Windows.Forms.Panel pnlControls;
        private System.Windows.Forms.Label lblControls;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblInsTitle;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.Label lblScreen;
        private System.Windows.Forms.Panel pnlModeSelect;
        private System.Windows.Forms.Label lblModeText;
        private System.Windows.Forms.Label lblModeTitle;
        private System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.Panel pnlGameOver;
        private System.Windows.Forms.Label lblBackToMenu;
        private System.Windows.Forms.Label lblFinalScore;
        private System.Windows.Forms.Label lblGameOver;
    }
}

