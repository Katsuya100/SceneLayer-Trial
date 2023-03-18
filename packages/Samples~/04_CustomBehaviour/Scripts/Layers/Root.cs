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
            // ������fadeGroup.alpha���g���ăt�F�[�h�A�E�g�����������Ă݂悤
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
            // ������fadeGroup.alpha���g���ăt�F�[�h�C�������������Ă݂悤
        }

        fadeGroup.alpha = 0;

        fadeGroup.gameObject.SetActive(false);
    }
}
