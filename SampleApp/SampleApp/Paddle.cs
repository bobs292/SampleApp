namespace SampleApp
{
    internal class Paddle
    {
        public const int PaddleSpeed = 8;
        public const int PaddleHeight = 100;
        public const int PaddleWidth = 10;

        public const int Player1X = 50;
        public const int Player2X = 740;

        private int player1Y = 250;
        private int player2Y = 250;

        public void MovePaddle(int player, bool moveUp, bool moveDown, Board board)
        {
            if (player == 1)
            {
                if (moveUp && player1Y > 0)
                    player1Y -= PaddleSpeed;
                if (moveDown && player1Y < board.ReturnBoardHeight() - PaddleHeight)
                    player1Y += PaddleSpeed;
            }
            else if (player == 2)
            {
                if (moveUp && player2Y > 0)
                    player2Y -= PaddleSpeed;
                if (moveDown && player2Y < board.ReturnBoardHeight() - PaddleHeight)
                    player2Y += PaddleSpeed;
            }
        }

        public int GetPaddleY(int player)
        {
            return player == 1 ? player1Y : player2Y;
        }

        public int GetPaddleX(int player)
        {
            return player == 1 ? Player1X : Player2X;
        }
    }
}