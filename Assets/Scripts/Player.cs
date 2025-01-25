using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public SpriteRenderer sprite;
    public PlayerMovement playerMovement;
    private SpriteRotator spriteRotator;

     private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        playerMovement = new PlayerMovement(this);
        spriteRotator = GetComponent<SpriteRotator>();
        spriteRotator.Initialize(() => _rb.velocity.normalized);
    }


    private void Update()
    {
        playerMovement.Update();
    }
}
