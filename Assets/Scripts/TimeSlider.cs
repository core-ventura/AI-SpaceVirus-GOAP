using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSlider : MonoBehaviour
{
    Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
    }
    public void UpdateTimeScale()
    {
        Time.timeScale = 1 + slider.value * 5;
    }
}
