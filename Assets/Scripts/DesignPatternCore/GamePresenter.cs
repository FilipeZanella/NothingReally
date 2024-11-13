using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.Experimental.AI;

public class GamePresenter : IDisposable
{
    private GameView gameView;
    private GameModel gameModel;

    List<CardView> selectedCards;

    public GamePresenter(GameView view, GameModel model)
    {
        gameModel = model;
        gameView = view;

        selectedCards = new List<CardView>();

        GameEvents.OnSelectCard += HandleCardSelection;
    }

    public void Dispose()
    {
        GameEvents.OnSelectCard -= HandleCardSelection;
    }

    private void HandleCardSelection(CardView cardView)
    {
        if (selectedCards.Count < 2)
        {
            selectedCards.Add(cardView);
        }
        else
        {
            if (selectedCards[0].card.imageIndex == selectedCards[1].card.imageIndex)
            {
                selectedCards[0].card.paired = true;
                selectedCards[1].card.paired = true;

                GameEvents.OnPaired?.Invoke(selectedCards[0], selectedCards[1]);
            }
            else
            {
                selectedCards[0].Hide();
                selectedCards[1].Hide();
            }

            selectedCards.Clear();

            selectedCards.Add(cardView);
        }
    }
}

public static class GameEvents
{
    public static Action<CardView> OnSelectCard;
    public static Action<CardView, CardView> OnPaired;
}