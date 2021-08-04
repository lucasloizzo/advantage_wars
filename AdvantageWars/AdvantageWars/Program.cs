using SFML.Audio;
using SFML.System;
using System;
using System.IO;
using SFML.Audio;


public class Program
{
    static void Main()
    {
        try
        {
            Game game = Game.GetInstance();
            Framerate.InitFrameRateSystem();
            MusicManager.GetInstance().Play();

            //TODO switch for main menu or gameplay
            while (game.UpdateWindow())
            {
                game.UpdateGame();
                game.DrawGame();
                Framerate.OnFrameEnd();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
            throw;
        }

    }
}
