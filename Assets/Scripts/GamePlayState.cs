using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class GamePlayState : State
{
    private GamePresenter presenter;
    private GameModel model;
    private GameView gameView;
    private Level level;
    private GameStatusJson status;

    public GamePlayState(StateContext _context, Level _level, GameStatusJson _status) : base(_context)
    {
        level = _level;
        status = _status;
    }

    public override void OnEnterState()
    {
        gameView = SceneObjectContainer.singleton.GetViewPanel<GameView>();

        IGameSaver saver = new PlayerPrefsSaver();

        gameView.Initialize(level, status);
        model = new GameModel(status);

        presenter = new GamePresenter(gameView, model, saver);

        GameEvents.EndOfGame += EndOfGame;
    }

    private void EndOfGame() 
    {
        var status = model.GetStatus();
        var newState = new EndOfGameState(context, status);
        ChangeState(newState);
    }

    public override void OnExitState()
    {
        GameEvents.EndOfGame += EndOfGame;
    }
}