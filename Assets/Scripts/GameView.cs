using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [SerializeField] private AnimationCurve endOfGameAnimationCurve;

    public void Initialize(Level level, GameStatusJson data)
    {
        IGridResponsivenessHandler handler = new ResponsiveGrid(aspect);
        controller.Initialize(level, data.cards, handler);
        UpdateScore(data.pairs);
        UpdateAttempt(data.attempts);
    }

    public void FinishGame() 
    {
        saveButton.interactable = false;
        StartCoroutine(EndOfGameAniamtion());
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

    private IEnumerator EndOfGameAniamtion ()
    {
        yield return new WaitForSeconds(1.23f);

        StartCoroutine(LoopUtility.Tween((t) => scoreLabel.transform.localScale = Vector3.one * endOfGameAnimationCurve.Evaluate(t), 0.23f));
        StartCoroutine(LoopUtility.Tween((t) => attemptLabel.transform.localScale = Vector3.one * endOfGameAnimationCurve.Evaluate(t), 0.23f));

        foreach (Transform child in controller.transform) 
        {
            StartCoroutine(LoopUtility.Tween((t) => child.localScale = Vector3.one * endOfGameAnimationCurve.Evaluate(t), 0.23f));

            yield return new WaitForSeconds(0.08f);
        }

        GameEvents.EndOfGame?.Invoke();
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
