using SFML.Graphics;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

public class Gameplay
{
    private Board board;
    private Deck mainDeck;
    private User user;
    private Opponent opponent;

    public Gameplay()
    {
        board = new Board(".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "resources" + Path.DirectorySeparatorChar + "assets" + Path.DirectorySeparatorChar + "board.png", new SFML.System.Vector2f());
        mainDeck = new Deck(".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "resources" + Path.DirectorySeparatorChar + "data" + Path.DirectorySeparatorChar + "mainDeck.deck");
        mainDeck.Shuffle(mainDeck);
        user = new User("Rean", mainDeck.SplitDeck(mainDeck, 0), PlayerID.User);
        opponent = new Opponent("Gilbert", mainDeck.SplitDeck(mainDeck, 1), PlayerID.Opponnent);
        //TODO delete mainDeck
    }

    public void Update()
    {

    }

    public void Draw(RenderWindow window)
    {
        board.Draw(window);
        if (user.GetDeck().GetCardsInDeck() > 0)
        {
            int totaCardsinUserDeck = user.GetDeck().GetCardsInDeck();
            Card cardToDraw = user.GetDeck().GetCard(totaCardsinUserDeck - 1);
            cardToDraw.Draw(window);
        }
        
        if (opponent.GetDeck().GetCardsInDeck() > 0)
        {
            int totaCardsinUserDeck = opponent.GetDeck().GetCardsInDeck();
            Card cardToDraw = opponent.GetDeck().GetCard(totaCardsinUserDeck - 1);
            cardToDraw.Draw(window);
        }
    }
}

