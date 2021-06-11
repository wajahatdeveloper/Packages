﻿using UnityEngine;
using System.Collections;

namespace MalbersAnimations
{

    /// <summary> Going slow motion on user input</summary>
    public class SlowMotion : MonoBehaviour
    {
        [Space]
        public InputRow slowMotion =  new InputRow("Fire2", KeyCode.Mouse2, InputButton.Down);
        public InputRow pauseEditor =  new InputRow("Pause", KeyCode.P, InputButton.Down);
        [Space]
        [Range(0.05f, 1)]
        [SerializeField] float slowMoTimeScale = 0.25f;
        [Range (0.1f,2)]
        [SerializeField] float slowMoSpeed =.2f; 

        void Update()
        {
            if (slowMotion.GetInput)
            {
                if (Time.timeScale == 1.0F)
                {
                    StartCoroutine(SlowTime());
                }
                else
                {
                    StartCoroutine(RestartTime());
                }
                Time.fixedDeltaTime = 0.02F * Time.timeScale;
            }

            if (pauseEditor.GetInput)
            {
                Debug.Break();
            }
        }

        IEnumerator SlowTime()
        {
            while (Time.timeScale > slowMoTimeScale)
            {
                Time.timeScale = Mathf.Clamp(Time.timeScale - (1 /slowMoSpeed * Time.unscaledDeltaTime),0,100);
                Time.fixedDeltaTime = 0.02F * Time.timeScale;
                yield return null;
            }
            Time.timeScale = slowMoTimeScale;
        }

        IEnumerator RestartTime()
        {
            while (Time.timeScale <1)
            {
                Time.timeScale += 1 / slowMoSpeed * Time.unscaledDeltaTime;
                yield return null;
            }
            Time.timeScale = 1;
        }
    }
}
