using UnityEngine;

public class EndOfGameState : State
{
    private GameStatusJson status;

    public EndOfGameState(StateContext context, GameStatusJson _status) : base(context)
    {
        status = _status;
    }

    public override void OnEnterState()
    {
        var view = SceneObjectContainer.singleton.GetViewPanel<EndOfGameView>();
        view.Initialize();

        int higuestScore = PlayerPrefs.GetInt("HighestScore");
        if (higuestScore < status.score) 
        {
            higuestScore = status.score;
            PlayerPrefs.SetInt("HighestScore", higuestScore);
        }

        view.UpdateScore(status.score);
        view.UpdateHighestScore(higuestScore);
    }

    public override void OnExitState()
    {

    }
}
