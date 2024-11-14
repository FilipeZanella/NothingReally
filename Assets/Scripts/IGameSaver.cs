public interface IGameSaver 
{
    bool ContainsSave();
    void Save(GameStatusJson status);
    GameStatusJson GetStatus();
}
