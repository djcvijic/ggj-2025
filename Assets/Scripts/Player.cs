using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite puffedSprite;

    private PlayerMovement playerMovement;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb;

    private float _puffSecondsRemaining;

    private bool IsDashing => playerMovement is { IsDashing: true };

    public bool IsPuffed => IsDashing || _puffSecondsRemaining > 0;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        playerMovement = new PlayerMovement(this);
    }


    private void Update()
    {
        playerMovement.Update();
        UpdatePuffedness();
    }

    private void UpdatePuffedness()
    {
        if (IsDashing)
            _puffSecondsRemaining = App.Instance.GameSettings.SecondsPuffedAfterDashEnds;
        else if (IsPuffed)
            _puffSecondsRemaining -= Time.deltaTime;

        _spriteRenderer.sprite = IsPuffed ? puffedSprite : defaultSprite;
    }
}