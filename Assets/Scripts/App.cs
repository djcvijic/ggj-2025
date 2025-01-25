using UnityEngine;

public class App : MonoSingleton<App>
{
    public GameSettings GameSettings;
    public EventsNotifier EventsNotifier;
    public GameWin GameWin;

    [SerializeField] private Player playerPrefab;
    [field: SerializeField] public BubblefishManager BubblefishManager { get; private set; }
    [field: SerializeField] public EnemyManager EnemyManager { get; private set; }
    [SerializeField] private AppCanvas appCanvas;

    private Player player;

    protected override void Awake()
    {
        base.Awake();

        EventsNotifier = new EventsNotifier();
        GameWin = new GameWin();

        appCanvas.Initialize();
        
        player = Instantiate(playerPrefab, GameSettings.PlayerSpawnY * Vector3.up, Quaternion.identity);
        
        CameraFollow.InstantiateCameraFollowObj().Initialize(player.transform, player.GetComponent<Rigidbody2D>());
        BubblefishManager.Initialize(player);
        EnemyManager.Initialize(() => player.transform.position);
        
    }
}
