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
    [SerializeField] private CircleCollider2D collider2d;
    [SerializeField] private BubbleExplosion bubbleExplosion;
    [SerializeField] private BlinkingEffect blinkingEffect;

    private EventsNotifier Notifier => App.Instance.EventsNotifier;

    private Rigidbody2D _rigidbody;
    private Func<Vector2> _playerPositionGetter;
    private Func<bool> _maxBubblefishPoppedGetter;
    private float _initialColliderRadius;

    public bool IsPopped { get; private set; }

    public void Initialize(Func<Vector2> playerPositionGetter, Func<bool> maxBubblefishPoppedGetter)
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerPositionGetter = playerPositionGetter;
        _maxBubblefishPoppedGetter = maxBubblefishPoppedGetter;
        rotator.Initialize(() => _playerPositionGetter() - (Vector2)transform.position);
        _initialColliderRadius = collider2d.radius;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsPopped)
            return;

        var player = other.GetComponent<Player>();
        if (player != null && player.IsPuffed)
            Pop();
    }

    public void Pop()
    {
        if (_maxBubblefishPoppedGetter())
            return;

        IsPopped = true;
        bubbleExplosion.Explode();
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
        if (!IsPopped)
            return;

        var t = transform;
        var position = t.position;
        var diff = _playerPositionGetter() - (Vector2)position;
        _rigidbody.MovePosition(position + Time.fixedDeltaTime * App.Instance.GameSettings.FollowSpeed * (Vector3)diff);
    }

    public void SetPuffed(bool value)
    {
        collider2d.radius = value
            ? _initialColliderRadius * App.Instance.GameSettings.SwarmRadiusPuffedFactor
            : _initialColliderRadius;
    }

    public void Kill()
    {
        App.Instance.EventsNotifier.NotifyBubblefishDied(this);
        Destroy(gameObject);
    }

    public void ToggleBlinking(bool state)
    {
        blinkingEffect.ToggleBlinking(state);
    }
}
