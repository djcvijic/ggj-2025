using UnityEngine;
using UnityEngine.Serialization;

public class AppCanvas : MonoBehaviour
{
    public PlayerStatusBars playerStatusBars;

    public void Initialize()
    {
        playerStatusBars.Initialize();
    }
}