using Katuusagi.SceneLayer;
using System.Collections;
using UnityEngine;

public class Root : LayerBehaviour
{
    [SerializeField]
    private CrossReference<CanvasGroup> _fadeGroup = null;

    protected override IEnumerator OnPreChildTransit(TransitionBehaviour transition, int generationDiff)
    {
        var fadeGroup = _fadeGroup?.Instance;
        if (fadeGroup == null ||
            generationDiff > 1)
        {
            yield break;
        }

        fadeGroup.gameObject.SetActive(true);

        {
            // ここにfadeGroup.alphaを使ってフェードアウト処理を書いてみよう
        }

        fadeGroup.alpha = 1;
    }

    protected override IEnumerator OnPostChildTransit(TransitionBehaviour transition, int generationDiff)
    {
        var fadeGroup = _fadeGroup?.Instance;
        if (fadeGroup == null ||
            generationDiff > 1)
        {
            yield break;
        }

        {
            // ここにfadeGroup.alphaを使ってフェードイン処理を書いてみよう
        }

        fadeGroup.alpha = 0;

        fadeGroup.gameObject.SetActive(false);
    }
}
