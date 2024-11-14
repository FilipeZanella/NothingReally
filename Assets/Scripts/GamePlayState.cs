using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayState : State
{
    private GamePresenter presenter;
    private GameModel model;
    private GameView gameView;  

    private Level level;

    public GamePlayState(StateContext _context, Level _level) : base(_context)
    {
        level = _level;
    }

    public override void OnEnterState()
    {
        gameView = SceneObjectContainer.singleton.GetViewPanel<GameView>();

        IGameSaver saver = new PlayerPrefsSaver();
        var status = GetCards(saver);

        gameView.Initialize(level, status);
        model = new GameModel(status);
        
        presenter = new GamePresenter(gameView, model, saver);
    }

    private GameStatusJson GetCards(IGameSaver saver)
    {
        bool isloadMode = PlayerPrefs.GetInt("IsLoadMode", 0) == 1;
        if (isloadMode)
        {
            return saver.GetStatus();
        }
        else
        {
            var data = new GameStatusJson();
            ICardShuffler shuffler = new CardShuffler_FisherYates();
            data.cards = level.GetShuffledCards(shuffler);
            return data;
        }
    }

    public override void OnExitState()
    {

    }
}