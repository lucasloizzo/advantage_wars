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

    public static bool MouseOver(FloatRect perimeter)
    {
        Vector2i relativeMousePosition = Mouse.GetPosition(window);
        Vector2f cammeraOffset = window.GetView().Center - startCenter;
        relativeMousePosition += (Vector2i)cammeraOffset;
        return perimeter.Contains(relativeMousePosition.X, relativeMousePosition.Y);
    }

    public static bool ClickOn(FloatRect perimeter, Mouse.Button button)
    {
        return MouseOver(perimeter) && Mouse.IsButtonPressed(button);
    }
}
