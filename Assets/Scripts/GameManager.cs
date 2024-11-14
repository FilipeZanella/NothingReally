using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private StateContext context;

    [SerializeField] private Level level;

    private void Awake()
    {
        Coroutines.starter = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        context = new StateContext();
        var initialState = new GameState(context, level);
        context.SetState(initialState);
    }
}
