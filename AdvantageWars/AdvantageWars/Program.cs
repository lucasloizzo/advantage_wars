using SFML.Audio;
using SFML.System;
using System;
using System.IO;


public class Program
{
    static void Main()
    {
        try
        {
            Game game = Game.GetInstance();
            Framerate.InitFrameRateSystem();
            //init music controller
            //switch for main menu or gameplay
            while (game.UpdateWindow())
            {
                game.UpdateGame();
                game.DrawGame();
                Framerate.OnFrameEnd();
            }
            //reset on frame end
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
            throw;
        }

    }
}
