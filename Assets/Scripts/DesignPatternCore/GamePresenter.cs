using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePresenter 
{
    private GameView gameView;
    private GameModel gameModel;

    Card selectedCard;

    public GamePresenter(GameView view, GameModel model) 
    {
        gameModel = model;
        gameView = view;

        
    }
}

