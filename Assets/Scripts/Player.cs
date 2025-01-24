using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public SpriteRenderer sprite;
    public PlayerMovement playerMovement;


    private void Awake()
    {
        playerMovement = new PlayerMovement(this);
    }


    private void Update()
    {
        playerMovement.Update();
    }
}
