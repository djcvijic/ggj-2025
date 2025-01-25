using UnityEngine;

public class App : MonoSingleton<App>
{
    public GameSettings GameSettings;
    public EventsNotifier EventsNotifier;

    [SerializeField] private Player playerPrefab;
    [SerializeField] private BubblefishManager bubblefishManager;

    private CameraFollow _cameraFollow;
    private Player player;

    protected override void Awake()
    {
        base.Awake();

        EventsNotifier = new EventsNotifier();
        
        player = Instantiate(playerPrefab);
        _cameraFollow = CameraFollow.InstantiateCameraFollowObj().Initialize(player.transform, player.GetComponent<Rigidbody2D>());
        bubblefishManager.Initialize(player);
    }
}
