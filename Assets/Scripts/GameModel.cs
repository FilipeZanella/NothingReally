using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel
{
    public Card[] cards;
    public int score { get; private set; }
    public int attempt { get; private set; }
    public int pairs { get; private set; }

    public GameModel(GameStatusJson data)
    {
        score = data.score;
        attempt = data.attempts;
        cards = data.cards;
        pairs = data.pairs;
    }

    public void Score()
    {
        pairs++;
        score += 5;
    }

    public void Attempt()
    {
        attempt++;

        score -= 2;
        if (score < 0) 
        {
            score = 0;
        }
    }

    public GameStatusJson GetStatus()
    {
        return new GameStatusJson()
        {
            attempts = attempt,
            cards = cards,
            pairs = pairs,
            score = score
        };
    }
}
