using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; // for Key-catching
using System.Runtime.InteropServices; // for implementing the GetASyncKeyState-method /w the dll-reference

namespace Pong
{
    class Program
    {
        // board variables
        public static int map_height = 29;
        public static int map_width = 119;
        public static int board_height = 26;
        public static int board_width = 114;
        public static string[,] map = new string[map_height, map_width];

        // player variables
        public static string player1Name;
        public static string player2Name;
        public static string winningPlayer;
        public static int maxNameLength = 10;
        public static int player1Score;
        public static int player2Score;
        public static int player1Paddle_Vertical = 6;
        public static int player1Paddle_Top = 14;
        public static int player1Paddle_Mid = 15;
        public static int player1Paddle_Bot = 16;
        public static int player2Paddle_Vertical = 113;
        public static int player2Paddle_Top = 14;
        public static int player2Paddle_Mid = 15;
        public static int player2Paddle_Bot = 16;
        public static int playerLastScore = 0;

        // Game over variables
        public static bool roundEnd;
        public static bool sessionEnd;
        public static bool gameEnd = false;
        public static string newGame;

        // Ball variables
        public static int arrayBall_X_Ini = 15;
        public static int arrayBall_Y_Ini = 72;
        public static int arrayBall_X = arrayBall_X_Ini;
        public static int arrayBall_Y = arrayBall_Y_Ini;
        public static int ballDirection_Updown_Ini = -1;
        public static int ballDirection_Leftright_Ini = -5;
        public static int ballDirectionModifier = (ballDirection_Leftright_Ini*-2);
        public static int ballDirection_Updown = ballDirection_Updown_Ini;
        public static int ballDirection_Leftright = ballDirection_Leftright_Ini;
        public static bool newBall = false;

        static void Main(string[] args)
        {
            Console.SetWindowSize(120, 30);

            // Game logic'ish
            int gameDiffDefault = 80;
            int gameDiff = gameDiffDefault;
            int timePassed = 0;
            int timePassedModifier = 0;
            
            do // Main app loop
            {
                sessionEnd = false;
                newGame = " ";

                // Get and store player names for the session
                Console.WriteLine("Player 1: ");
                player1Name = Console.ReadLine();
                player1Name = PlayerNames.GetP1Name(player1Name, maxNameLength);
                Console.WriteLine("Player 2: ");
                player2Name = Console.ReadLine();
                player2Name = PlayerNames.GetP2Name(player2Name, maxNameLength);
                Console.CursorVisible = false;

                Console.Clear();

                // Session loop
                do
                {
                    roundEnd = false;
                    Console.CursorVisible = false;

                    // Assign default map to array and board into map-array
                    AssignMap();
                    AssignBoard();

                    // Reset gamestate
                    player1Score = 0;
                    player2Score = 0;
                    arrayBall_X = arrayBall_X_Ini;
                    arrayBall_Y = arrayBall_Y_Ini;
                    ballDirection_Leftright = ballDirection_Leftright_Ini;
                    ballDirection_Updown = ballDirection_Updown_Ini;

                    // Draw Map + Board + Player Names
                    DrawMap();
                    DrawNames.DrawPlayerNames(player1Name, player2Name);
                    Score.DrawScore(player1Score, player2Score);

                    do // Game loop ----------------------------------------------------------------------------
                    {                        
                        DrawPaddles();
                        while (!newBall) // Round loop
                        {
                            PaddleMovement();
                            MoveBall();
                            Thread.Sleep(gameDiff);
                            if (timePassed == 10+timePassedModifier && gameDiff != 35)
                            {
                                gameDiff -= 5;
                                timePassed = 0;
                                timePassedModifier++;
                            }
                            timePassed++;
                        }

                        // Reset ball status
                        if (playerLastScore == 0)
                        {
                            arrayBall_X = arrayBall_X_Ini;
                            arrayBall_Y = arrayBall_Y_Ini;
                            ballDirection_Leftright = ballDirection_Leftright_Ini;
                        }
                        else
                        {
                            arrayBall_X = 17;
                            arrayBall_Y = 52;
                            ballDirection_Leftright = ballDirection_Leftright_Ini;
                            ballDirection_Leftright = ballDirection_Leftright_Ini*-1;
                        }

                        // Reset game diff
                        gameDiff = gameDiffDefault;
                        timePassed = 0;

                        Score.DrawScore(player1Score, player2Score);

                        // Winner + check for rematch/new game/quit
                        Win(player1Score, player2Score);
                        newBall = false;
                    } while (!roundEnd);// --------------------------------------------------------------------

                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.SetCursorPosition(5,28);
                    Console.Write("Player {0} has won! Press Enter to continue!", winningPlayer);
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.ReadLine();
                    Console.SetCursorPosition(5, 28);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("Press N+Enter for a new game, press Q+Enter to quit, or" +
                        " press Enter for a rematch!", winningPlayer);
                    NewGame();

                    Console.Clear();
                } while (!sessionEnd); // End of round            
            } while (!gameEnd); // End of app
        }

        static void Win(int p1Score, int p2Score)
        {
            if (p1Score > 10)
            {
                winningPlayer = player1Name;
                roundEnd = true;
            }
            else if (p2Score > 10)
            {
                winningPlayer = player2Name;
                roundEnd = true;
            }
        }

        static void AssignMap()
        {
            for (int y = 0; y < map_width; y++)
            {
                for (int x = 0; x < map_height - 2; x++)
                {
                    if (y == 1 || y == map_width - 1)
                    {
                        map[x, y] = "|";
                    }
                    else if (x == 4 && y != 0 || x == map_height - 3 && y != 0)
                    {
                        map[x, y] = "_";
                    }
                    else
                    {
                        map[x, y] = " ";
                    }
                }
            }
            map[2, 59] = "-";
            map[2, 60] = "-";
            map[1, 21] = "U";
            map[1, 22] = "p";
            map[1, 23] = ":";
            map[1, 25] = "W";
            map[3, 19] = "D";
            map[3, 20] = "o";
            map[3, 21] = "w";
            map[3, 22] = "n";
            map[3, 23] = ":";
            map[3, 25] = "S";

            map[1, 96] = "U";
            map[1, 97] = "p";
            map[1, 98] = ":";
            map[1, 100] = "O";
            map[3, 94] = "D";
            map[3, 95] = "o";
            map[3, 96] = "w";
            map[3, 97] = "n";
            map[3, 98] = ":";
            map[3, 100] = "L";
            map[arrayBall_X, arrayBall_Y] = "O";
        }

        static void DrawMap()
        {
            Console.Clear();
            for (int x = 0; x < map_height; x++)
            {
                for (int y = 0; y < map_width; y++)
                {
                    Console.Write(map[x, y]);                   
                }
                Console.WriteLine();
            }
        }
        static void AssignBoard()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            for (int y = 6; y < board_width; y++)
            {
                for (int x = 6; x < board_height; x++)
                {
                    if (x == 6 || x == board_height - 1)
                    {
                        map[x, y] = "@";
                    }
                }
            }
        }
        static void DrawPaddles()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(player2Paddle_Vertical, player2Paddle_Top);
            Console.Write("X");
            Console.SetCursorPosition(player2Paddle_Vertical, player2Paddle_Mid);
            Console.Write("X");
            Console.SetCursorPosition(player2Paddle_Vertical, player2Paddle_Bot);
            Console.Write("X");
            Console.SetCursorPosition(player1Paddle_Vertical, player1Paddle_Top);
            Console.Write("X");
            Console.SetCursorPosition(player1Paddle_Vertical, player1Paddle_Mid);
            Console.Write("X");
            Console.SetCursorPosition(player1Paddle_Vertical, player1Paddle_Bot);
            Console.Write("X");
        }

        // Creating the C++-method "GetAsynchKeyState()" to circumvent all the loop errors
        // PS: YEEEEESSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS IT FUCKING WORKS
        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(Keys vKey);

        static void PaddleMovement()
        {
            if ((GetAsyncKeyState(Keys.W) & 0x8000) != 0)
            {
                if (player1Paddle_Top > 7)
                {
                    Console.SetCursorPosition(player1Paddle_Vertical, player1Paddle_Bot);
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("X");
                    player1Paddle_Top = player1Paddle_Top - 1;
                    player1Paddle_Mid = player1Paddle_Mid - 1;
                    player1Paddle_Bot = player1Paddle_Bot - 1;
                    Console.SetCursorPosition(player1Paddle_Vertical, player1Paddle_Top);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("X");
                }
                else { }
            }
            else if ((GetAsyncKeyState(Keys.S) & 0x8000) != 0)
            {
                if (player1Paddle_Top < 22)
                {
                    Console.SetCursorPosition(player1Paddle_Vertical, player1Paddle_Top);
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("X");
                    player1Paddle_Top = player1Paddle_Top + 1;
                    player1Paddle_Mid = player1Paddle_Mid + 1;
                    player1Paddle_Bot = player1Paddle_Bot + 1;
                    Console.SetCursorPosition(player1Paddle_Vertical, player1Paddle_Bot);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("X");
                }
                else { }
            }

            if ((GetAsyncKeyState(Keys.O) & 0x8000) != 0)
            {
                if (player2Paddle_Top > 7)
                {
                    Console.SetCursorPosition(player2Paddle_Vertical, player2Paddle_Bot);
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("X");
                    player2Paddle_Top = player2Paddle_Top - 1;
                    player2Paddle_Mid = player2Paddle_Mid - 1;
                    player2Paddle_Bot = player2Paddle_Bot - 1;
                    Console.SetCursorPosition(player2Paddle_Vertical, player2Paddle_Top);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("X");
                }
                else { }
            }
            else if ((GetAsyncKeyState(Keys.L) & 0x8000) != 0)
            {
                if (player2Paddle_Top < 22)
                {
                    Console.SetCursorPosition(player2Paddle_Vertical, player2Paddle_Top);
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("X");
                    player2Paddle_Top = player2Paddle_Top + 1;
                    player2Paddle_Mid = player2Paddle_Mid + 1;
                    player2Paddle_Bot = player2Paddle_Bot + 1;
                    Console.SetCursorPosition(player2Paddle_Vertical, player2Paddle_Bot);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("X");
                }
                else { }
            }
        }

        static void MoveBall()
        {
            if (arrayBall_X == 7) // Hit top border
            {
                ballDirection_Updown += 2; // Change ball direction up->down
            }
            else if (arrayBall_X == 24) // Hit bottom border
            {
                ballDirection_Updown -= 2; // Change ball direction down->up
            }

            if (ballDirection_Updown == -1) // Check for paddle impact in relation to direction of ball
            {
                if (((arrayBall_Y == player1Paddle_Vertical + 1) && arrayBall_X == player1Paddle_Top) ||
                    ((arrayBall_Y == player1Paddle_Vertical + 1) && arrayBall_X == player1Paddle_Top + 1) ||
                    ((arrayBall_Y == player1Paddle_Vertical + 1) && arrayBall_X == player1Paddle_Mid + 1) ||
                    ((arrayBall_Y == player1Paddle_Vertical + 1) && arrayBall_X == player1Paddle_Bot + 1))
                {
                    ballDirection_Leftright += ballDirectionModifier;
                }
                if (((arrayBall_Y == player2Paddle_Vertical - 1) && arrayBall_X == player2Paddle_Top) ||
                    ((arrayBall_Y == player2Paddle_Vertical - 1) && arrayBall_X == player2Paddle_Top + 1) ||
                    ((arrayBall_Y == player2Paddle_Vertical - 1) && arrayBall_X == player2Paddle_Mid + 1) ||
                    ((arrayBall_Y == player2Paddle_Vertical - 1) && arrayBall_X == player2Paddle_Bot + 1))
                {
                    ballDirection_Leftright -= ballDirectionModifier;
                }
            }
            else
            {
                if (((arrayBall_Y == player1Paddle_Vertical + 1) && arrayBall_X == player1Paddle_Bot) ||
                    ((arrayBall_Y == player1Paddle_Vertical + 1) && arrayBall_X == player1Paddle_Top - 1) ||
                    ((arrayBall_Y == player1Paddle_Vertical + 1) && arrayBall_X == player1Paddle_Mid - 1) ||
                    ((arrayBall_Y == player1Paddle_Vertical + 1) && arrayBall_X == player1Paddle_Bot - 1))
                {
                    ballDirection_Leftright += ballDirectionModifier;
                }
                if (((arrayBall_Y == player2Paddle_Vertical - 1) && arrayBall_X == player2Paddle_Bot) ||
                    ((arrayBall_Y == player2Paddle_Vertical - 1) && arrayBall_X == player2Paddle_Top - 1) ||
                    ((arrayBall_Y == player2Paddle_Vertical - 1) && arrayBall_X == player2Paddle_Mid - 1) ||
                    ((arrayBall_Y == player2Paddle_Vertical - 1) && arrayBall_X == player2Paddle_Bot - 1))
                {
                    ballDirection_Leftright -= ballDirectionModifier;
                }
            }

            Console.SetCursorPosition(arrayBall_Y, arrayBall_X);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("O");
            arrayBall_X = arrayBall_X + ballDirection_Updown;
            arrayBall_Y = arrayBall_Y + ballDirection_Leftright;
            Console.SetCursorPosition(arrayBall_Y, arrayBall_X);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("O");

            if (arrayBall_Y == 2)
            {
                Console.SetCursorPosition(arrayBall_Y, arrayBall_X);
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("O");
                Console.ForegroundColor = ConsoleColor.White;
                player2Score++;
                playerLastScore = 0;
                GoalGraphicP2();
                newBall = true;
            }
            else if (arrayBall_Y == 117)
            {
                Console.SetCursorPosition(arrayBall_Y, arrayBall_X);
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("O");
                Console.ForegroundColor = ConsoleColor.White;
                player1Score++;
                playerLastScore = 1;
                GoalGraphicP1();
                newBall = true;
            }             
        }
        static void GoalGraphicP1()
        {
            for (int x = 0; x < 4; x++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                for (int i = 6; i < 26; i++)
                {
                    Console.SetCursorPosition(114, i);
                    Console.Write("$");
                    Console.SetCursorPosition(115, i);
                    Console.Write("$");
                    Console.SetCursorPosition(116, i);
                    Console.Write("$");
                }
                Thread.Sleep(200);

                Console.ForegroundColor = ConsoleColor.Black;
                for (int j = 6; j < 26; j++)
                {
                    Console.SetCursorPosition(114, j);
                    Console.Write("$");
                    Console.SetCursorPosition(115, j);
                    Console.Write("$");
                    Console.SetCursorPosition(116, j);
                    Console.Write("$");
                }
                Thread.Sleep(200);
            }
        }

        static void GoalGraphicP2()
        {
            for (int x = 0; x < 4; x++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                for (int i = 6; i < 26; i++)
                {
                    Console.SetCursorPosition(3, i);
                    Console.Write("$");
                    Console.SetCursorPosition(4, i);
                    Console.Write("$");
                    Console.SetCursorPosition(5, i);
                    Console.Write("$");
                }
                Thread.Sleep(200);

                Console.ForegroundColor = ConsoleColor.Black;
                for (int j = 6; j < 26; j++)
                {
                    Console.SetCursorPosition(3, j);
                    Console.Write("$");
                    Console.SetCursorPosition(4, j);
                    Console.Write("$");
                    Console.SetCursorPosition(5, j);
                    Console.Write("$");
                }
                Thread.Sleep(200);
            }
        }

        static void NewGame()
        {
            // Bugged, catches all input from game
            // What if we use GetAsyncKeyState for this logic chain?
            Console.CursorVisible = true;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(5, 29);
            newGame = Console.ReadLine().ToUpper();

            if (newGame == "N")
            {
                sessionEnd = true;
            }
            else if (newGame == "Q")
            {
                sessionEnd = true;
                gameEnd = true;
            }
        }
    }
}
