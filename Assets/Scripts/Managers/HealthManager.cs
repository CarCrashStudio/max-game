using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private ProgressBar healthBar;
    [SerializeField] private FloatValue playerCurrentHealth;
    [SerializeField] private Text playerCurrentHealthText;
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
        playerCurrentHealth.runtimeValue = playerCurrentHealth.initialValue;
    }
    public void UpdateHealth ()
    {
        healthBar.current = playerCurrentHealth.runtimeValue;
        playerCurrentHealthText.text = $"{playerCurrentHealth.runtimeValue} / {playerCurrentHealth.initialValue}";
    }
}
