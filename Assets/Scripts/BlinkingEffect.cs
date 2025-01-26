using UnityEngine;

public class BlinkingEffect : MonoBehaviour
{
    [SerializeField]private SpriteRenderer spriteRenderer;
    private Coroutine _blinkingCoroutine;
    private bool _isBlinking;

    private float BlinkSpeed => App.Instance.GameSettings.graceBlinkInterval; // Speed of the blinking effect in seconds

    private void Awake()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                Debug.LogError("BlinkingEffect requires a SpriteRenderer component on the same GameObject.");
            }
        }
    }

    public void ToggleBlinking(bool enable)
    {
        if (enable)
        {
            if (!_isBlinking)
            {
                _blinkingCoroutine = StartCoroutine(Blink());
                _isBlinking = true;
            }
        }
        else
        {
            if (_isBlinking)
            {
                StopCoroutine(_blinkingCoroutine);
                _isBlinking = false;
                SetSpriteAlpha(1f); // Ensure alpha is fully visible when blinking stops
            }
        }
    }

    private System.Collections.IEnumerator Blink()
    {
        while (true)
        {
            SetSpriteAlpha(0f); 
            yield return new WaitForSeconds(BlinkSpeed);
            SetSpriteAlpha(1f); 
            yield return new WaitForSeconds(BlinkSpeed);
        }
    }

    private void SetSpriteAlpha(float alpha)
    {
        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;
        }
    }
}