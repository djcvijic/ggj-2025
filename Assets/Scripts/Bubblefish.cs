using System;
using UnityEngine;

public class Bubblefish : MonoBehaviour
{
    public event Action Popped;

    [SerializeField] private SpriteRenderer bubble;
    [SerializeField] private SpriteRenderer fish;
    [SerializeField] private Sprite happySprite;

    private bool _popped;

    private void OnTriggerEnter(Collider other)
    {
        if (_popped)
            return;

        var player = other.GetComponent<Player>();
        if (player != null)
        {
            bubble.gameObject.SetActive(false);
            fish.sprite = happySprite;
            Popped?.Invoke();
        }
    }
}
