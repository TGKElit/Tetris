
class Tetronimo
{
    private ConsoleColor[,] tetronimoArray = new ConsoleColor[4, 4];
    private char type;
    public (int, int) position = (-1, 3);
    public (int, int) positionBuffer;

    public Tetronimo(char type)
    {
        this.type = type;
    }

    public void generateTetronimo()
    {
       
        switch (type)
        {
            case 'I':
                for (int i = 0; i < 4; i++)
                {
                    tetronimoArray[2, i] = ConsoleColor.Cyan;
                }
                break;
            case 'J':
                for (int i = 0; i < 3; i++)
                {
                    tetronimoArray[2, i] = ConsoleColor.Blue;
                }
                tetronimoArray[1, 0] = ConsoleColor.Blue;
                break;
            case 'L':
                for (int i = 0; i < 3; i++)
                {
                    tetronimoArray[2, i] = ConsoleColor.DarkYellow;
                }
                tetronimoArray[1, 2] = ConsoleColor.DarkYellow;
                break;
            case 'O':
                for (int i = 1; i < 3; i++)
                {
                    for (int j = 1; j < 3; j++)
                    {
                        tetronimoArray[i, j] = ConsoleColor.Yellow;
                    }
                }
                break;
            case 'S':
                tetronimoArray[1, 1] = ConsoleColor.Green;
                tetronimoArray[1, 2] = ConsoleColor.Green;
                tetronimoArray[2, 2] = ConsoleColor.Green;
                tetronimoArray[2, 3] = ConsoleColor.Green;
                break;
            case 'T':
                tetronimoArray[1, 2] = ConsoleColor.DarkMagenta;
                tetronimoArray[2, 1] = ConsoleColor.DarkMagenta;
                tetronimoArray[2, 2] = ConsoleColor.DarkMagenta;
                tetronimoArray[2, 3] = ConsoleColor.DarkMagenta;
                break;
            case 'Z':
                tetronimoArray[1, 2] = ConsoleColor.Red;
                tetronimoArray[1, 3] = ConsoleColor.Red;
                tetronimoArray[2, 1] = ConsoleColor.Red;
                tetronimoArray[2, 2] = ConsoleColor.Red;
                break;
        }

    }

    public ConsoleColor[,] getTetronimoArray()
    {
        return tetronimoArray;
    }

    public void AddTetronimo()
    {
        generateTetronimo();
        int yPos = position.Item1;
        int xPos = position.Item2;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (tetronimoArray[i, j] != ConsoleColor.Black && yPos + i > 0 && xPos + j > 0 && yPos + i < 15 && xPos + j < 10)
                {
                    Game.GameField[yPos + i, xPos + j] = tetronimoArray[i, j];
                }

            }
        }
    }
    public void RemoveTetronimo()
    {
        generateTetronimo();
        int yPos = position.Item1;
        int xPos = position.Item2;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (tetronimoArray[i, j] != ConsoleColor.Black && yPos + i > 0 && xPos + j > 0 && yPos + i < 15 && xPos + j < 10)
                {
                    Game.GameField[yPos + i, xPos + j] = ConsoleColor.Black;
                }

            }
        }
    }

    public void MoveTetronimo()
    {
        int yPos = position.Item1;
        int xPos = position.Item2;
        bool collision = false;
        bool settled = false;
        RemoveTetronimo();
        if (Game.ticks == 5)
        {

            position.Item1++;
            Game.ticks = 0;
        }
        Game.ticks++;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                
                try
                {

                    if (tetronimoArray[i, j] != ConsoleColor.Black && Game.GameField[yPos + i + 1, xPos + j] != ConsoleColor.Black)
                    {
                        collision = true;
                    }
                }
                catch
                {
                    collision = true;
                }

            }
        }

        if (collision)
        {
            position = positionBuffer;
        }

        if (Game.ticks == 1 && position == positionBuffer)
        {
            settled = true;
        }

        AddTetronimo();

        positionBuffer = position;

        if (settled)
        {
            Game.tetronimo = new Tetronimo(Game.tetronimoTypes[Game.rnd.Next(7)]);
        }

    }

    public ConsoleColor[,] tempArray = new ConsoleColor[4, 4];
    public void Rotate()
    {
        tempArray = tetronimoArray.Clone() as ConsoleColor[,];

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                    tetronimoArray[i, j] = tempArray[3-j, i];
            }
        }
    }
}