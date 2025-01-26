using UnityEngine;

public class HitRegistrationEffect : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private Color color1 = Color.red; // First color
    [SerializeField]
    private Color color2 = Color.white; // Second color

    [SerializeField]
    private float toggleSpeed = 0.4f; // Duration of the color transition

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (_spriteRenderer == null)
        {
            Debug.LogError("HitRegistrationEffect requires a SpriteRenderer component on the same GameObject.");
        }
    }

    public void ToggleColor()
    {
        if (_spriteRenderer != null)
        {
            StartCoroutine(ToggleColorCoroutine());
        }
    }

    private System.Collections.IEnumerator ToggleColorCoroutine()
    {
        _spriteRenderer.color = color1;
        yield return new WaitForSeconds(toggleSpeed);
        _spriteRenderer.color = color2;
    }
}