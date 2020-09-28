using UnityEngine;
using UnityEngine.UI;

public class ExperienceManager : MonoBehaviour
{
    [SerializeField] private ProgressBar expBar;
    [SerializeField] private FloatValue maxExp;
    [SerializeField] private FloatValue playerCurrentExp;
    [SerializeField] private Text playerCurrentExpText;

    // Start is called before the first frame update
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
