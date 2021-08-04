using System;
using System.Collections.Generic;
using System.Text;


public sealed class Opponent : Player
{
    public Opponent(string opponentName, Deck opponentDeck, PlayerID playerID) : base(opponentName, opponentDeck, playerID)
    {

    }
}

