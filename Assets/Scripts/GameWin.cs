using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWin
{
    public void TriggerWinGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}