using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{
    #region IEnumerator
    public static IEnumerator IE_LocalTranslate(Transform obj, Vector3 start, Vector3 end, float duration, Action callbacks = null)
    {
        float t = 0;
        while (t < duration)
        {
            obj.localPosition = Vector3.Lerp(start, end, t / duration);
            t += Time.deltaTime;
            yield return null;
        }
        Debug.Log($"IE_LocalTranslate: start = {start}, end = {end}");
        obj.localPosition = end;
        callbacks?.Invoke();
    }
    public static IEnumerator IE_WorldTranslate(Transform obj, Vector3 start, Vector3 end, float duration, Action callbacks = null)
    {
        float t = 0;
        while (t < duration)
        {
            obj.position = Vector3.Lerp(start, end, t / duration);
            t += Time.deltaTime;
            yield return null;
        }
        Debug.Log($"IE_WorldTranslate: start = {start}, end = {end}");
        obj.position = end;
        callbacks?.Invoke();
    }
    public static IEnumerator IE_LocalRotate(Transform obj, Vector3 start, Vector3 end, float duration, Action callbacks = null)
    {
        float t = 0;
        while (t < duration)
        {
            obj.localEulerAngles = Vector3.Lerp(start, end, t / duration);
            t += Time.deltaTime;
            yield return null;
        }
        Debug.Log($"IE_LocalRotate: start = {start}, end = {end}");
        obj.localEulerAngles = end;
        callbacks?.Invoke();
    }
    public static IEnumerator IE_WorldRotate(Transform obj, Vector3 start, Vector3 end, float duration, Action callbacks = null)
    {
        float t = 0;
        while (t < duration)
        {
            obj.eulerAngles = Vector3.Lerp(start, end, t / duration);
            t += Time.deltaTime;
            yield return null;
        }
        Debug.Log($"IE_WorldRotate: start = {start}, end = {end}");
        obj.eulerAngles = end;
        callbacks?.Invoke();
    }
    public static IEnumerator IE_LocalScale(Transform obj, Vector3 start, Vector3 end, float duration, Action callbacks = null)
    {
        float t = 0;
        while (t < duration)
        {
            obj.localScale = Vector3.Lerp(start, end, t / duration);
            t += Time.deltaTime;
            yield return null;
        }
        Debug.Log($"IE_LocalScale: start = {start}, end = {end}");
        obj.localScale = end;
        callbacks?.Invoke();
    }
    public static IEnumerator IE_DelayForAction(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);

        action.Invoke();
    }
    #endregion
}
