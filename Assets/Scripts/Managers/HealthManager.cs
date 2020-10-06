using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private ProgressBar healthBar;
    [SerializeField] private FloatValue playerCurrentHealth;
    [SerializeField] private Text playerCurrentHealthText;
    // Start is called before the first frame update
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
