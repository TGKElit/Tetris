//
// This is the game event logic that you can customize and cannibalize
// as needed. You should try to write your game in a modular way, avoid
// making one huge Game class.
//

using System.Linq.Expressions;

class Game
{
    ScheduleTimer? _timer;

    public static Random rnd = new Random();
    
    public static char[] tetronimoTypes = {'I', 'J', 'L', 'O', 'S', 'T', 'Z'};
    public bool Paused { get; private set; }
    public bool GameOver { get; private set; }

    public static ConsoleColor[,] GameField = new ConsoleColor[16, 11];

    private static ConsoleColor[,] GameFieldBuffer = new ConsoleColor[16, 11];

    public static Tetronimo tetronimo = new Tetronimo(tetronimoTypes[rnd.Next(7)]);
    
    public static int ticks = 0;

    public void Start()
    {
        for (int i = 0; i < GameField.GetLength(0); i++)
        {
            GameField[i, 0] = ConsoleColor.White;
            GameField[i, GameField.GetLength(1) - 1] = ConsoleColor.White;
        }
        for (int i = 0; i < GameField.GetLength(1); i++)
        {
            GameField[0, i] = ConsoleColor.White;
            GameField[GameField.GetLength(0) - 1, i] = ConsoleColor.White;
        }
        ScheduleNextTick();
    }

    public void Pause()
    {
        Console.WriteLine("Pause");
        Paused = true;
        _timer!.Pause();
    }

    public void Resume()
    {
        Console.WriteLine("Resume");
        Paused = false;
        _timer!.Resume();
    }

    public void Stop()
    {
        Console.WriteLine("Stop");
        GameOver = true;
    }

    public void Input(ConsoleKey key)
    {
        tetronimo.RemoveTetronimo();
        switch(key)
        {
            case ConsoleKey.LeftArrow:
                tetronimo.position.Item2--;
                break;
            case ConsoleKey.RightArrow:
                tetronimo.position.Item2++;
                break;
                case ConsoleKey.UpArrow:
                tetronimo.Rotate();
                break;
            case ConsoleKey.DownArrow:
                tetronimo.position.Item1++;
            break;
        }
    }

    void Tick()
    {
        //Update tetronimo position

        tetronimo.MoveTetronimo();

        Render();


        ScheduleNextTick();
    }

    void ScheduleNextTick()
    {
        // the game will automatically update itself every half a second, adjust as needed
        _timer = new ScheduleTimer(50, Tick);
    }

    private static void Render()
    {

        for (int i = 0; i < GameField.GetLength(0); i++)
        {
            for (int j = 0; j < GameField.GetLength(1); j++)
            {
                if (GameField[i, j] != GameFieldBuffer[i, j])
                {
                    Console.SetCursorPosition(2 * j, i);
                    Console.BackgroundColor = GameField[i, j];
                    Console.Write($"  ");
                    Console.ResetColor();
                }

            }
            Console.WriteLine();
        }
        Console.SetCursorPosition(0, 0);
        GameFieldBuffer = GameField.Clone() as ConsoleColor[,];
    }

    
}
