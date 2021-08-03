using System;
using System.Collections.Generic;
using System.Text;


public class Deck
{
    private List<Card> deck;
    private int currentCardsInDeck;
    private int totalCardsQuantity; //number of different cards in the deck
    private int cardFieldsQuantity; //number of attributes of each card in the deck file

    public Deck()
    {
    }

    public Deck(string deckPath)
    {
        deck = new List<Card>();
        totalCardsQuantity = 7;
        cardFieldsQuantity = 3;
        string[] deckInfo = DeckReader.ReadDeckInfoFromFile(deckPath);
        for (int i = 0; i < (totalCardsQuantity * cardFieldsQuantity); i+= cardFieldsQuantity)
        {
            Card c = new Card();
            c.LoadCard(c, deckInfo, i);
            for (int j = 0; j < c.GetCardQuantity(); j++)
            {
                deck.Add(c);
            }
        }
        currentCardsInDeck = deck.Count;
    }

    public Deck Shuffle(Deck mainDeck)
    {
        //shuffle main deck and divide the cards into 2 decks, 1 for each player
        Random rand = new Random();
        for (int i = mainDeck.deck.Count - 1; i > 0; i--)
        {
            int n = rand.Next(i + 1);
            Card temp = mainDeck.deck[i];
            mainDeck.deck[i] = mainDeck.deck[n];
            mainDeck.deck[n] = temp;
        }

        return mainDeck;
    }

    public Deck SplitDeck(Deck mainDeck, int n)
    {
        Deck playerDeck = new Deck();
        playerDeck.deck = new List<Card>();

        for (int i = 0; i < mainDeck.deck.Count; i++)
        {
            if (i % 2 == n)
            {
                playerDeck.deck.Add(mainDeck.deck[i]);
            }
        }
        currentCardsInDeck = playerDeck.deck.Count;

        return playerDeck;
    }

    public Card GetCard(int index)
    {
        return deck[index];
    }

    public List<Card> GetDeck()
    {
        return deck;
    }
}
