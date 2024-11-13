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
        
        ICardShuffler shuffler = new CardShuffler_FisherYates();
        var cards = level.GetShuffledCards(shuffler);

        gameView.Initialize(level, cards);
        model = new GameModel(cards);
        presenter = new GamePresenter(gameView, model);
    }

    public override void OnExitState()
    {

    }
}