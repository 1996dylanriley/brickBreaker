using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public int PaddleX { get; set; }
        public int BallX { get; set; }
        public int BallY { get; set; }

        const int PADDLEWIDTH = 40;
        const int PADDLEHEIGHT = 10;
        const int BRICKWIDTH = 40;
        const int BRICKHEIGHT = 20;

        public int XDir { get; set; }
        public int YDir { get; set; }

        List<Rectangle> bricksList = new List<Rectangle>();
        List<Rectangle> hitLsit = new List<Rectangle>();

        SolidBrush myBrush = new SolidBrush(Color.Red);
        SolidBrush blue = new SolidBrush(Color.Blue);

        bool ballStopped = false;
        bool won = false;

        Graphics formGraphics;

        public Form1()
        {
            InitializeComponent();
            formGraphics = this.panel1.CreateGraphics();
            PaddleX = this.panel1.Width / 2;
            XDir = 2;
            YDir = 2;
            BallX = PaddleX + 2;
            BallY = PaddleX + 2;
            DrawBall(BallX, BallY);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (!won)
            {
                DrawBricks();
                DrawPaddle(PaddleX, 400);
                DrawBall(BallX, BallY);
                if (ballStopped)
                {
                    BallX = PaddleX + 20;
                    BallY = 350;
                    YDir = 0;
                    XDir = 0;


                }
            }
            else
            {
                panel1.BackColor = Color.Tomato;
            }
            

           

        }
        private void DrawBall(int x, int y)
        {
            if (BallX >= panel1.Width)
            {
                XDir = -2;
            }
            if (BallX <= 10)
            {
                XDir = 2;
            }
            if (BallY >= panel1.Height)
            {
                ballStopped = true;
                
            }
            if (BallY <= 10)
            {
                YDir = 2;
            }
            if((BallY == 400) && (BallX >= PaddleX && BallX <= PaddleX + 40))
            {
                YDir -= 2;
                
                XDir = XDir > 0 ? 2 : -2;

            }

            BallX += XDir;
            BallY += YDir;
            //if (BallX > 0)
            formGraphics.FillEllipse(myBrush, new Rectangle(BallX, BallY, 10, 10));


        }
        private void DrawBricks()
        {
            int spacing = 5;
            int brickPosY = 0;
            int brickPosX = 20;
            if (hitLsit.Count == 30)
                won = true;

            if (bricksList.Count == 0)
            {
                for (int x = 0; x < 30; x++)
                {
                    bricksList.Add(
                     new Rectangle(brickPosX, brickPosY, BRICKWIDTH, BRICKHEIGHT));



                    if (brickPosX > panel1.Width - 90)
                    {
                        brickPosY += BRICKHEIGHT + spacing;
                        brickPosX = 20;
                    }
                    else
                    {
                        brickPosX += BRICKWIDTH + spacing;
                    }
                    formGraphics.FillRectangle(myBrush,
                    bricksList[x]);
                }
            }
            else
            {
                foreach (var brick in bricksList)
                {
                    if ((BallY >= brick.Y && BallY <= brick.Y + 20) && (BallX >= brick.X && BallX <= brick.X + 40) && hitLsit.Contains(brick) == false)
                    {

                        
                        if (YDir >= 0)
                        {
                            YDir = -2;
                        }

                        else
                        {
                            YDir = +2;

                        }
                        if (XDir >= 0)
                        {
                            XDir = +2;
                        }

                        else
                        {
                            XDir = -2;

                        }
                        if (hitLsit.Contains(brick) == false)
                        {                                                        
                            hitLsit.Add(brick);
                        }
                        formGraphics.FillRectangle(blue,
                        brick);
                    }

                   
                    else if (hitLsit.Contains(brick))
                        formGraphics.FillRectangle(blue,
                    brick);
                    else
                    {
                        formGraphics.FillRectangle(myBrush,
                    brick);
                    }




                }
            }




        }
        private void DrawPaddle(int x, int y)
        {
            formGraphics.FillRectangle(myBrush,
                new Rectangle(PaddleX, 400, PADDLEWIDTH, PADDLEHEIGHT));
        }

        private void CheckBallToBrickCollision()
        {
        }
        private void CheckBallToPaddleCollision()
        {
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            PaddleX = e.X;
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            panel1.Invalidate();
            panel1.Refresh();
        }
       

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {

            if (ballStopped == true)
            {
                XDir = PaddleX > panel1.Width / 2 ? -2 : 2;

                YDir = -2;
                
                ballStopped = false;
            }
        }
    }
}
