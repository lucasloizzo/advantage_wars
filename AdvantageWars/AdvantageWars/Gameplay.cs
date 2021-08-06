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

    public void CheckGarbage()
    {
        if (user != null)
        {
            user.CheckGarbage();
        }
        if (opponent != null)
        {
            opponent.CheckGarbage();
        }
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

        //Empy board or stalemate check
        if (user.GetPoints() == opponent.GetPoints())
        {
            if (userDeckBack.CardClicked() == true && user.GetClickRate() >= 1) //check si clickeo deck
            {
                //if (user.GetCardsInPlay().Count > 0)
                //{
                //    user.ClearBoard();
                //}
                //if (opponent.GetCardsInPlay().Count > 0)
                //{
                //    opponent.ClearBoard();
                //}
                user.ResetClickRate();

                if (opponent.GetDeck().GetCardsInDeck() <= 0)
                {
                    Console.WriteLine("You Win!");
                    Game.SetPause();
                }
                else if (user.GetDeck().GetCardsInDeck() <= 0)
                {
                    Console.WriteLine("You Lost!");
                    Game.SetPause();
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
        //Opponent turn check
        else if (user.GetPoints() > opponent.GetPoints())
        {
            //empieza oponente
            //si no puede jugar, pierde
            if (opponent.GetCardsLeftInHand() > 0)
            {
                int cardToPlayIndex = opponent.UpdateState(opponent, user.GetCardsLeftInHand());
                opponent.PlayCardFromHand(opponent.GetCardInHand(cardToPlayIndex), cardToPlayIndex, opponent.GetBoardInitialPosition());
                Console.WriteLine("User = " + user.GetPoints());
                Console.WriteLine("Opp = " + opponent.GetPoints());
            }
            else
            {
                Console.WriteLine("You Win!");
                Game.SetPause();
            }
            //mover carta a board
            //sacarla de la mano
            //check si perdio (points < user)
            //pausar juego
        }
        //User turn check
        else if(user.GetPoints() < opponent.GetPoints())
        {
            //empieza user
            //si no puede jugar, pierde
            if (user.GetCardsLeftInHand() > 0) { 
                for (int i = 0; i < user.GetHand().Count; i++)
                {
                    if (user.GetCardInHand(i).CardClicked() == true && user.GetClickRate() >= 1)
                    {
                        //mover carta a board
                        user.ResetClickRate();
                        user.PlayCardFromHand(user.GetCardInHand(i), i, user.GetBoardInitialPosition()); //new Vector2f(1050.0f, 580.0f)
                        //sacarla de la mano
                        //check si perdi (points < enemigo)
                        Console.WriteLine("User = " + user.GetPoints());
                        Console.WriteLine("Opp = " + opponent.GetPoints());
                        CheckWinner(user.GetPoints(), opponent.GetPoints());
                        //pausar juego
                    }
                }
            }
            else
            {
                Console.WriteLine("You Lost");
                Game.SetPause();
            }
        }
        user.ClickRateCooldown();
    }

    public void CheckWinner(int lastPlayed, int target)
    {
        if (lastPlayed < target)
        {
            Console.WriteLine("You Lost!");
            Game.SetPause();
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
        userPoints.SetNewText(user.GetStringPoints());
        opponentPoints.SetNewText(opponent.GetStringPoints());

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

        if (user.GetCardsInPlay().Count > 0 && user.GetCardsInPlay() != null)
        {
            List<Card> userBoard = new List<Card>();
            //Vector2f nextPosition = new Vector2f();
            userBoard = user.GetCardsInPlay();
            //nextPosition = user.GetBoardInitialPosition(); //set initial position
            foreach (Card card in userBoard)
            {
                //set next position
                //nextPosition.X += GameObjectBase.GetSpriteSize(card).X;
                card.SetCardPosition(user.GetBoardInitialPosition());
                card.Draw(window);
            }
        }
        if (opponent.GetCardsInPlay().Count > 0 && opponent.GetCardsInPlay() != null)
        {
            List<Card> opponentBoard = new List<Card>();
            //Vector2f nextPosition = new Vector2f();
            opponentBoard = opponent.GetCardsInPlay();
            //nextPosition = opponent.GetBoardInitialPosition(); //set initial position
            foreach (Card card in opponentBoard)
            {
                //set next position
                card.SetCardPosition(opponent.GetBoardInitialPosition());
                card.Draw(window);
                //nextPosition.X -= GameObjectBase.GetSpriteSize(card).X;
            }
        }
    }
}

