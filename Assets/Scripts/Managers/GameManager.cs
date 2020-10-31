using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] bool unsavedChangesExist = false;

    [SerializeField] DungeonManager dungeon;
    [SerializeField] PlayerSaveData playerData;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject quitConfirmation;

    public static Database<Item> Items;
    public static Database<Quest> Quests;
    public static Database<Rarity> Rarities;

    // Use this for initialization
    void Awake()
    {
        GameEvents.onChangesMade += onChangesMade;
        GameEvents.onMainMenuGameLoad += onMainMenuGameLoad;
    }

    private void onChangesMade ()
    {
        unsavedChangesExist = true;
    }
    private void onMainMenuGameLoad ()
    {
        LoadGame();
    }

    public void TogglePause ()
    {
        if (pauseMenu.activeSelf == true)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Pause ()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        GameEvents.Pause();
    }
    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        GameEvents.Resume();
    }
    public void Quit ()
    {
        if (unsavedChangesExist)
        {
            pauseMenu.SetActive(false);
            quitConfirmation.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
    }

    public void ConfirmQuit ()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void CancelQuit()
    {
        pauseMenu.SetActive(true);
        quitConfirmation.SetActive(false);
    }

    public void SaveGame ()
    {
        unsavedChangesExist = false;
        playerData.Save();
        dungeon.Save();
    }
    public void LoadGame ()
    {
        // check if unsaved change exist before loading

        unsavedChangesExist = false;
        playerData.Load();
        dungeon.Load();
    }

    public static void LoadItems ()
    {
        Items = new Database<Item>();
    }
    public static void LoadQuests()
    {
        Quests = new Database<Quest>();
    }
    public static void LoadRarities()
    {
        Rarities = new Database<Rarity>();
    }
}