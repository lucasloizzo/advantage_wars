using SFML.Graphics;
using SFML.System;

public sealed class Board : GameObjectBase
{
    public Board(string texturePath, Vector2f startPosition) : base(texturePath, startPosition)
    {

    }
    //private Texture boardTexture;
    //private Sprite board;
    //
    //public Board()
    //{
    //    boardTexture = new Texture("../../../resources/assets/board.png");
    //    board = new Sprite(boardTexture);
    //    board.Scale = new Vector2f(1.0f, 1.0f);
    //}
    //
    //public void Draw(RenderWindow window)
    //{
    //    window.Draw(board);
    //}
}
