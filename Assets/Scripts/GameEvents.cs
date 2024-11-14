using System;

public static class GameEvents
{
    public static Action<CardView> OnSelectCard;
    public static Action<CardView> OnHideCard;
    public static Action<CardView, CardView> OnPaired;
    public static Action OnWin;
    public static Action EndOfGame;
    public static Action SaveStatus;
    public static Action NewGame;
    public static Action LoadGame;
}