using System;

public static class GameEvents
{
    public static Action<CardView> OnSelectCard;
    public static Action<CardView> OnHideCard;
    public static Action<CardView, CardView> OnPaired;
    public static Action OnWin;
    public static Action SaveStatus;
}