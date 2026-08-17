using System;
using System.Drawing;
using System.Windows.Forms;

namespace SampleApp
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PongForm());
        }
    }

    public class PongForm : Form
    {
        private readonly Board board = new Board();
        private readonly Paddle paddle = new Paddle();
        private readonly Ball ball = new Ball();

        private bool moveP1Up, moveP1Down, moveP2Up, moveP2Down;

        private readonly Timer gameTimer = new Timer();

        public PongForm()
        {
            this.DoubleBuffered = true; // Stops flicker
            this.ClientSize = new Size(Board.BoardWidth, Board.BoardHeight);
            this.BackColor = Color.Black;
            this.Text = "Pong";

            this.Paint += PongForm_Paint;
            this.KeyDown += PongForm_KeyDown;
            this.KeyUp += PongForm_KeyUp;

            gameTimer.Interval = 20;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (board.GameOver)
            {
                return; //keep screen static
            }

            if (moveP1Up) paddle.MovePaddle(1, true, false, board);
            if (moveP1Down) paddle.MovePaddle(1, false, true, board);
            if (moveP2Up) paddle.MovePaddle(2, true, false, board);
            if (moveP2Down) paddle.MovePaddle(2, false, true, board);

            ball.MoveBall(board, paddle);

            bool scored = board.CheckScore(ball);
            if (scored)
            {
                bool serveTowardsPlayer2 = ball.GetBallX() < 0;
                ball.ResetBall(serveTowardsPlayer2);

                if (board.GameOver)
                {
                    MessageBox.Show(board.WinnerMessage);
                    board.ResetGame();
                    ball.ResetBall(true);
                }
            }

            this.Invalidate(); // repaint
        }

        private void PongForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            using (var font = new Font("Arial", 16))
            {
                g.DrawString($"Player 1: {board.Player1Score}", font, Brushes.White, new PointF(10, 10));
                g.DrawString($"Player 2: {board.Player2Score}", font, Brushes.White, new PointF(Board.BoardWidth - 150, 10));
            }

            // Paddles
            g.FillRectangle(Brushes.White, paddle.GetPaddleX(1), paddle.GetPaddleY(1), Paddle.PaddleWidth, Paddle.PaddleHeight);
            g.FillRectangle(Brushes.White, paddle.GetPaddleX(2), paddle.GetPaddleY(2), Paddle.PaddleWidth, Paddle.PaddleHeight);

            // Ball
            g.FillEllipse(Brushes.White, ball.GetBallX(), ball.GetBallY(), Ball.BallSize, Ball.BallSize);
        }

        private void PongForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W) moveP1Up = true;
            if (e.KeyCode == Keys.S) moveP1Down = true;
            if (e.KeyCode == Keys.Up) moveP2Up = true;
            if (e.KeyCode == Keys.Down) moveP2Down = true;
        }

        private void PongForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W) moveP1Up = false;
            if (e.KeyCode == Keys.S) moveP1Down = false;
            if (e.KeyCode == Keys.Up) moveP2Up = false;
            if (e.KeyCode == Keys.Down) moveP2Down = false;
        }
    }
}