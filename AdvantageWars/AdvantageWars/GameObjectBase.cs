using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.System;

public abstract class GameObjectBase
{
    protected Texture texture;
    private Sprite sprite;
    protected Vector2f currentPosition;

    public GameObjectBase(string texturePath, Vector2f startPosition)
    {
        texture = new Texture(texturePath);
        sprite = new Sprite(texture);
        currentPosition = startPosition;
        sprite.Position = currentPosition;
    }

    public virtual void Update()
    {
        sprite.Position = currentPosition;
    }

    public virtual void Draw(RenderWindow window)
    {
        window.Draw(sprite);
    }
}

