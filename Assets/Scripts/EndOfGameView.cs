using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfGameView: ViewPanel
{
    [SerializeField] private GameObject holder;
    [SerializeField] private TMP_Text score;
    [SerializeField] private TMP_Text maxScore;

    public void Initialize() 
    {
        holder.SetActive(true);
    }

    public void UpdateScore (int _score) 
    {
        score.text = "Current Score: " + _score;
    }

    public void UpdateHighestScore(int _score)
    {
        maxScore.text = "Highest Score: " + _score;
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(0);
    }
}
