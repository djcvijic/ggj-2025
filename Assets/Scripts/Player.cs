using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite puffedSprite;

    private PlayerMovement playerMovement;
    private PlayerGrace playerGrace;
    private BlinkingEffect blinkingEffect;
    
    private SpriteRenderer _spriteRenderer;
    
    private float _puffSecondsRemaining;
    private float _gracedSecondsRemaining;
    private CircleCollider2D _circleCollider2D;

    private bool IsDashing => playerMovement is { IsDashing: true };
    public bool IsPuffed => IsDashing || _puffSecondsRemaining > 0;
    public bool IsGraced => playerGrace.IsGraced;

    private void Awake()
    {
        _circleCollider2D = GetComponent<CircleCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = new PlayerMovement(this);
        playerGrace = gameObject.AddComponent<PlayerGrace>();
        blinkingEffect = gameObject.AddComponent<BlinkingEffect>();
    }

    private void Start()
    {
        App.Instance.EventsNotifier.GraceChanged += OnGraceChange;
    }


    private void Update()
    {
        if (App.Instance.ShouldPauseAllMovement)
            return;

        playerMovement.Update();
        UpdatePuffedness();
    }

    private void UpdatePuffedness()
    {
        if (IsDashing)
            _puffSecondsRemaining = App.Instance.GameSettings.SecondsPuffedAfterDashEnds;
        else if (IsPuffed)
            _puffSecondsRemaining -= Time.deltaTime;

        if (IsPuffed && _spriteRenderer.sprite != puffedSprite)
        {
            _spriteRenderer.sprite = puffedSprite;
            
            App.Instance.EventsNotifier.NotifyPuffednessChanged(true);
        }
        else if (!IsPuffed && _spriteRenderer.sprite != defaultSprite)
        {
            _spriteRenderer.sprite = defaultSprite;
            App.Instance.EventsNotifier.NotifyPuffednessChanged(false);
            ResetColliders();
        }
    }

    private void OnGraceChange(bool isGraced)
    {
        blinkingEffect.ToggleBlinking(isGraced);

        if (isGraced == false)
        {
            ResetColliders();
        }
    }

    private void ResetColliders()
    {
        _circleCollider2D.enabled = false;
        _circleCollider2D.enabled = true;
    }

    public void TriggerGracePeriod() => playerGrace.TriggerGracePeriod();
}