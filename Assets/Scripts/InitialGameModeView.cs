public class InitialGameModeView : ViewPanel 
{
    public void Initialize() 
    {
        gameObject.SetActive(true);
    }

    public void NewGame() 
    {
        GameEvents.NewGame?.Invoke();
    }

    public void LoadGame()
    {
        GameEvents.LoadGame?.Invoke();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
