using UnityEngine;

public class BlinkingEffect : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Coroutine _blinkingCoroutine;
    private bool _isBlinking;

    private float BlinkSpeed => App.Instance.GameSettings.graceBlinkInterval; // Speed of the blinking effect in seconds

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer == null)
        {
            Debug.LogError("BlinkingEffect requires a SpriteRenderer component on the same GameObject.");
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
        if (_spriteRenderer != null)
        {
            Color color = _spriteRenderer.color;
            color.a = alpha;
            _spriteRenderer.color = color;
        }
    }
}