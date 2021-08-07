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
    private Title winner;
    private Board winnerScreen;
    private float moveTime;

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

        winnerScreen = new Board(".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "resources" + Path.DirectorySeparatorChar + "assets" + Path.DirectorySeparatorChar + "winnerscreen.png", new Vector2f());
        moveTime = 2.0f;
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

        //Empty board or stalemate check
        if (user.GetPoints() == opponent.GetPoints())
        {
            if (opponent.GetDeck().GetCardsInDeck() == 0 && user.GetDeck().GetCardsInDeck() == 0)
            {
                winner = new Title("Draw!", new Vector2f(750.0f, 530.0f), Color.Cyan);
                winner.SetSize(72);
                winner.SetStyle(1);
                Game.SetPause();
            }
            if (userDeckBack.CardClicked() == true && user.GetClickRate() >= 1) //check si clickeo deck
            {
                user.ResetClickRate();

                
                if (opponent.GetDeck().GetCardsInDeck() <= 0)
                {
                    CheckWinner(user, opponent);
                }
                else if (user.GetDeck().GetCardsInDeck() <= 0)
                {
                    CheckWinner(opponent, user);
                }

                if (moveTime >= 2)
                {
                    //move card from top of deck to play
                    user.PlayCardFromDeck(user.GetNextCardInDeck(user), user.GetDeck().GetCardsInDeck() - 1, new Vector2f(1050.0f, 580.0f));
                    //do the same for opponent
                    opponent.PlayCardFromDeck(opponent.GetNextCardInDeck(opponent), opponent.GetDeck().GetCardsInDeck() - 1, new Vector2f(850.0f, 380.0f));
                    moveTime = 0;
                }
            }
        }
        //Opponent turn check
        else if (user.GetPoints() > opponent.GetPoints())
        {
            if (opponent.GetCardsLeftInHand() > 0)
            {
                if (moveTime >= 2)
                {
                    int cardToPlayIndex = opponent.UpdateState(opponent, user.GetCardsLeftInHand(), user.GetPoints());
                    opponent.PlayCardFromHand(opponent.GetCardInHand(cardToPlayIndex), cardToPlayIndex, opponent.GetBoardInitialPosition());
                    CheckWinner(opponent, user);
                    moveTime = 0;
                }
            }
            else
            {
                CheckWinner(opponent, user);
            }
        }
        //User turn check
        else if(user.GetPoints() < opponent.GetPoints())
        {
            if (user.GetCardsLeftInHand() > 0) { 
                for (int i = 0; i < user.GetHand().Count; i++)
                {
                    if (user.GetCardInHand(i).CardClicked() == true) // && user.GetClickRate() >= 1
                    {
                        user.ResetClickRate();
                        user.PlayCardFromHand(user.GetCardInHand(i), i, user.GetBoardInitialPosition());
                        moveTime = 0;
                        CheckWinner(user, opponent);
                    }
                }
            }
            else
            {
                CheckWinner(user, opponent);
            }
        }
        moveTime += Framerate.GetDeltaTime();
        user.ClickRateCooldown();
    }

    public void CheckWinner(Player casterPlayer, Player targetPlayer)
    {
        if (casterPlayer.GetPoints() < targetPlayer.GetPoints())
        {
            winner = new Title(targetPlayer.GetPlayerName() + " Won!", new Vector2f(750.0f, 530.0f), Color.Cyan);
            winner.SetSize(72);
            winner.SetStyle(1);
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

        userPoints.SetNewText(user.GetStringPoints());
        opponentPoints.SetNewText(opponent.GetStringPoints());

        userPoints.Draw(window);
        opponentPoints.Draw(window);

        //Draw deck if cards left
        if (user.GetDeck().GetCardsInDeck() > 0)
        {
            userDeckBack.Draw(window);
        }
        if (opponent.GetDeck().GetCardsInDeck() > 0)
        {
            opponentDeckBack.Draw(window);
        }

        //TODO create a single function to minimize code
        if (user.GetCardsLeftInHand() > 0)
        {
            foreach (Card card in user.GetHand())
            {
                card.Draw(window);
            }
        }
        if (opponent.GetCardsLeftInHand() > 0)
        {

            foreach (Card card in opponent.GetHand())
            {
                card.Draw(window);
            }
        }

        if (user.GetCardsInPlay().Count > 0 && user.GetCardsInPlay() != null)
        {
            List<Card> userBoard = new List<Card>();
            userBoard = user.GetCardsInPlay();
            foreach (Card card in userBoard)
            {
                card.SetCardPosition(user.GetBoardInitialPosition());
                card.Draw(window);
            }
        }
        if (opponent.GetCardsInPlay().Count > 0 && opponent.GetCardsInPlay() != null)
        {
            List<Card> opponentBoard = new List<Card>();
            opponentBoard = opponent.GetCardsInPlay();
            foreach (Card card in opponentBoard)
            {
                card.SetCardPosition(opponent.GetBoardInitialPosition());
                card.Draw(window);
            }
        }

        //Draw winner screen
        if (winner != null)
        {
            winnerScreen.Draw(window);
            winner.Draw(window);
        }
    }
}

