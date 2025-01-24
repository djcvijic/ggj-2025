using UnityEngine;

public class App : MonoSingleton<App>
{
    public GameSettings GameSettings;
    
    public Player playerPrefab;

    [SerializeField] private BubblefishSpawner bubblefishSpawner;

    [HideInInspector]
    public Player player;
    
    protected override void Awake()
    {
        base.Awake();
        
        player = Instantiate(playerPrefab);
    }
}
