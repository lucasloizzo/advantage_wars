using System.Collections.Generic;
using SFML.System;
using System;

public abstract class Player
{
    protected string name;
    protected Deck deck;
    protected List<Card> hand;
    protected Vector2f handInitialPosition;
    protected int handSize;
    protected List<Card> cardsInPlay;
    protected int points;

    public Player(string playerName, Deck playerDeck, PlayerID playerID)
    {
        handSize = 10;
        points = 0;
        hand = new List<Card>();
        cardsInPlay = new List<Card>();
        this.name = playerName;
        this.deck = playerDeck;
        this.hand = InitialHand(playerDeck);
        this.hand = SortHand(hand);
        float windowWidth = Game.GetWindowSize().X;
        float windowHeight = Game.GetWindowSize().Y;
        Vector2f deckPosition = new Vector2f();
        switch (playerID)
        {
            case PlayerID.User:
                deckPosition.X = 0 + GameObjectBase.GetSpriteSize(deck.GetCard(0)).X;
                deckPosition.Y = windowHeight - GameObjectBase.GetSpriteSize(deck.GetCard(0)).Y;
                deck.SetDeckPosition(deckPosition);
                handInitialPosition = new Vector2f((windowWidth) / 10, windowHeight - GameObjectBase.GetSpriteSize(deck.GetCard(0)).Y);
                break;
            case PlayerID.Opponnent:
                deckPosition.X = windowWidth - (GameObjectBase.GetSpriteSize(deck.GetCard(0)).X * 2) ;
                deckPosition.Y = 0;
                deck.SetDeckPosition(deckPosition);
                handInitialPosition = new Vector2f((windowWidth) / 10, 0);
                break;
            default:
                break;
        }
        foreach (Card card in deck.GetDeck())
        {
            card.SetCardPosition(deckPosition);
        }
    }

    public void Update()
    {
        if (deck != null)
        {
            deck.Update();
        }
    }

    public List<Card> InitialHand(Deck playerDeck)
    {
        for (int i = handSize; i > 0; i--)
        {
            hand.Add(deck.GetCard(i));
            deck.GetDeck().RemoveAt(i);
        }
        deck.SetCardsLeftInDeck();
        return hand;
    }

    public List<Card> SortHand(List<Card> hand)
    {
        Card temp = new Card();
        for (int i = 0; i < hand.Count; i++)
        {
            for (int j = 0; j < hand.Count; j++)
            {
                if (hand[j].GetCardValue() > hand[i].GetCardValue())
                {
                    temp = hand[i];
                    hand[i] = hand[j];
                    hand[j] = temp;
                }
            }
        }
        return hand;
    }

    public int GetPoints()
    {
        foreach (Card card in cardsInPlay)
        {
            this.points += card.GetCardValue();
        }
        return points;
    }

    public string GetStringPoints()
    {
        return Convert.ToString(GetPoints());
    }

    public Deck GetDeck()
    {
        return deck;
    }

    public List<Card> GetHand()
    {
        return hand;
    }

    public String GetPlayerName()
    {
        return name;
    }

    public int GetCardsLeftInHand()
    {
        return hand.Count;
    }

    public Vector2f GetHandInitialPosition()
    {
        return handInitialPosition;
    }
}
