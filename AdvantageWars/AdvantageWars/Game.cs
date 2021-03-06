using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

public class Game
{
    private static Game instance;
    private Gameplay gameplay;
    private static Vector2f windowSize;
    private RenderWindow window;
    private static bool pause;

    public Game()
    {
        VideoMode videoMode = new VideoMode();
        videoMode.Width = 1920;
        videoMode.Height = 1080;

        window = new RenderWindow(videoMode, "Blade III");
        window.Closed += CloseWindow;
        window.SetFramerateLimit(Framerate.FRAMERATE_LIMIT);
        windowSize = window.GetView().Size;

        gameplay = new Gameplay();
        pause = false;
        MouseUtils.SetWindow(window);
    }

    public static void SetPause()
    {
        pause = true;
    }

    private void CloseWindow(object sender, EventArgs e)
    {
        window.Close();
    }

    public bool UpdateWindow()
    {
        window.DispatchEvents();
        return window.IsOpen;
    }

    public void UpdateGame()
    {
        gameplay.Update();
        windowSize = window.GetView().Size;
    }

    public void DrawGame()
    {
        gameplay.Draw(window);
        window.Display();
    }

    public void CheckGarbage()
    {
        gameplay.CheckGarbage();
    }

    public static Vector2f GetWindowSize()
    {
        return windowSize;
    }

    public static Game GetInstance()
    {
        if (instance == null)
        {
            instance = new Game();
        }
        return instance;
    }

    public static bool GetPauseStatus()
    {
        return pause;
    }
}

