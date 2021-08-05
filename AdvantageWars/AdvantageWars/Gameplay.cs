using SFML.Graphics;
using SFML.System;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

public class Gameplay
{
    private Board board;
    private Deck mainDeck;
    private User user;
    private Title userName;
    private Title userPoints;
    private Opponent opponent;
    private Title opponentName;
    private Title opponentPoints;


    public Gameplay()
    {
        board = new Board(".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "resources" + Path.DirectorySeparatorChar + "assets" + Path.DirectorySeparatorChar + "board.png", new SFML.System.Vector2f());
        mainDeck = new Deck(".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "resources" + Path.DirectorySeparatorChar + "data" + Path.DirectorySeparatorChar + "mainDeck.deck");
        mainDeck.Shuffle(mainDeck);
        user = new User("Rean", mainDeck.SplitDeck(mainDeck, 0), PlayerID.User);
        userName = new Title(user.GetPlayerName(), new Vector2f(100.0f, 590.0f), Color.Green);
        userPoints = new Title("0", new Vector2f(850.0f, 580.0f), Color.Yellow);
        userPoints.SetSize(72);
        userPoints.SetStyle(1);

        opponent = new Opponent("Gilbert", mainDeck.SplitDeck(mainDeck, 1), PlayerID.Opponnent);
        opponentName = new Title(opponent.GetPlayerName(), new SFML.System.Vector2f(1700.0f, 410.0f), Color.Green);
        opponentPoints = new Title("0", new SFML.System.Vector2f(1050.0f, 380.0f), Color.Yellow);
        opponentPoints.SetSize(72);
        opponentPoints.SetStyle(1);
        //TODO delete mainDeck
    }

    public void Play()
    {
        //check si board esta vacio
        //si esta, click en mazo para empezar
        //si no, check turn order
    }

    public bool CheckBoardState()
    {
        return true;
    }

    public PlayerID CheckTurnOrder()
    {
        //ver si se hace en turn manager
        return PlayerID.User;
    }

    public void Update()
    {
        if (user != null)
        {
            user.Update();
        }
        if (opponent != null)
        {
            opponent.Update();
        }
    }

    public void Draw(RenderWindow window)
    {
        board.Draw(window);

        if (userName != null)
        {
            userName.Draw(window);
        }
        if (opponentName != null)
        {
            opponentName.Draw(window);
        }

        //TODO update points and convert to string
        userPoints.Draw(window);
        opponentPoints.Draw(window);

        //TODO create a single function to minimize code
        if (user.GetDeck().GetCardsInDeck() > 0)
        {
            int totaCardsinUserDeck = user.GetDeck().GetCardsInDeck();
            Card cardToDraw = user.GetDeck().GetCard(totaCardsinUserDeck - 1); //TODO fix frame skip bug. bandaid: add card back asset and draw if condition meets
            cardToDraw.Draw(window);
        }
        if (opponent.GetDeck().GetCardsInDeck() > 0)
        {
            int totaCardsinUserDeck = opponent.GetDeck().GetCardsInDeck();
            Card cardToDraw = opponent.GetDeck().GetCard(totaCardsinUserDeck - 1);
            cardToDraw.Draw(window);
        }

        //TODO create a single function to minimize code
        if (user.GetCardsLeftInHand() > 0)
        {
            List<Card> userHand = new List<Card>();
            Vector2f nextPosition = new Vector2f();
            userHand = user.GetHand();
            nextPosition = user.GetHandInitialPosition(); //set initial position
            foreach (Card card in userHand)
            {
                //set next position
                nextPosition.X += GameObjectBase.GetSpriteSize(card).X;
                card.SetCardPosition(nextPosition);
                card.Draw(window);
            }
        }
        if (opponent.GetCardsLeftInHand() > 0)
        {
            List<Card> userHand = new List<Card>();
            Vector2f nextPosition = new Vector2f();
            userHand = opponent.GetHand();
            nextPosition = opponent.GetHandInitialPosition(); //set initial position
            foreach (Card card in userHand)
            {
                //set next position
                nextPosition.X += GameObjectBase.GetSpriteSize(card).X;
                card.SetCardPosition(nextPosition);
                card.Draw(window);
            }
        }
    }
}

