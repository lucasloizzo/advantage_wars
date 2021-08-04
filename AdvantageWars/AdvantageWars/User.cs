using System;
using System.Collections.Generic;
using System.Text;


public sealed class User : Player
{
    public User(string userName, Deck userDeck, PlayerID playerID) : base(userName, userDeck, playerID)
    {

    }
}
