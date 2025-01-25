using UnityEngine;

public class BubblefishManager : MonoBehaviour
{
    [SerializeField] private BubblefishSpawner bubblefishSpawner;

    public void Initialize(Player player)
    {
        bubblefishSpawner.Initialize(() => player.transform.position);
    }

}