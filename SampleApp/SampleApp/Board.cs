using System.Windows.Forms;

namespace SampleApp
{
    internal class Board
    {
        public const int BoardWidth = 800;
        public const int BoardHeight = 600;

        public int Player1Score { get; private set; } = 0;
        public int Player2Score { get; private set; } = 0;

        public bool GameOver { get; private set; } = false;
        public string WinnerMessage { get; private set; } = "";

        private void UpdateScore(int player)
        {
            if (player == 1)
                Player1Score++;
            else if (player == 2)
                Player2Score++;
        }
        public bool CheckScore(Ball ball)
        {
            int ballX = ball.GetBallX();

            if (ballX < 0)
            {
                UpdateScore(2);
                CheckWinCondition();
                return true;
            }
            else if (ballX > BoardWidth)
            {
                UpdateScore(1);
                CheckWinCondition();
                return true;
            }

            return false;
        }

        private void CheckWinCondition()
        {
            if (Player1Score >= 5)
            {
                GameOver = true;
                WinnerMessage = "Player 1 Wins!";
            }
            else if (Player2Score >= 5)
            {
                GameOver = true;
                WinnerMessage = "Player 2 Wins!";
            }
        }

        public void ResetGame()
        {
            Player1Score = 0;
            Player2Score = 0;
            GameOver = false;
            WinnerMessage = "";
        }

        public int ReturnBoardWidth() => BoardWidth;
        public int ReturnBoardHeight() => BoardHeight;
    }
}