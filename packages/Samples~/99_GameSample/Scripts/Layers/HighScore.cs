using Katuusagi.SceneLayer;
using TMPro;
using UnityEngine;

public class HighScore : LayerBehaviour
{
    [SerializeField]
    private CrossReference<TextMeshProUGUI> _score;

    [LayerInstanceObject]
    private void OnInstantiateHighScore(GameObject go)
    {
        _score.Instance.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
}
