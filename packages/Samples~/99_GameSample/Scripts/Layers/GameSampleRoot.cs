using Katuusagi.SceneLayer;
using System.Collections;
using System.Linq;
using UnityEngine;

public class GameSampleRoot : LayerBehaviour
{
    [SerializeField]
    private CrossReference<CanvasGroup> _fadeGroup = null;

    [SerializeField]
    private float _fadeTime = 0.5f;

    [SerializeField]
    private LayerBehaviour[] _blackouts;

    protected override IEnumerator OnPreChildTransit(TransitionBehaviour transition, int generationDiff)
    {
        if (_blackouts.Contains(transition.From))
        {
            yield break;
        }

        var fadeGroup = _fadeGroup.Instance;
        if (fadeGroup == null ||
            generationDiff > 1)
        {
            yield break;
        }

        fadeGroup.gameObject.SetActive(true);
        float startTime = Time.time;
        do
        {
            fadeGroup.alpha = (Time.time - startTime) / _fadeTime;
            yield return null;
        }
        while (Time.time - startTime < _fadeTime);
        fadeGroup.alpha = 1;
    }

    protected override IEnumerator OnPostChildTransit(TransitionBehaviour transition, int generationDiff)
    {
        if (_blackouts.Contains(transition.To))
        {
            yield break;
        }

        var fadeGroup = _fadeGroup.Instance;
        if (fadeGroup == null ||
            generationDiff > 1)
        {
            yield break;
        }

        float startTime = Time.time;
        do
        {
            fadeGroup.alpha = 1.0f - (Time.time - startTime) / _fadeTime;
            yield return null;
        }
        while (Time.time - startTime < _fadeTime);
        fadeGroup.gameObject.SetActive(false);
    }
}
