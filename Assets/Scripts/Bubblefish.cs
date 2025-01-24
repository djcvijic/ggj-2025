using System;
using UnityEngine;

public class Bubblefish : MonoBehaviour
{
    [SerializeField] private SpriteRenderer bubble;
    [SerializeField] private SpriteRenderer fish;
    [SerializeField] private Sprite happySprite;
    [SerializeField] private Sprite sadSprite;

    public event Action Popped;

    private bool _popped;

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
        bubble.gameObject.SetActive(false);
        fish.sprite = happySprite;
        Popped?.Invoke();
    }

    private void OnDestroy()
    {
        Popped = null;
    }
}
