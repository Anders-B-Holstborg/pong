//using system;
//using system.collections.generic;
//using system.linq;
//using system.text;
//using system.threading.tasks;

//namespace pong
//{
//    class moveballbackup
//    {
//        static void moveball()
//        {
//            if (arrayball_x != 7 && arrayball_x != 24)
//            {
//                if (arrayball_y != 4 && arrayball_y != 115)
//                {
//                    console.setcursorposition(arrayball_y, arrayball_x);
//                    console.foregroundcolor = consolecolor.black;
//                    console.write("o");
//                    arrayball_x = arrayball_x + balldirection_updown;
//                    arrayball_y = arrayball_y + balldirection_leftright;
//                    console.setcursorposition(arrayball_y, arrayball_x);
//                    console.foregroundcolor = consolecolor.white;
//                    console.write("o");
//                }
//                else if (arrayball_y == 4)
//                {
//                    console.setcursorposition(arrayball_y, arrayball_x);
//                    console.foregroundcolor = consolecolor.black;
//                    console.write("o");
//                    console.foregroundcolor = consolecolor.white;
//                    player2score++;
//                    newball = true;
//                }
//                else if (arrayball_y == 115)
//                {
//                    console.setcursorposition(arrayball_y, arrayball_x);
//                    console.foregroundcolor = consolecolor.black;
//                    console.write("o");
//                    console.foregroundcolor = consolecolor.white;
//                    player1score++;
//                    newball = true;
//                }
//            }
//            else if (arrayball_x == 7) // hit top border
//            {
//                balldirection_updown = balldirection_updown + 2; // change ball direction up->down
//                if (arrayball_y != 4 && arrayball_y != 115)
//                {
//                    console.setcursorposition(arrayball_y, arrayball_x);
//                    console.foregroundcolor = consolecolor.black;
//                    console.write("o");
//                    arrayball_x = arrayball_x + balldirection_updown;
//                    arrayball_y = arrayball_y + balldirection_leftright;
//                    console.setcursorposition(arrayball_y, arrayball_x);
//                    console.foregroundcolor = consolecolor.white;
//                    console.write("o");
//                }
//                else if (arrayball_y == 4)
//                {
//                    console.setcursorposition(arrayball_y, arrayball_x);
//                    console.foregroundcolor = consolecolor.black;
//                    console.write("o");
//                    console.foregroundcolor = consolecolor.white;
//                    player2score++;
//                    newball = true;
//                }
//                else if (arrayball_y == 115)
//                {
//                    console.setcursorposition(arrayball_y, arrayball_x);
//                    console.foregroundcolor = consolecolor.black;
//                    console.write("o");
//                    console.foregroundcolor = consolecolor.white;
//                    player1score++;
//                    newball = true;
//                }
//            }
//            else if (arrayball_x == 24) // hit bottom border
//            {
//                balldirection_updown = balldirection_updown - 2; // change ball direction down->up
//                if (arrayball_y != 4 && arrayball_y != 115)
//                {
//                    console.setcursorposition(arrayball_y, arrayball_x);
//                    console.foregroundcolor = consolecolor.black;
//                    console.write("o");
//                    arrayball_x = arrayball_x + balldirection_updown;
//                    arrayball_y = arrayball_y + balldirection_leftright;
//                    console.setcursorposition(arrayball_y, arrayball_x);
//                    console.foregroundcolor = consolecolor.white;
//                    console.write("o");
//                }
//                else if (arrayball_y == 4)
//                {
//                    console.setcursorposition(arrayball_y, arrayball_x);
//                    console.foregroundcolor = consolecolor.black;
//                    console.write("o");
//                    console.foregroundcolor = consolecolor.white;
//                    player2score++;
//                    newball = true;
//                }
//                else if (arrayball_y == 115)
//                {
//                    console.setcursorposition(arrayball_y, arrayball_x);
//                    console.foregroundcolor = consolecolor.black;
//                    console.write("o");
//                    console.foregroundcolor = consolecolor.white;
//                    player1score++;
//                    newball = true;
//                }
//            }
//        }
//    }
//}
