using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public SpriteRenderer sprite;
    public PlayerMovement playerMovement;

     private Rigidbody2D _rb;

     private float _puffSecondsRemaining;

     private bool IsDashing => playerMovement is { IsDashing: true };

     public bool IsPuffed => IsDashing || _puffSecondsRemaining > 0;

    private void Awake()
    {
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
    }
}
