using UnityEngine;

public class App : MonoSingleton<App>
{
    public GameSettings GameSettings;
    
    public Player playerPrefab;
    [HideInInspector]
    public Player player;
    
    protected override void Awake()
    {
        base.Awake();
        
        player = Instantiate(playerPrefab);
    }
}
