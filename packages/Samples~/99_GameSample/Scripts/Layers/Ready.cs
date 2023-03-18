using Katuusagi.SceneLayer;
using UnityEngine;

public class Ready : LayerBehaviour
{
    [SerializeField]
    private CrossReference<CountDown> _countDown = null;

    public InGame InGameRoot => Parent as InGame;

    protected override void OnEntry()
    {
        _countDown.Instance.Play();
    }
}
