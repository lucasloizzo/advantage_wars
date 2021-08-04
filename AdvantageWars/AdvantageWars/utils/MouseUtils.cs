using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.System;
using SFML.Window;


public static class MouseUtils
{

    private static RenderWindow window;
    private static Vector2f startCenter;
    public static void SetWindow(RenderWindow gameWindow)
    {
        window = gameWindow;
        startCenter = window.GetView().Center;
    }


}
