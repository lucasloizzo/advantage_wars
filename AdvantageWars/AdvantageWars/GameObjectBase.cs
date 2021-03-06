using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.System;

public abstract class GameObjectBase
{
    protected Texture texture;
    protected Sprite sprite;
    protected Vector2f currentPosition;
    public bool toDelete;

    public GameObjectBase()
    {
        toDelete = false;
    }

    public GameObjectBase(string texturePath, Vector2f startPosition)
    {
        texture = new Texture(texturePath);
        sprite = new Sprite(texture);
        currentPosition = startPosition;
        sprite.Position = currentPosition;
        toDelete = false;
    }

    public virtual void Delete()
    {
        sprite.Dispose();
        texture.Dispose();
        toDelete = true;
    }

    public virtual void Update()
    {
        sprite.Position = currentPosition;
    }

    public virtual void Draw(RenderWindow window)
    {
        window.Draw(sprite);
    }

    public static Vector2f GetSpriteSize(GameObjectBase gameObject)
    {
        float spritewidth = gameObject.texture.Size.X * gameObject.sprite.Scale.X;
        float spriteHeight = gameObject.texture.Size.Y * gameObject.sprite.Scale.Y;
        Vector2f spriteSize = new Vector2f(spritewidth, spriteHeight);
        return spriteSize;
    }

    public Vector2f GetPosition()
    {
        return sprite.Position;
    }

    public FloatRect GetBounds()
    {
        return sprite.GetGlobalBounds();
    }
}

