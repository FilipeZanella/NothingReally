using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

public class GameView : ViewPanel
{
    [SerializeField] private GridController controller;
    [SerializeField] private float aspect;

    public void Initialize(Level level, Card[] data)
    {
        IGridResponsivenessHandler handler = new ResponsiveGrid(aspect);
        controller.Initialize(level, data, handler);
    }
}
