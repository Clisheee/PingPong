﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{

    class Pong 
    {
        int width;
        int height;
        Board board;
        Paddle paddle1;
        Paddle paddle2;
        ConsoleKeyInfo keyInfo;
        ConsoleKey consoleKey;
        Ball ball;


        public Pong(int width, int height)
        {
            this.width = width;
            this.height = height;
            board = new Board(width, height);
            ball = new Ball(width/2, height/2, height, width);
        }
        public void Setup()
        {
            paddle1 = new Paddle(2, height);
            paddle2 = new Paddle(width - 2, height);
            keyInfo = new ConsoleKeyInfo();
            consoleKey = new ConsoleKey();
            ball.X = width / 2;
            ball.Y = height / 2;
            ball.Kierunek = 0;
        }
        void Input()
        {
            if (Console.KeyAvailable)
            {
                keyInfo = Console.ReadKey(true);
                consoleKey = keyInfo.Key;
            }
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Setup();
                board.Write();
                paddle1.Write();
                paddle2.Write();
                ball.Write();
                while(ball.X != 1 && ball.X != width - 1)
                {
                    Input();
                    switch (consoleKey)
                    {
                        case ConsoleKey.W:
                            paddle1.Up();
                            paddle2.Up();
                            break;
                        case ConsoleKey.S:
                            paddle1.Down();
                                paddle2.Down();
                            break;

                    }
                    consoleKey = ConsoleKey.A;
                    ball.Logic(paddle1,paddle2);
                    ball.Write();
                    Thread.Sleep(100);
                }
            }
        }

    }
    class Ball
    {
        public int X { set; get; }

        public int Y { set; get; }

        int zwrotX;
        int zwrotY;
        int i;
        int boardHeight;
        int boardWidth;

        public int Kierunek { set; get; }
        public Ball(int x, int y,int boardHeight, int boardWidth)
        {
            X = x;
            Y = y;
            this.boardHeight = boardHeight;
            this.boardWidth = boardWidth;

            zwrotX = -1;
            zwrotY = 1;
        }
        public void Logic(Paddle paddle1, Paddle paddle2)
        {
            Console.SetCursorPosition(X, Y);
            Console.Write("\0 ");
            if (Y <= 1 || Y >= boardHeight)
            {
                zwrotY *= -1;
            }
            if (((X == 3 || X == boardWidth - 3) && (paddle1.Y - (paddle1.Length / 2)) <= Y && (paddle1.Y + (paddle1.Length / 2)) > Y))
            {
                zwrotX *= -1;
                if (Y == paddle1.Y)
                {
                    Kierunek = 0;
                }
                if ((Y>= (paddle1.Y - (paddle1.Length/2)) && Y<paddle1.Y || (Y>paddle1.Y && Y<(paddle1.Y + (paddle1.Length / 2)))))
                {
                    Kierunek = 1;
                }
            }
                switch (Kierunek)
                {
                case 0:
                    X += zwrotX;
                    break;
                case 1:
                    X += zwrotX;
                    Y += zwrotY;
                    break;

                }

        }
        public void Write()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(X, Y);
            Console.Write("o");
            Console.ForegroundColor = ConsoleColor.White;
        }

    }

    class Paddle
    {
        public int X {set;get; }
        public int Y { set; get; }
        public int Length { set; get; }

        int boardHeight;

        public Paddle(int x, int boardHeight)
        {
            this.boardHeight = boardHeight;
            X = x;
            Y = boardHeight / 2;
            Length = boardHeight / 3;
        }
        public void Up()
        {
            if ((Y - 1 - (Length / 2)) != 0)
            {
                Console.SetCursorPosition(X, (Y + (Length / 2)) - 1);
                Console.Write("\0 ");
                Y--;
                Write();
            }
        }
        public void Down()
        {
            if ((Y + 1 + (Length / 2))!= boardHeight+2)
            {
                Console.SetCursorPosition(X,(Y - (Length / 2)));
                Console.Write("\0 ");
                Y++;
                Write();
            }
        }
        public void Write()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = (Y - (Length/2));i<(Y+(Length/2));i++)
            {
                Console.SetCursorPosition(X, i);
                Console.Write("|");
            }
            Console.ForegroundColor= ConsoleColor.White;
        }
    }
    

    public class Board
    {
        public int Height { get; set; }

        public int Width { get; set; }

        public Board()
        {
            Height = 40;
            Width = 120;
        }
        public Board(int width, int height)
        {
            Width = width;
            Height = height;

        }
        public void Write()
        {
            for(int i = 1; i <= Width; i++) 
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("-");
            
            }
            for (int i = 1; i <= Width; i++)
            {
                Console.SetCursorPosition(i, Height + 1);
                Console.Write("-");
            }
            for (int i = 1; i <= Height; i++)
            {
                Console.SetCursorPosition(0,i);
                Console.Write("|");
            }
            for (int i = 1; i <= Height; i++)
            {
                Console.SetCursorPosition((Width+1),i);
                Console.Write("|");
            }
            Console.SetCursorPosition(0, 0);
            Console.Write("╂");
            Console.SetCursorPosition(Width+1, 0);
            Console.Write("╂");
            Console.SetCursorPosition(0, Height+1);
            Console.Write("╂");
            Console.SetCursorPosition(Width+1, Height+1);
            Console.Write("╂");
        }
    }
}
