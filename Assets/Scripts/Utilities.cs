using System;
using System.Collections;
using UnityEngine;
namespace MGUtilities
{
    public class Coroutines
    {
        public static IEnumerator LerpVector3OverTime(Vector3 start, Vector3 end, float duration, Action<Vector3> onUpdate)
        {
            float timePassed = 0f;

            while (timePassed < duration)
            {
                timePassed += Time.deltaTime;
                float lerpFactor = Mathf.Clamp01(timePassed / duration);
                Vector3 currentValue = Vector3.Lerp(start, end, lerpFactor);

                onUpdate?.Invoke(currentValue);

                yield return null;
            }
            onUpdate?.Invoke(end);
        }
        public static IEnumerator LerpVector3OverTime(bool state, float duration, Action<Vector3> onUpdate)
        {
            float timePassed = 0f;
            Vector3 start = state ? Vector3.zero : Vector3.one;
            Vector3 end = state ? Vector3.one : Vector3.zero;
            while (timePassed < duration)
            {
                timePassed += Time.deltaTime;
                float lerpFactor = Mathf.Clamp01(timePassed / duration);
                Vector3 currentValue = Vector3.Lerp(start, end, lerpFactor);

                onUpdate?.Invoke(currentValue);

                yield return null;
            }
            onUpdate?.Invoke(end);
        }
        public static IEnumerator PingPongVector3OverTime(Vector3 start, Vector3 end, float duration, Action<Vector3> onUpdate)
        {
            float timePassed = 0f;
            float d = 0.5f * duration;
            while (timePassed < d)
            {
                timePassed += Time.deltaTime;
                float lerpFactor = Mathf.Clamp01(timePassed / duration);
                Vector3 currentValue = Vector3.Lerp(start, end, lerpFactor);

                onUpdate?.Invoke(currentValue);

                yield return null;
            }
            timePassed = 0f;
            while (timePassed < d)
            {
                timePassed += Time.deltaTime;
                float lerpFactor = Mathf.Clamp01(timePassed / duration);
                Vector3 currentValue = Vector3.Lerp(end, start, lerpFactor);

                onUpdate?.Invoke(currentValue);

                yield return null;
            }
            onUpdate?.Invoke(start);
        }
        public static IEnumerator DelayBoolChange(bool startState, bool endState, float waitTime, Action<bool> onUpdate)
        {
            onUpdate?.Invoke(startState);
            yield return new WaitForSeconds(waitTime);
            onUpdate?.Invoke(endState);
        }
        public static IEnumerator PingPongVector3OverTime(Vector3 start, Vector3 end, float duration, Action<bool> boolToUpdate, Action<Vector3> Vector3ToUpdate)
        {
            boolToUpdate?.Invoke(false);
            float timePassed = 0f;
            float d = 0.5f * duration;
            while (timePassed < d)
            {
                timePassed += Time.deltaTime;
                float lerpFactor = Mathf.Clamp01(timePassed / d);
                Vector3 currentValue = Vector3.Lerp(start, end, lerpFactor);

                Vector3ToUpdate?.Invoke(currentValue);

                yield return null;
            }
            timePassed = 0f;
            while (timePassed < d)
            {
                timePassed += Time.deltaTime;
                float lerpFactor = Mathf.Clamp01(timePassed / d);
                Vector3 currentValue = Vector3.Lerp(end, start, lerpFactor);

                Vector3ToUpdate?.Invoke(currentValue);

                yield return null;
            }
            Vector3ToUpdate?.Invoke(start);
            boolToUpdate?.Invoke(true);
        }
    }
}