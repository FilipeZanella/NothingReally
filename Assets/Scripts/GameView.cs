using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

public class GameView : ViewPanel
{
    [SerializeField] private GridController controller;
    [SerializeField] private float aspect;

    [SerializeField] private TMP_Text scoreLabel;
    [SerializeField] private TMP_Text attemptLabel;

    [SerializeField] private Button saveButton;
    [SerializeField] private Image saveLoading;

    public void Initialize(Level level, GameStatusJson data)
    {
        IGridResponsivenessHandler handler = new ResponsiveGrid(aspect);
        controller.Initialize(level, data.cards, handler);
        UpdateScore(data.pairs);
        UpdateAttempt(data.attempts);

        GameEvents.OnWin += OnWin;
    }

    private void OnDestroy()
    {
        GameEvents.OnWin -= OnWin;  
    }

    private void OnWin() 
    {
        saveButton.interactable = false;
    }

    public void Save()
    {
        GameEvents.SaveStatus?.Invoke();
        StartCoroutine(AnimateLoading());
    }

    public void UpdateScore(int score)
    {
        scoreLabel.text = "Score: " + score.ToString();
    }

    public void UpdateAttempt(int attempt)
    {
        attemptLabel.text = "Attempt: " + attempt.ToString();
    }

    private IEnumerator AnimateLoading()
    {
        float size = 1.14f;
        saveLoading.transform.localScale = Vector3.one * size;

        yield return LoopUtility.Tween((t) => saveLoading.fillAmount = t, 0.23f);
        yield return new WaitForSeconds(0.14f);
        yield return LoopUtility.Tween((t) => saveLoading.transform.localScale = Vector3.one * Mathf.Lerp(size, 1,t), 0.14f);
    }
}
