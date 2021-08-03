using SFML.Graphics;

public class Gameplay
{
    private Board board;
    private Deck mainDeck;
    private User user;
    private Opponent opponent;

    public Gameplay()
    {
        board = new Board("../../../resources/assets/board.png", new SFML.System.Vector2f(0.00f,0.00f));
        mainDeck = new Deck("../../../resources/data/mainDeck.deck");
        mainDeck.Shuffle(mainDeck);
        user = new User("Rean", mainDeck.SplitDeck(mainDeck, 0));
        opponent = new Opponent("Gilbert", mainDeck.SplitDeck(mainDeck, 1));
        //delete mainDeck
    }

    public void Update()
    {

    }

    public void Draw(RenderWindow window)
    {
        board.Draw(window);
    }
}

