using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bubblefish : MonoBehaviour
{
    [SerializeField] private SpriteRenderer bubble;
    [SerializeField] private SpriteRenderer fish;
    [SerializeField] private Sprite happySprite;
    [SerializeField] private Sprite sadSprite;
    [SerializeField] private SpriteRotator rotator;

    public EventsNotifier Notifier => App.Instance.EventsNotifier;

    private Rigidbody2D _rigidbody;
    private Func<Vector2> _playerPositionGetter;
    private bool _popped;

    public void Initialize(Func<Vector2> playerPositionGetter)
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerPositionGetter = playerPositionGetter;
        rotator.Initialize(() => _playerPositionGetter() - (Vector2)transform.position);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_popped)
            return;

        var player = other.GetComponent<Player>();
        if (player != null && player.IsPuffed)
            Pop();
    }

    private void Pop()
    {
        _popped = true;
        bubble.gameObject.SetActive(false);
        fish.sprite = happySprite;
        Notifier.NotifyBubblefishPopped(this);
    }

    private void FixedUpdate()
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
        _rigidbody.MovePosition(position + Time.fixedDeltaTime * App.Instance.GameSettings.FollowSpeed * (Vector3)diff);
    }
}
