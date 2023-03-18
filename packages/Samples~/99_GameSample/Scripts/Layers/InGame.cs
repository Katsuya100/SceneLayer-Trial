using Katuusagi.SceneLayer;
using System.Collections.Generic;
using UnityEngine;

public class InGame : LayerBehaviour, IPausable
{
    private const float Top = -0.36f;
    private const float Bottom = -3.23f;
    private const float SpaceSize = (Top - Bottom) / 3;

    [SerializeField]
    private CrossReference<Camera> _camera = null;

    [SerializeField]
    private CrossReference<Transform> _parentRoot;

    [SerializeField]
    private ResourcesPrefabPoolLoader _wallLoader;

    [SerializeField]
    private ResourcesPrefabPoolLoader _damageWallLoader;

    [SerializeField]
    private float _span = 10.0f;

    private bool _isPaused = false;
    private float _beforeTime = 0.0f;
    private Queue<GameObject> _walls = new Queue<GameObject>();
    private Queue<GameObject> _damageWalls = new Queue<GameObject>();

    private float _left = 0.0f;
    private float _right = 0.0f;
    private PauseHandle _pause = null;

    public int Score { get; private set; } = 0;

    bool IPausable.IsPaused => _isPaused;

    public void Pause()
    {
        _pause = PauseManager.Pause(_parentRoot.Instance.gameObject, this);
    }

    public void Resume()
    {
        _pause.Resume();
        _pause = null;
    }

    protected override void OnEntry()
    {
        Pause();
        _beforeTime = 0;
        _walls.Clear();
        _damageWalls.Clear();
        _left = _camera.Instance.ScreenToWorldPoint(Vector2.zero).x;
        _right = _camera.Instance.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).x;
        Score = 0;
    }

    protected override void OnUpdate()
    {
        if (_isPaused)
        {
            return;
        }

        bool isReturned = false;
        int count = 0;
        foreach (var wall in _walls)
        {
            if (wall.transform.localPosition.x > _left - wall.transform.localScale.x)
            {
                continue;
            }

            isReturned = true;
            _wallLoader.Return(wall);
            ++count;
        }

        for (int i = 0; i < count; ++i)
        {
            _walls.Dequeue();
        }

        count = 0;
        foreach (var wall in _damageWalls)
        {
            if (wall.transform.localPosition.x > _left - wall.transform.localScale.x)
            {
                continue;
            }

            isReturned = true;
            _damageWallLoader.Return(wall);
            ++count;
        }

        for (int i = 0; i < count; ++i)
        {
            _damageWalls.Dequeue();
        }

        if (isReturned)
        {
            ++Score;
        }

        _beforeTime -= Time.deltaTime;
        if (_beforeTime <= 0)
        {
            CreateWall();
            _beforeTime = _span;
        }
    }

    private void CreateWall()
    {
        var space = Random.Range(Bottom + SpaceSize, Top);

        bool isDamage = Random.Range(0, 100) < Score;
        GameObject topWall;
        if (isDamage)
        {
            topWall = _damageWallLoader.Get(_parentRoot.Instance);
            _damageWalls.Enqueue(topWall);
        }
        else
        {
            topWall = _wallLoader.Get(_parentRoot.Instance);
            _walls.Enqueue(topWall);
        }

        var top = topWall.GetComponent<MoveObject>();
        var topPos = top.transform.localPosition;
        topPos.x = _right + topWall.transform.localScale.x;
        topPos.y = space + top.transform.localScale.y;
        top.transform.localPosition = topPos;
        top.Resume();

        isDamage = Random.Range(0, 100) < Score;
        GameObject bottomWall;
        if (isDamage)
        {
            bottomWall = _damageWallLoader.Get(_parentRoot.Instance);
            _walls.Enqueue(bottomWall);
        }
        else
        {
            bottomWall = _wallLoader.Get(_parentRoot.Instance);
            _walls.Enqueue(bottomWall);
        }

        var bottom = bottomWall.GetComponent<MoveObject>();
        var bottomPos = bottom.transform.localPosition;
        bottomPos.x = _right + bottomWall.transform.localScale.x;
        bottomPos.y = space - SpaceSize;
        bottom.transform.localPosition = bottomPos;
        bottom.Resume();
    }

    void IPausable.Pause()
    {
        _isPaused = true;
    }

    void IPausable.Resume()
    {
        _isPaused = false;
    }
}
