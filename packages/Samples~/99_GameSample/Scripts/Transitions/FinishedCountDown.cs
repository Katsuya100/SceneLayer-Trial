using Katuusagi.SceneLayer;
using UnityEngine;

public class FinishedCountDown  : TransitionBehaviour
{
    [SerializeField]
    private CrossReference<CountDown> _countDown;

    protected override bool IsTransitable()
    {
        return !_countDown.Instance.IsPlaying;
    }
}
