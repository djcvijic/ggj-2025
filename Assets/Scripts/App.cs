using UnityEngine;

public class App : MonoBehaviour
{
    public static App Instance;
    
    public GameSettings GameSettings;
    public EventsNotifier EventsNotifier;
    public GameWin GameWin;

    [SerializeField] private Player playerPrefab;
    [field: SerializeField] public BubblefishManager BubblefishManager { get; private set; }
    [field: SerializeField] public EnemyManager EnemyManager { get; private set; }
    [field: SerializeField] public DialogSystem DialogSystem { get; private set; }
    [SerializeField] private AppCanvas appCanvas;

    public Player Player { get; private set; }

    public bool ShouldPauseAllMovement => DialogSystem.IsMessageShown;

    protected void Awake()
    {
        Instance = this;

        EventsNotifier = new EventsNotifier();
        GameWin = new GameWin();

        Player = Instantiate(playerPrefab, GameSettings.PlayerSpawnY * Vector3.up, Quaternion.identity);

        appCanvas.Initialize();
        CameraFollow.InstantiateCameraFollowObj().Initialize(Player.transform, Player.GetComponent<Rigidbody2D>());
        BubblefishManager.Initialize(Player);
        EnemyManager.Initialize(() => Player.transform.position);
        DialogSystem.Initialize();
    }
}
