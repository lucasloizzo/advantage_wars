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
            //TODO ACTIVAR AUDIO ANTES DE ENTREGA
            //MusicManager.GetInstance().Play();
            Framerate.InitFrameRateSystem();

            while (game.UpdateWindow())
            {
                if (!Game.GetPauseStatus())
                {
                    game.UpdateGame();
                    game.CheckGarbage();
                    game.DrawGame();
                }
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
