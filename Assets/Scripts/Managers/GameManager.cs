using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] static bool unsavedChangesExist = false;

    [SerializeField] static DungeonManager dungeon;
    [SerializeField] static PlayerSaveData playerData;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject quitConfirmation;

    [SerializeField] ProgressBar loadingBarSlider;
    [SerializeField] Text loadingPercentageText;
    [SerializeField] GameObject loadingScreen;

    public static Database<Item> Items;
    public static Database<Quest> Quests;
    public static Database<Rarity> Rarities;

    void Start() { 
    }

    // Use this for initialization
    void Awake()
    {
        GameEvents.onChangesMade += onChangesMade;
        GameEvents.onMainMenuGameLoad += onMainMenuGameLoad;
        GameEvents.onMainMenuGameNew += onMainMenuGameNew;
    }

    private void onChangesMade ()
    {
        unsavedChangesExist = true;
    }
    private void onMainMenuGameLoad ()
    {
        LoadGame();
    }
    private void onMainMenuGameNew()
    {
        NewGame();
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
            StartCoroutine(UnloadScene(1));
            StartCoroutine(LoadScene(0, LoadSceneMode.Single));
            StartCoroutine(LoadScene(2, LoadSceneMode.Additive));
        }
    }

    public void ConfirmQuit ()
    {
        Time.timeScale = 1;
        StartCoroutine(UnloadScene(1));
        StartCoroutine(LoadScene(0, LoadSceneMode.Single));
        StartCoroutine(LoadScene(2, LoadSceneMode.Additive));
    }
    public void CancelQuit()
    {
        pauseMenu.SetActive(true);
        quitConfirmation.SetActive(false);
    }


    private void GameStart()
    {
        loadingScreen.SetActive(true);
        LoadDatabases();
        StartCoroutine(UnloadScene(0));
        StartCoroutine(LoadScene(1));
        StartCoroutine(LoadScene(2, LoadSceneMode.Additive));
        loadingScreen.SetActive(false);
    }

    public void NewGame()
    {
        GameStart();
    }
    public void SaveGame ()
    {
        unsavedChangesExist = false;
        playerData.Save();
        dungeon.Save();
    }
    public void LoadGame ()
    {
        GameStart();

        unsavedChangesExist = false;
        playerData.Load();
        dungeon.Load();
    }

    public void LoadItems ()
    {
        Items = new Database<Item>();
        Items.Load(Application.persistentDataPath + "/databases/items.json");
    }
    public void LoadQuests()
    {
        Quests = new Database<Quest>();
        Quests.Load(Application.persistentDataPath + "/databases/quests.json");
    }
    public void LoadRarities()
    {
        Rarities = new Database<Rarity>();
        Rarities.Load(Application.persistentDataPath + "/databases/rarities.json");
    }

    public void LoadDatabases ()
    {
        Debug.Log(Application.persistentDataPath);

        loadingBarSlider.minimum = 0;
        loadingBarSlider.maximum = 3;

        loadingBarSlider.current = 0;
        loadingPercentageText.text = $"Loading Databases: {Mathf.RoundToInt(0 * 100)}%";

        LoadRarities();
        loadingBarSlider.current = 1;
        loadingPercentageText.text = $"Loading Databases: {Mathf.RoundToInt((1/3) * 100)}%";

        //LoadQuests();
        loadingBarSlider.current = 2;
        loadingPercentageText.text = $"Loading Databases: {Mathf.RoundToInt((2/3) * 100)}%";

        LoadItems();
        loadingBarSlider.current = 3;
        loadingPercentageText.text = $"Loading Databases: {Mathf.RoundToInt((3/3) * 100)}%";
    }
    public IEnumerator LoadScene(int id, LoadSceneMode sceneMode = LoadSceneMode.Single)
    {
        loadingBarSlider.minimum = 0;
        loadingBarSlider.maximum = 1;

        AsyncOperation currentLoadingData = SceneManager.LoadSceneAsync(id, sceneMode);

        loadingScreen.SetActive(true);

        while (!currentLoadingData.isDone)
        {
            float progress = Mathf.Clamp(currentLoadingData.progress / 0.9f, 0, 1);
            loadingBarSlider.current = progress;
            loadingPercentageText.text = $"Loading: {Mathf.RoundToInt(progress * 100)}%";

            yield return null;
        }

        loadingScreen.SetActive(false);

        yield return null;
    }
    public IEnumerator UnloadScene(int id)
    {
        AsyncOperation currentLoadingData = SceneManager.UnloadSceneAsync(id);

        while (!currentLoadingData.isDone)
        {
            yield return null;
        }


        yield return null;
    }
}