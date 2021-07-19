using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoSlider : MonoBehaviour
{
    public bool autoStart = false;
    public float waitTimePerLoop = 0.1f;
    public float fillValuePerLoop = 0.03f;
    public Slider slider;

    private void Start()
    {
        slider.value = 0.0f;
        if (autoStart)
        {
            StartAutoSlider();
        }
    }

    public void StartAutoSlider()
    {
        StartCoroutine("RunSlider", 0.1f);
    }

    public IEnumerator RunSlider()
    {
        while (slider.value < 1.0f)
        {
            yield return new WaitForSecondsRealtime(waitTimePerLoop);
            slider.value += fillValuePerLoop;
        }
    }
}
