using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public SpriteRenderer sprite;
    public PlayerMovement playerMovement;

     private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        playerMovement = new PlayerMovement(this);
    }


    private void Update()
    {
        playerMovement.Update();
    }
}
