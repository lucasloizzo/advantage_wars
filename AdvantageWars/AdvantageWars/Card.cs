using SFML.Graphics;
using SFML.System;
using System;


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

    }

    public Card LoadCard(string[] lines, int i)
    {
        this.cardValue = Convert.ToInt32(lines[i]);
        this.cardQuantity = Convert.ToInt32(lines[i + 1]);
        this.texturePath = "../../../" + lines[i + 2];
        texture = new Texture(texturePath);
        sprite = new Sprite(texture);
        //currentPosition = startPosition;
        //sprite.Position = currentPosition;
        return this;
    }

    public void SetCardPosition(Vector2f position)
    {
        currentPosition = position;
        sprite.Position = currentPosition;
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