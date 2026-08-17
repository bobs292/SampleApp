using PongBoard;
using PongPaddle;
namespace PongBall
{
    public class Ball
    {
        public const int BallSize = 15;

        private int ballXSpeed = 6;
        private int ballYSpeed = 6;

        private int ballX = 390;
        private int ballY = 290;

        public void MoveBall(Board board, Paddle paddle)
        {
            ballX += ballXSpeed;
            ballY += ballYSpeed;

            if (ballY <= 0 || ballY >= board.ReturnBoardHeight() - BallSize)
            {
                ballYSpeed = -ballYSpeed;
            }

            if (ballX <= paddle.GetPaddleX(1) + Paddle.PaddleWidth &&
                ballX >= paddle.GetPaddleX(1) &&
                ballY + BallSize >= paddle.GetPaddleY(1) &&
                ballY <= paddle.GetPaddleY(1) + Paddle.PaddleHeight &&
                ballXSpeed < 0)
            {
                ballXSpeed = -ballXSpeed;
                ballX = paddle.GetPaddleX(1) + Paddle.PaddleWidth; //avoid double-bounce
            }

            if (ballX + BallSize >= paddle.GetPaddleX(2) &&
                ballX <= paddle.GetPaddleX(2) + Paddle.PaddleWidth &&
                ballY + BallSize >= paddle.GetPaddleY(2) &&
                ballY <= paddle.GetPaddleY(2) + Paddle.PaddleHeight &&
                ballXSpeed > 0)
            {
                ballXSpeed = -ballXSpeed;
                ballX = paddle.GetPaddleX(2) - BallSize; //avoid double-bounce
            }
        }

        public void ResetBall(bool serveTowardsPlayer2)
        {
            ballX = 390;
            ballY = 290;
            ballXSpeed = serveTowardsPlayer2 ? 6 : -6;
            ballYSpeed = 6;
        }

        public int GetBallX() => ballX;
        public int GetBallY() => ballY;
    }
}

