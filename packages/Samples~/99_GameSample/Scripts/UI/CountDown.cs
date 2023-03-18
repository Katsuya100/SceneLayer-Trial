using TMPro;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    [SerializeField]
    private Animation _anim = null;

    [SerializeField]
    private TextMeshProUGUI _text = null;

    [SerializeField]
    private float _timer = 3;

    private float _startTime = 0;

    public bool IsPlaying => _anim.isPlaying;

    public void Play()
    {
        gameObject.SetActive(true);
        _anim.Play();
        _text.text = _timer.ToString();
        _startTime = Time.time;
    }

    public void Update()
    {
        int remain = Mathf.CeilToInt(_timer - (Time.time - _startTime));
        if (remain <= 0)
        {
            _text.text = "Go!";
        }
        else
        {
            _text.text = remain.ToString();
        }

        if (_anim.isPlaying && remain < 0)
        {
            gameObject.SetActive(false);
            _anim.Stop();
        }
    }
}
