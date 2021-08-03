using System;
using System.Collections.Generic;
using System.Text;


public abstract class Player
{
    protected string name;
    protected Deck deck;
    protected List<Card> hand;
    protected int handSize;

    public Player(string playerName, Deck playerDeck)
    {
        handSize = 10;
        hand = new List<Card>();
        this.name = playerName;
        this.deck = playerDeck;
        this.hand = InitialHand(playerDeck);
    }

    public List<Card> InitialHand(Deck playerDeck)
    {
        for (int i = handSize; i > 0; i--)
        {
            hand.Add(deck.GetCard(i));
            deck.GetDeck().RemoveAt(i);
        }
        return hand;
    }
}
