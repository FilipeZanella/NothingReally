using System;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    public AudioEffect onSelectCard;
    public AudioEffect onHideCard;
    public AudioEffect OnWin;
    public AudioEffect OnPair;
    public AudioEffect OnSave;

    private void Start()
    {
        GameEvents.OnSelectCard += _onSelectCard;
        GameEvents.OnWin += _onWin;
        GameEvents.OnPaired += _onPair;
        GameEvents.SaveStatus += _onSave;
        GameEvents.OnHideCard += _onHideCard;
    }

    private void OnDestroy()
    {
        GameEvents.OnSelectCard -= _onSelectCard;
        GameEvents.OnWin -= _onWin;
        GameEvents.OnPaired -= _onPair;
        GameEvents.SaveStatus -= _onSave;
        GameEvents.OnHideCard -= _onHideCard;
    }

    void _onSelectCard(CardView c) => onSelectCard.ApplyEffect();
    void _onHideCard(CardView c) => onHideCard.ApplyEffect();
    void _onPair(CardView c, CardView t) => OnPair.ApplyEffect();
    void _onWin() => OnWin.ApplyEffect();
    void _onSave() => OnSave.ApplyEffect();
}
