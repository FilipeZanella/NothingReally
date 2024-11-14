using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class GamePresenter : IDisposable
{
    private GameView gameView;
    private GameModel gameModel;
    private IGameSaver gameSaver;

    List<CardView> selectedCards;

    public GamePresenter(GameView view, GameModel model, IGameSaver saver)
    {
        gameModel = model;
        gameView = view;
        gameSaver = saver;

        selectedCards = new List<CardView>();

        GameEvents.OnPaired += HandlePairing;
        GameEvents.OnSelectCard += HandleCardSelection;
        GameEvents.SaveStatus += Save;
    }

    public void Save()
    {
        var status = gameModel.GetStatus();
        gameSaver.Save(status);
    }

    public void Dispose()
    {
        GameEvents.OnPaired -= HandlePairing;
        GameEvents.OnSelectCard -= HandleCardSelection;
        GameEvents.SaveStatus -= Save;
    }

    private void Score()
    {
        gameModel.Score();
        gameView.UpdateScore(gameModel.score);
    }

    private void Attempt()
    {
        gameModel.Attempt();
        gameView.UpdateAttempt(gameModel.attempt);
    }

    private void HandlePairing(CardView card1, CardView card2)
    {
        card1.card.paired = true;
        card2.card.paired = true;

        Score();

        int max = gameModel.cards.Length / 2;
        if (gameModel.score == max)
        {
            GameEvents.OnWin?.Invoke();
        }
    }

    private void HandleCardSelection(CardView cardView)
    {
        if (selectedCards.Count == 2)
        {
            if (selectedCards[0].card.imageIndex != selectedCards[1].card.imageIndex)
            {
                selectedCards[0].Hide();
                selectedCards[1].Hide();
            }

            Attempt();

            selectedCards.Clear();
        }

        if (selectedCards.Count < 2)
        {
            selectedCards.Add(cardView);
        }

        if (selectedCards.Count == 2)
        {
            if (selectedCards[0].card.imageIndex == selectedCards[1].card.imageIndex)
            {
                GameEvents.OnPaired?.Invoke(selectedCards[0], selectedCards[1]);
            }
        }
    }
}
