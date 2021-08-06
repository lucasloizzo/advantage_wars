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

    public int UpdateState(Opponent player, int enemyHandQuantity)
    {
        int cardToPlayIndex = 0;
        switch (currentState)
        {
            case STATES.DEFENSIVE:
                //play the most efficient way (less cost card) if you have card in the deck
                cardToPlayIndex = Defensive(hand, deck.GetDeck().Count);
                break;
            case STATES.OFFENSIVE:
                //if you have 1 card in the deck & the opponent has less cards in hand than you, play the most expensive one
                cardToPlayIndex = Offensive(hand, deck.GetDeck().Count, enemyHandQuantity);
                break;
            default:
                break;
        }
        return cardToPlayIndex;
    }

    private STATES CheckState()
    {
        return STATES.DEFENSIVE;
    }

    private int Defensive(List<Card> hand, int cardsLeftInDeck)
    {
        return 0;
    }

    private int Offensive(List<Card> hand, int cardsLeftInDeck, int enemyHandQuantity)
    {
        return 0;
    }
}

