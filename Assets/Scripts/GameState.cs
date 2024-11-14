public class GameState : State
{
    private Level level;
    private StateContext gameContext;

    public GameState(StateContext _context, Level _level) : base(_context)
    {
        level = _level;
    }

    public override void OnEnterState()
    {
        gameContext = new StateContext();
        IGameSaver saver = new PlayerPrefsSaver();
        var initialState = new InitialGameModeState(gameContext, level, saver);
        gameContext.SetState(initialState);
    }

    public override void OnExitState()
    {

    }
}
