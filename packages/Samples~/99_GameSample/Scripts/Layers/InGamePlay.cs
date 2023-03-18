using Katuusagi.SceneLayer;
using TMPro;
using UnityEngine;

public class InGamePlay : LayerBehaviour
{
    [SerializeField]
    private CrossReference<TextMeshProUGUI> _scoreText;

    public InGame InGameRoot => Parent as InGame;

    [LayerInstanceObject]
    private void OnInstantiatedScoreText(GameObject go)
    {
        _scoreText.Instance.text = $"Score:{InGameRoot.Score}";
    }

    protected override void OnEntry()
    {
        InGameRoot.Resume();
    }

    protected override void OnUpdate()
    {
        _scoreText.Instance.text = $"Score:{InGameRoot.Score}";

    }
    protected override void OnExit()
    {
        InGameRoot.Pause();
    }
}
