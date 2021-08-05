using SFML.Graphics;
using SFML.System;
using System;
using SFML.Window;

public class Card : GameObjectBase
{
    private int cardValue;
    private int cardQuantity;
    private string texturePath;

    public Card()
    {
    }

    public Card(string texturePath, Vector2f startPosition) : base(texturePath, startPosition)
    {
        sprite.Scale = new Vector2f(0.75f, 0.75f);
    }

    public Card LoadCard(string[] lines, int i)
    {
        this.cardValue = Convert.ToInt32(lines[i]);
        this.cardQuantity = Convert.ToInt32(lines[i + 1]);
        this.texturePath = "../../../" + lines[i + 2];
        texture = new Texture(texturePath);
        sprite = new Sprite(texture);
        return this;
    }

    public override void Update()
    {
    }

    public bool CardClicked()
    {
        if (MouseUtils.ClickOn(GetBounds(), Mouse.Button.Left))
        {
            return true;
        }
        return false;
    }

    public void SetCardPosition(Vector2f position)
    {
        currentPosition = position;
        sprite.Position = currentPosition;
    }

    public void SetCardSpriteScale(Card card, Vector2f scale)
    {
        card.sprite.Scale = scale;
    }

    public int GetCardValue()
    {
        return cardValue;
    }

    public int GetCardQuantity()
    {
        return cardQuantity;
    }
}