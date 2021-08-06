using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SFML.Graphics;
using SFML.System;


public class Title
{
    private Text title;
    public Title(string text, Vector2f position, Color textColor)
    {
        Font arial = new Font(".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "resources" + Path.DirectorySeparatorChar + "fonts" + Path.DirectorySeparatorChar + "arial.ttf");
        title = new Text(text, arial);
        title.Position = position;
        title.FillColor = textColor;
    }

    public void SetSize(uint size)
    {
        title.CharacterSize = size;
    }

    public void SetStyle(int style)
    {
        title.Style = (Text.Styles)style;
    }

    public void SetOutlineColor(Color color)
    {
        title.OutlineColor = color;
    }

    public void Draw(RenderWindow window)
    {
        title.Draw(window, RenderStates.Default);
    }

    public void SetNewText(string newtext)
    {
        title.DisplayedString = newtext;
    }
}

