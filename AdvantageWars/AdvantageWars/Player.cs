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
    protected Vector2f boardInitialPosition;
    protected int points;
    protected Vector2f deckPosition;

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
        deckPosition = new Vector2f();
        switch (playerID)
        {
            case PlayerID.User:
                deckPosition.X = 0 + GameObjectBase.GetSpriteSize(deck.GetCard(0)).X;
                deckPosition.Y = windowHeight - GameObjectBase.GetSpriteSize(deck.GetCard(0)).Y;
                deck.SetDeckPosition(deckPosition);
                handInitialPosition = new Vector2f(windowWidth / 10, windowHeight - GameObjectBase.GetSpriteSize(deck.GetCard(0)).Y);
                boardInitialPosition = new Vector2f(windowWidth / 2, windowHeight / 2);
                break;
            case PlayerID.Opponnent:
                deckPosition.X = windowWidth - (GameObjectBase.GetSpriteSize(deck.GetCard(0)).X * 2) ;
                deckPosition.Y = 0;
                deck.SetDeckPosition(deckPosition);
                handInitialPosition = new Vector2f(windowWidth / 10, 0);
                boardInitialPosition = new Vector2f(windowWidth / 2 - (GameObjectBase.GetSpriteSize(deck.GetCard(0)).X * 2), windowHeight / 2 - GameObjectBase.GetSpriteSize(deck.GetCard(0)).Y - 25);
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
        if (hand != null)
        {
            //update hand position and order
        }
    }

    public void CheckGarbage()
    {
        if (deck != null)
        {

        }
        if (hand != null)
        {

        }
        if (cardsInPlay != null)
        {

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

    public void ClearBoard()
    {
        List<int> indexToDelete = new List<int>();
        for (int i = 0; i < cardsInPlay.Count; i++)
        {
            indexToDelete.Add(i);
        }
        for (int i = indexToDelete.Count - 1; i >=0; i--)
        {
            cardsInPlay[i].Delete();
            cardsInPlay.RemoveAt(i);
        }
    }

    public void PlayCardFromDeck(Card card, int index, Vector2f position)
    {
        card.SetCardPosition(position);
        cardsInPlay.Add(card);
        deck.GetDeck().RemoveAt(index);
        deck.SetCardsLeftInDeck();
    }
    
    public void PlayCardFromHand(Card card, int index, Vector2f position)
    {
        card.SetCardPosition(position);
        cardsInPlay.Add(card);
        hand.RemoveAt(index);
    }

    public List<Card> GetCardsInPlay()
    {
        return cardsInPlay;
    }

    public Card GetNextCardInDeck(Player player)
    {
        //return the next card on deck to draw
        return player.GetDeck().GetCard(player.GetDeck().GetCardsInDeck() - 1);
    }

    public int GetPoints()
    {
        this.points = 0;
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

    public Vector2f GetDeckPosition()
    {
        return deckPosition;
    }

    public List<Card> GetHand()
    {
        return hand;
    }

    public string GetPlayerName()
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
    
    public Vector2f GetBoardInitialPosition()
    {
        return boardInitialPosition;
    }

    public Card GetCardInHand(int index)
    {
        return hand[index];
    }
}
