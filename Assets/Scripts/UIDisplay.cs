using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    [SerializeField] Slider buildHealthSlider;
    [SerializeField] Slider tankHealthSlider;
    [SerializeField] Slider strikerHealthSlider;
    [SerializeField] Health buildHealth;
    [SerializeField] Health tankHealth;
    [SerializeField] Health strikerHealth;
    [SerializeField] TextMeshProUGUI buildName;
    [SerializeField] TextMeshProUGUI tankName;
    [SerializeField] TextMeshProUGUI strikerName;
    void Awake()
    {
        buildHealthSlider.maxValue = buildHealth.GetBuildHealth();
        tankHealthSlider.maxValue = tankHealth.GetTankHealth();
        strikerHealthSlider.maxValue = strikerHealth.GetStrikerHealth();
    }
    void Start()
    {
        tankHealthSlider.value = tankHealth.GetTankHealth();
        strikerHealthSlider.value = strikerHealth.GetStrikerHealth();
        tankName.text = tankHealth.name;
        strikerName.text = strikerHealth.name;
    }

    // Update is called once per frame
    void Update()
    {
        if (buildHealth != null)
        {
            buildHealthSlider.value = buildHealth.GetBuildHealth();
            buildName.text = buildHealth.name;
        }

    }
}
