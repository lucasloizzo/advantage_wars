using System.Collections.Generic;
using SFML.System;
using System;

public abstract class Player
{
    protected string name;
    protected Deck deck;
    protected List<Card> hand;
    public Vector2f handInitialPosition;
    protected int handSize;

    public Player(string playerName, Deck playerDeck, PlayerID playerID)
    {
        handSize = 10;
        hand = new List<Card>();
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
                handInitialPosition = new Vector2f((windowWidth) / 2, windowHeight);
                break;
            case PlayerID.Opponnent:
                deckPosition.X = windowWidth - (GameObjectBase.GetSpriteSize(deck.GetCard(0)).X * 2) ;
                deckPosition.Y = 0;
                deck.SetDeckPosition(deckPosition);
                handInitialPosition = new Vector2f((windowWidth) / 2, 0);
                break;
            default:
                break;
        }
        foreach (Card card in deck.GetDeck())
        {
            card.SetCardPosition(deckPosition);
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

    public Deck GetDeck()
    {
        return deck;
    }
}
