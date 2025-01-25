using UnityEngine;

public class AppCanvas : MonoBehaviour
{
    public PlayerScoreHolder playerScoreHolder;

    public void Initialize()
    {
        playerScoreHolder.Initialize();
    }
}