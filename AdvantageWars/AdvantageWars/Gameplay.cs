using SFML.Graphics;

public class Gameplay
{
    private Board board;

    public Gameplay()
    {
        board = new Board("../../../resources/assets/board.png", new SFML.System.Vector2f(0.00f,0.00f));
    }

    public void Update()
    {

    }

    public void Draw(RenderWindow window)
    {
        board.Draw(window);
    }
}

