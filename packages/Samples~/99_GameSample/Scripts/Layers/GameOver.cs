using Katuusagi.SceneLayer;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameOver : LayerBehaviour
{
    public InGame InGameRoot => Parent as InGame;
    public CrossReference<TextMeshProUGUI> _scoreText;

    [LayerInstanceObject]
    private void OnInstantiatedGameOver(GameObject go)
    {
        _scoreText.Instance.text = $"Score:{InGameRoot.Score}";
    }

    protected override IEnumerator OnPreTransitFrom(TransitionBehaviour transition)
    {
        var highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScore = Mathf.Max(highScore, InGameRoot.Score);
        PlayerPrefs.SetInt("HighScore", highScore);
        yield break;
    }
}
