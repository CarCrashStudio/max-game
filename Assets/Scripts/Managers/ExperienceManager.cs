using UnityEngine;
using UnityEngine.UI;

public class ExperienceManager : MonoBehaviour
{
    [SerializeField] private ProgressBar expBar;
    [SerializeField] private FloatValue maxExp;
    [SerializeField] private FloatValue playerCurrentExp;
    [SerializeField] private Text playerCurrentExpText;

    void Awake()
    {
        GameEvents.onPause += GameEvents_onPause;
        GameEvents.onResume += GameEvents_onResume;
    }

    private void GameEvents_onResume()
    {
        gameObject.SetActive(true);
    }

    private void GameEvents_onPause()
    {
        gameObject.SetActive(false);
    }

    private void Start()
    {
        playerCurrentExp.runtimeValue = 0;
        maxExp.runtimeValue = maxExp.initialValue;
        expBar.maximum = maxExp.runtimeValue;
        expBar.minimum = 0;
    }
    public void UpdateExperience ()
    {
        expBar.current = playerCurrentExp.runtimeValue;
        expBar.maximum = maxExp.runtimeValue;

        playerCurrentExpText.text = $"{playerCurrentExp.runtimeValue} / {maxExp.runtimeValue}";
    }
}
