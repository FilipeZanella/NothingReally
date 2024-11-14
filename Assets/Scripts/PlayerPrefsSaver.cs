using UnityEngine;

public class PlayerPrefsSaver : IGameSaver
{
    public const string KEY = "LastGameStatus";

    public bool ContainsSave()
    {
        return PlayerPrefs.HasKey(KEY);
    }

    public GameStatusJson GetStatus()
    {
        var json = PlayerPrefs.GetString(KEY);
        var data = JsonUtility.FromJson<GameStatusJson>(json);

        return data;
    }

    public void Save(GameStatusJson status)
    {
        var json = JsonUtility.ToJson(status);

        PlayerPrefs.SetString(KEY, json);
    }
} 