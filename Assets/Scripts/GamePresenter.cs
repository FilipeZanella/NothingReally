using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class GamePresenter 
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
        GameEvents.OnWin += OnWin; 
    }

    private void OnWin() 
    {
        gameView.FinishGame();

        GameEvents.OnPaired -= HandlePairing;
        GameEvents.OnSelectCard -= HandleCardSelection;
        GameEvents.SaveStatus -= Save;
        GameEvents.OnWin -= OnWin;
    }

    public void Save()
    {
        var status = gameModel.GetStatus();
        gameSaver.Save(status);
    }

    private void Score()
    {
        gameModel.Score();
        gameView.UpdateScore(gameModel.score);
    }

    private void Attempt()
    {
        gameModel.Attempt();
        gameView.UpdateScore(gameModel.score);
        gameView.UpdateAttempt(gameModel.attempt);
    }

    private void HandlePairing(CardView card1, CardView card2)
    {
        card1.card.paired = true;
        card2.card.paired = true;

        Score();

        int max = gameModel.cards.Length / 2;
        if (gameModel.pairs == max)
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
            else 
            {
                Attempt();
            }
        }
    }
}
