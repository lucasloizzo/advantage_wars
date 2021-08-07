using System;
using System.Collections.Generic;
using System.Text;


public sealed class Opponent : Player
{
    private STATES currentState;

    public Opponent(string opponentName, Deck opponentDeck, PlayerID playerID) : base(opponentName, opponentDeck, playerID)
    {
        currentState = STATES.DEFENSIVE;
    }

    public int UpdateState(Opponent player, int enemyHandQuantity, int enemyPoints)
    {
        currentState = CheckState(enemyHandQuantity);
        int cardToPlayIndex = 0;
        switch (currentState)
        {
            case STATES.DEFENSIVE:
                //play the most efficient way (less cost card) if you have card in the deck
                cardToPlayIndex = Defensive(hand, deck.GetDeck().Count, enemyPoints);
                break;
            case STATES.OFFENSIVE:
                //if you have 1 card in the deck & the opponent has less cards in hand than you, play the most expensive one
                cardToPlayIndex = Offensive(hand, enemyHandQuantity, enemyPoints);
                break;
            default:
                break;
        }
        return cardToPlayIndex;
    }

    private STATES CheckState(int enemyHandQuantity)
    {
        if (deck.GetCardsInDeck() >= 1 && hand.Count > enemyHandQuantity)
        {
            return STATES.OFFENSIVE;
        }

        return STATES.DEFENSIVE;
    }

    private int Defensive(List<Card> hand, int cardsLeftInDeck, int enemyPoints)
    {
        int cardToPlayIndex = 0;

        for (int i = 0; i < hand.Count; i++)
        {
            cardToPlayIndex = i;
            if (deck.GetCardsInDeck() > 0)
            {
                if (this.points + hand[cardToPlayIndex].GetCardValue() >= enemyPoints)
                {
                    return cardToPlayIndex;
                }
            }
            else if (this.points + hand[cardToPlayIndex].GetCardValue() > enemyPoints)
            {
                return cardToPlayIndex;
            }
        }

        return cardToPlayIndex;
    }

    private int Offensive(List<Card> hand, int enemyHandQuantity, int enemyPoints)
    {
        int cardToPlayIndex = hand.Count - 1;

        return cardToPlayIndex;
    }
}

