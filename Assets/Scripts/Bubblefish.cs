using System;
using UnityEngine;

public class Bubblefish : MonoBehaviour
{
    [SerializeField] private SpriteRenderer bubble;
    [SerializeField] private SpriteRenderer fish;
    [SerializeField] private Sprite happySprite;
    [SerializeField] private Sprite sadSprite;
    [SerializeField] private SpriteRotator rotator;

    public event Action Popped;

    private Func<Vector2> _playerPositionGetter;
    private bool _popped;

    public void Initialize(Func<Vector2> playerPositionGetter)
    {
        _playerPositionGetter = playerPositionGetter;
        rotator.Initialize(() => _playerPositionGetter() - (Vector2)transform.position);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_popped)
            return;

        var player = other.GetComponent<Player>();
        if (player != null)
            Pop();
    }

    private void Pop()
    {
        _popped = true;
        bubble.gameObject.SetActive(false);
        fish.sprite = happySprite;
        Popped?.Invoke();
    }

    private void Update()
    {
        Follow();
    }

    private void Follow()
    {
        if (!_popped)
            return;

        var t = transform;
        var position = t.position;
        var diff = _playerPositionGetter() - (Vector2)position;
        t.position = position + Time.deltaTime * App.Instance.GameSettings.FollowSpeed * (Vector3)diff;
    }

    private void OnDestroy()
    {
        Popped = null;
    }
}
