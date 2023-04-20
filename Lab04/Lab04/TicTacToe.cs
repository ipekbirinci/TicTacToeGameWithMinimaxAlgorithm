using System;
using System.Collections.Generic;
using System.Text;

namespace Lab04
{
    public class TicTacToe
    {
        public enum Mark { EMPTY, X, O };
        public Mark[,] board { get; set; }
        //private Mark[,] board;
        private Mark currentPlayerMark;

        public TicTacToe()
        {
            board = new Mark[3, 3];
            currentPlayerMark = Mark.X;
            InitializeBoard();
        }
        //oyun tahtası
        private void InitializeBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = Mark.EMPTY;
                }
            }
        }

        public bool PlaceMark(int row, int col, Mark mark)
        {
            if (row < 0 || row >= 3 || col < 0 || col >= 3 || board[row, col] != Mark.EMPTY)
                return false;

            board[row, col] = mark;
            return true;
        }

        public TicTacToe.Mark CheckForWinner()
        {
            // Check rows
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] != Mark.EMPTY && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                    return board[i, 0];
            }

            // Check columns
            for (int i = 0; i < 3; i++)
            {
                if (board[0, i] != Mark.EMPTY && board[0, i] == board[1, i] && board[1, i] == board[2, i])
                    return board[0, i];
            }

            // Check diagonals
            if (board[0, 0] != Mark.EMPTY && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
                return board[0, 0];

            if (board[2, 0] != Mark.EMPTY && board[2, 0] == board[1, 1] && board[1, 1] == board[0, 2])
                return board[2, 0];

            return Mark.EMPTY;
        }

        public bool IsBoardFull()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == Mark.EMPTY)
                        return false;
                }
            }

            return true;
        }


    }

}
