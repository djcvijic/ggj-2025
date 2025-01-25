﻿using UnityEngine;

public class App : MonoSingleton<App>
{
    public GameSettings GameSettings;
    public EventsNotifier EventsNotifier;

    [SerializeField] private Player playerPrefab;
    [field: SerializeField] public BubblefishManager BubblefishManager { get; private set; }
    [SerializeField] private AppCanvas appCanvas;

    private CameraFollow _cameraFollow;
    private Player player;

    protected override void Awake()
    {
        base.Awake();

        EventsNotifier = new EventsNotifier();
        
        appCanvas.Initialize();
        
        player = Instantiate(playerPrefab);
        _cameraFollow = CameraFollow.InstantiateCameraFollowObj().Initialize(player.transform, player.GetComponent<Rigidbody2D>());
        BubblefishManager.Initialize(player);
    }
}
