using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("Hit");
        GameEvents.MainMenuGameNew();
    }
    public void LoadGame ()
    {
        GameEvents.MainMenuGameLoad();
    }
}
