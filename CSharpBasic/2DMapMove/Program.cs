namespace _2DMapMove
{
    internal class Program
    {
        static int[,] map = new int[4, 5]
            {
                { 3, 0, 0, 0, 0 },
                { 0, 0, 1, 1, 1 },
                { 1, 0, 0, 0, 0 },
                { 1, 0, 1, 1, 2 },
            };

        static int posY = 0;
        static int posX = 0;

        static int endY = 3;
        static int endX = 4;

        static void Main(string[] args)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.Write($"{map[i, j]} ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("---------------");

            while (posY != endY || posX != endX)
            {
                ConsoleKeyInfo c = Console.ReadKey(true);

                switch (c.Key)
                {
                    case ConsoleKey.RightArrow:
                        MoveRight();
                        break;
                    case ConsoleKey.LeftArrow:
                        MoveLeft();
                        break;
                    case ConsoleKey.UpArrow:
                        MoveUp();
                        break;
                    case ConsoleKey.DownArrow:
                        MoveDown();
                        break;
                }

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        Console.Write($"{map[i, j]} ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("---------------");
            }
        }

        static void MoveRight()
        {
            int nextY = posY;
            int nextX = posX + 1;

            if (nextX >= map.GetLength(1))
                return;

            if (map[nextY, nextX] == 1)
                return;

            map[posY, posX] = 0;
            posY = nextY;
            posX = nextX;
            map[posY, posX] = 3;
        }

        static void MoveLeft()
        {
            int nextY = posY;
            int nextX = posX - 1;

            if (nextX < 0)
                return;

            if (map[nextY, nextX] == 1)
                return;

            map[posY, posX] = 0;
            posY = nextY;
            posX = nextX;
            map[posY, posX] = 3;
        }

        static void MoveUp()
        {
            int nextY = posY - 1;
            int nextX = posX;

            if (nextY < 0)
                return;

            if (map[nextY, nextX] == 1)
                return;

            map[posY, posX] = 0;
            posY = nextY;
            posX = nextX;
            map[posY, posX] = 3;
        }

        static void MoveDown()
        {
            int nextY = posY + 1;
            int nextX = posX;

            if (nextY >= map.GetLength(0))
                return;

            if (map[nextY, nextX] == 1)
                return;

            map[posY, posX] = 0;
            posY = nextY;
            posX = nextX;
            map[posY, posX] = 3;
        }
    }
}