public class InitialGameModeState : State
{
    private InitialGameModeView view;
    private IGameSaver saver;
    private Level level;

    public InitialGameModeState(StateContext _context, Level _level, IGameSaver _saver) : base(_context)
    {
        saver = _saver;
        level = _level;
        view = SceneObjectContainer.singleton.GetViewPanel<InitialGameModeView>();
    }

    public override void OnEnterState()
    {
        if (saver.ContainsSave()) 
        {
            view.Initialize();

            GameEvents.LoadGame += LoadGame;
            GameEvents.NewGame += NewGame;
        }
        else 
        {
            NewGame();
        }
    }

    private void LoadGame() 
    {
        var data = saver.GetStatus();
        var gameplay = new GamePlayState(context, level, data);
        ChangeState(gameplay);
    }

    private void NewGame() 
    {
        var data = new GameStatusJson();
        ICardShuffler shuffler = new CardShuffler_FisherYates();
        data.cards = level.GetShuffledCards(shuffler);
        var gameplay = new GamePlayState(context, level, data);
        ChangeState(gameplay);
    }

    public override void OnExitState()
    {
        GameEvents.LoadGame -= LoadGame;
        GameEvents.NewGame -= NewGame;
        view.Close();
    }
}
