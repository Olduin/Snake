using System;
using System.Threading;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            //Const
            int screenWidth = 80;
            int screenHeigh = 25;
            int snakeMaxLeinght = 100;
            int defaultPeriod = 120;
            


            //ControlGame.GameState gameState;

            SnakeModel snake = new SnakeModel();
            ControlPlayer.StatePlayer controlPlayer = new ControlPlayer.StatePlayer();
            //controlPlayer = ControlPlayer.StatePlayer.down;
            ControlGame.GameState gameState = new ControlGame.GameState();
                                 

            DateTime nextTick = new DateTime();
            int period;


            void ReadControl()
            {
                ConsoleKeyInfo input = Console.ReadKey(true);

                if (Console.KeyAvailable == true)
                {
                    if(gameState== ControlGame.GameState.pause)
                    {
                        gameState = ControlGame.GameState.run;
                    }
                }

                switch (input.Key)
                {
                    case ConsoleKey.Escape:
                        gameState = ControlGame.GameState.pause;
                        break;
                    case ConsoleKey.W:
                        controlPlayer = ControlPlayer.StatePlayer.up;
                        break;
                    case ConsoleKey.S:
                        controlPlayer = ControlPlayer.StatePlayer.down;
                        break;
                    case ConsoleKey.A:
                        controlPlayer = ControlPlayer.StatePlayer.left;
                        break;
                    case ConsoleKey.D:
                        controlPlayer = ControlPlayer.StatePlayer.right;
                        break;
                }
            }

            void GameArenaDrow()
            {
                int counterLine;
                string fillLine = "";
                string verticalPlaceholder = ".";

                

                Console.SetCursorPosition(2, 2);
                Console.WriteLine(fillLine.PadRight(screenWidth, '.'));

                Console.SetCursorPosition(2, screenHeigh);
                Console.WriteLine(fillLine.PadRight(screenWidth, '.'));
       
                for (counterLine = 2; counterLine <= screenHeigh; counterLine++)
                {
                    Console.SetCursorPosition(2, counterLine);
                    Console.WriteLine(verticalPlaceholder);
                }

                for (counterLine = 2; counterLine <= screenHeigh; counterLine++)
                {
                    Console.SetCursorPosition(screenWidth, counterLine);
                    Console.WriteLine(verticalPlaceholder);
                }

            }

            void WaitTick()
            {
                int waitTime;
                DateTime nowTime = DateTime.Now;
                period = defaultPeriod;

                if (nowTime < nextTick)
                {
                    waitTime = nextTick.Millisecond-nowTime.Millisecond;
                    Thread.Sleep(waitTime);
                }                

                nextTick = nowTime.AddMilliseconds(period);
            }

            void PutFood()
            {
                Random random = new Random();
                FoodModel food = new FoodModel();                

                food.position.x = random.Next(5, screenWidth - 5);
                food.position.y = random.Next(4, screenHeigh - 4);                      

                Console.SetCursorPosition(food.position.x, food.position.y);
                Console.Write('0');

            }

            void SnakeControl()
            {
                int bodyCoursor;

                if(gameState == ControlGame.GameState.run)
                {
                    bodyCoursor = snake.lenght + 1;
                    do
                    {
                        snake.body[bodyCoursor] = snake.body[bodyCoursor - 1];
                        bodyCoursor = bodyCoursor - 1;
                    }
                    while (bodyCoursor > 1);

                    switch (controlPlayer)
                    {

                        case ControlPlayer.StatePlayer.up:
                            snake.body[1].y = snake.body[1].y - 1;
                            break;
                        case ControlPlayer.StatePlayer.down:
                            snake.body[1].y = snake.body[1].y + 1;
                            break;
                        case ControlPlayer.StatePlayer.left:
                            snake.body[1].x = snake.body[1].x - 1;
                            break;
                        case ControlPlayer.StatePlayer.right:
                            snake.body[1].x = snake.body[1].x + 1;
                            break;
                    }
                }
            }

            void SnakeDrow()
            {
                Console.SetCursorPosition(snake.body[1].x, snake.body[1].y);
                Console.Write('*');

                Console.SetCursorPosition(snake.body[snake.lenght+1].x, snake.body[snake.lenght + 1].y);
                Console.Write(' ');
            }

            void SnakeEat()
            {
                snake.lenght = snake.lenght + 1;
                PutFood();
            }

            void DetectCollision()
            {
                int bodyCoursor;
                DisplayPosition snakeHead;

                //Пересечение с игровой областью
                if (snake.body[1].x == 1) {if (controlPlayer == ControlPlayer.StatePlayer.left){gameState = ControlGame.GameState.gameOver;}}
                if (snake.body[1].x == screenWidth) { if (controlPlayer == ControlPlayer.StatePlayer.right) { gameState = ControlGame.GameState.gameOver; } }
                if (snake.body[1].y == 1) { if (controlPlayer == ControlPlayer.StatePlayer.up) { gameState = ControlGame.GameState.gameOver; } }
                if (snake.body[1].y == screenHeigh) { if (controlPlayer == ControlPlayer.StatePlayer.down) { gameState = ControlGame.GameState.gameOver; } }

                //Пересечение с телом
                snakeHead = snake.body[1];
                for (bodyCoursor = 2; bodyCoursor < snake.lenght; bodyCoursor++) 
                {
                    if ((snakeHead.x==snake.body[bodyCoursor].x) && (snakeHead.y == snake.body[bodyCoursor].y))
                    {
                        gameState = ControlGame.GameState.gameOver;
                    }
                }
            }

            void InitGame()
            {
                Random random = new Random();

                GameArenaDrow();
                gameState = ControlGame.GameState.pause;
                period = defaultPeriod;
                snake.lenght = 1;


                int x = random.Next(5, screenWidth - 5);
                int y = random.Next(5, screenHeigh - 5);
                snake.AddBody(0, x, y);



                //snake.body[1].x = random.Next(5, screenWidth - 5);
                //snake.body[1].y = random.Next(5, screenHeigh - 5);
                PutFood();
            }

            Console.Clear();

            Console.SetWindowSize(screenWidth, screenHeigh);

            InitGame();

            GameArenaDrow();

            SnakeDrow();
           
            ReadControl();

            SnakeControl();

            DetectCollision();

            WaitTick();
            
            //Console.WriteLine("Hello World!");
            Console.SetCursorPosition(screenWidth+1, screenHeigh+1);

        }
    }
}
