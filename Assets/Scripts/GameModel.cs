using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel 
{
    public Card[] cards;
    public int score {  get; private set; }
    public int attempt {  get; private set; }

    public GameModel(GameStatusJson data)
    {
        score = data.pairs;
        attempt = data.attempts;
        cards = data.cards;
    }

    public void Score()
    {
        score++;
    }

    public void Attempt() 
    {
        attempt++;
    }

    public GameStatusJson GetStatus() 
    {
        return new GameStatusJson()
        {
            attempts = attempt,
            cards = cards,
            pairs = score
        };
    }
}
