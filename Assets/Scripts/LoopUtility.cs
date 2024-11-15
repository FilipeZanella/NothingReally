﻿using System;
using System.Collections;
using UnityEngine;

public static class Coroutines 
{
    public static MonoBehaviour starter;

    public static Coroutine Start(IEnumerator c)
    {
        return starter.StartCoroutine(c);
    }
    public static void Stop(Coroutine c)
    {
        starter.StopCoroutine(c);
    }
}

public static class LoopUtility
{
    public static IEnumerator Tween(Action<float> action, float duration)
    {
        float time = 0;

        while (time < duration)
        {
            action(time / duration);
            time += Time.deltaTime;

            yield return null;
        }

        action(1);
    }
}
