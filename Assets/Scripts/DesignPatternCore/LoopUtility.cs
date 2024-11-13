using System;
using System.Collections;
using UnityEngine;

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
