using SFML.Graphics;
using SFML.System;
using System;
using System.IO;
using System.Collections.Generic;
using SFML.Window;

public class Gameplay
{
    private Board board;
    private Deck mainDeck;
    private User user;
    private Title userName;
    private Title userPoints;
    private Card userDeckBack;
    private Opponent opponent;
    private Title opponentName;
    private Title opponentPoints;
    private Card opponentDeckBack;

    public Gameplay()
    {
        board = new Board(".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "resources" + Path.DirectorySeparatorChar + "assets" + Path.DirectorySeparatorChar + "board.png", new Vector2f());
        mainDeck = new Deck(".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "resources" + Path.DirectorySeparatorChar + "data" + Path.DirectorySeparatorChar + "mainDeck.deck");
        mainDeck.Shuffle(mainDeck);

        user = new User("Rean", mainDeck.SplitDeck(mainDeck, 0), PlayerID.User);
        userName = new Title(user.GetPlayerName(), new Vector2f(100.0f, 590.0f), Color.Green);
        userPoints = new Title("0", new Vector2f(850.0f, 580.0f), Color.Yellow);
        userPoints.SetSize(72);
        userPoints.SetStyle(1);
        userDeckBack = new Card(".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "resources" + Path.DirectorySeparatorChar + "assets" + Path.DirectorySeparatorChar + "cardback.png", new Vector2f(user.GetDeckPosition().X, user.GetDeckPosition().Y));

        opponent = new Opponent("Gilbert", mainDeck.SplitDeck(mainDeck, 1), PlayerID.Opponnent);
        opponentName = new Title(opponent.GetPlayerName(), new Vector2f(1700.0f, 410.0f), Color.Green);
        opponentPoints = new Title("0", new Vector2f(1050.0f, 380.0f), Color.Yellow);
        opponentPoints.SetSize(72);
        opponentPoints.SetStyle(1);
        opponentDeckBack = new Card(".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "resources" + Path.DirectorySeparatorChar + "assets" + Path.DirectorySeparatorChar + "cardback.png", new Vector2f(opponent.GetDeckPosition().X, opponent.GetDeckPosition().Y));
        //TODO delete mainDeck
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

        //check si board esta vacio
        if (user.GetPoints() == opponent.GetPoints())
        {
            if (userDeckBack.CardClicked() == true && user.GetClickRate() >= 1) //check si clickeo deck
            {
                user.ResetClickRate();

                if (opponent.GetDeck().GetCardsInDeck() <= 0)
                {
                    Console.WriteLine("You Win!");
                }
                else if (user.GetDeck().GetCardsInDeck() <= 0)
                {
                    Console.WriteLine("You Lost!");
                }

                //move card from top of deck to play
                user.PlayCardFromDeck(user.GetNextCardInDeck(user), user.GetDeck().GetCardsInDeck() - 1, new Vector2f(1050.0f, 580.0f));
                //do the same for opponent
                opponent.PlayCardFromDeck(opponent.GetNextCardInDeck(opponent), opponent.GetDeck().GetCardsInDeck() - 1, new Vector2f(850.0f, 380.0f));
                //update scores
                Console.WriteLine("User = " + user.GetPoints());
                Console.WriteLine("Opp = " + opponent.GetPoints());
            }
        }
        else if (user.GetPoints() > opponent.GetPoints())
        {
            //empieza oponente
            Console.WriteLine("Opponent turn");
        }
        else
        {
            //empieza user
            Console.WriteLine("User turn");
        }
        user.ClickRateCooldown();
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
            //user.GetNextCardInDeck(user).Draw(window);
            userDeckBack.Draw(window);
        }
        if (opponent.GetDeck().GetCardsInDeck() > 0)
        {
            //opponent.GetNextCardInDeck(opponent).Draw(window);
            opponentDeckBack.Draw(window);
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

