using System;
using System.Collections.Generic;
using System.Text;


public sealed class User : Player
{
    private float clickRate;

    public User(string userName, Deck userDeck, PlayerID playerID) : base(userName, userDeck, playerID)
    {
        clickRate = 1.0f;
    }

    public void ResetClickRate()
    {
        clickRate = 0;
    }

    public void ClickRateCooldown()
    {
        clickRate += Framerate.GetDeltaTime();
    }

    public float GetClickRate()
    {
        return clickRate;
    }
}
