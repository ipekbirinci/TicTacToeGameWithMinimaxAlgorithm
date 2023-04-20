using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using testScript;

namespace Lab04
{
    public partial class Form1 : Form
    {
        private TicTacToe game;
        private bool playerX=true;
        private bool gameEnded=false;
        private Button[,] buttons;

        public Form1()
        {
            InitializeComponent();
            game = new TicTacToe();
            playerX = true;
            gameEnded = false;
            buttons = new Button[,]
            {
            {button1, button2, button3},
            {button4, button5, button6},
            {button7, button8, button9}
            };
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    buttons[row, col].Tag = row * 3 + col;
                    buttons[row, col].Click += new EventHandler(button_Click);
                }
            }

        }

        private void button_Click(object sender, EventArgs e)
        {
            if (gameEnded)
                return;

            Button button = (Button)sender;
            int row = (int)button.Tag / 3;
            int col = (int)button.Tag % 3;

            if (!game.PlaceMark(row, col, playerX ? TicTacToe.Mark.X : TicTacToe.Mark.O))
                return;

            button.Text = playerX ? "X" : "O";
            button.ForeColor = playerX ? Color.Red : Color.Blue;

            if (game.CheckForWinner() != TicTacToe.Mark.EMPTY)
            {
                gameEnded = true;
                string xOrO = (playerX ? "X" : "O");
                MessageBox.Show(xOrO+ " WINS!");
               
                testClass test = new testClass();
                int score = test.testFuncXoX(textBox1.Text, xOrO, button1.Text, button2.Text, button3.Text, button4.Text, button5.Text, button6.Text, button7.Text, button8.Text, button9.Text);
                label1.Text = "SCORE:"+score.ToString();
            }
            else if (game.IsBoardFull())
            {
                gameEnded = true;
                MessageBox.Show("Draw!");
                string winner = "draw";
                testClass test = new testClass();
                int score = test.testFuncXoX(textBox1.Text, winner, button1.Text, button2.Text, button3.Text, button4.Text, button5.Text, button6.Text, button7.Text, button8.Text, button9.Text);
                label1.Text = "SCORE:" + score.ToString();
            }
            else
            {
                playerX = !playerX;
                if (!playerX)
                    PerformComputerMove();
            }
        }

       
        private void PerformComputerMove()
        {
            int row, col;
            (row, col) = FindBestMove(game.board, TicTacToe.Mark.O);

            game.PlaceMark(row, col, TicTacToe.Mark.O);

            buttons[row, col].Text = "O";
            buttons[row, col].ForeColor = Color.Blue;

            if (game.CheckForWinner() != TicTacToe.Mark.EMPTY)
            {
                gameEnded = true;
                MessageBox.Show("AI WINS!!");
                string winner = "o";
                testClass test = new testClass();
                int score = test.testFuncXoX(textBox1.Text, winner, button1.Text, button2.Text, button3.Text, button4.Text, button5.Text, button6.Text, button7.Text, button8.Text, button9.Text);
                label1.Text = "SCORE:" + score.ToString();
            }
            else if (game.IsBoardFull())
            {
                gameEnded = true;
                MessageBox.Show("DRAW!");
                string winner = "draw";
                testClass test = new testClass();
                int score = test.testFuncXoX(textBox1.Text, winner, button1.Text, button2.Text, button3.Text, button4.Text, button5.Text, button6.Text, button7.Text, button8.Text, button9.Text);
                label1.Text = "SCORE:" + score.ToString();
            }
            else
            {
                playerX = true;
            }
        }

        private (int, int) FindBestMove(TicTacToe.Mark[,] board, TicTacToe.Mark playerMark)
        {
            int bestScore = int.MinValue;
            int row = -1;
            int col = -1;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == TicTacToe.Mark.EMPTY)
                    {
                        board[i, j] = playerMark;
                        int score = Minimax(board, false, TicTacToe.Mark.X);
                        board[i, j] = TicTacToe.Mark.EMPTY;

                        if (score > bestScore)
                        {
                            bestScore = score;
                            row = i;
                            col = j;
                        }
                    }
                }
            }

            return (row, col);
        }

        private int Minimax(TicTacToe.Mark[,] board, bool isMaximizing, TicTacToe.Mark playerMark)
        {
            TicTacToe.Mark winner = game.CheckForWinner();
            if (winner != TicTacToe.Mark.EMPTY)
            {
                if (winner == TicTacToe.Mark.O)
                    return 1;
                else
                    return -1;
            }
            else if (game.IsBoardFull())
                return 0;

            if (isMaximizing)
            {
                int bestScore = int.MinValue;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i, j] == TicTacToe.Mark.EMPTY)
                        {
                            board[i, j] = playerMark;
                            int score = Minimax(board, false, TicTacToe.Mark.X);
                            board[i, j] = TicTacToe.Mark.EMPTY;

                            if (score > bestScore)
                                bestScore = score;
                        }
                    }
                }
                return bestScore;
            }
            else
            {
                int bestScore = int.MaxValue;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i, j] == TicTacToe.Mark.EMPTY)
                        {
                            board[i, j] = TicTacToe.Mark.X;
                            int score = Minimax(board, true, TicTacToe.Mark.O);
                            board[i, j] = TicTacToe.Mark.EMPTY;

                            if (score < bestScore)
                                bestScore = score;
                        }
                    }
                }
                return bestScore;
            }
        }

        private void restartButton_Click(object sender, EventArgs e)
        {
            game = new TicTacToe(); // Yeni bir oyun nesnesi oluştur
            playerX = true;
            gameEnded = false;

            // Tüm düğmeleri temizle ve etiketleri sıfırla
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    buttons[row, col].Text = "";
                    buttons[row, col].Enabled = true;
                }
            }

            // Skoru sıfırla
            label1.Text = "SCORE:";
        }
    }
}
