using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GameManager : MonoBehaviour {

    public bool recording = true;

    private bool isPaused = false, resumeNow = false;
    private float fixedDeltaTime, timeScale;

    private void Start()
    {
        fixedDeltaTime = Time.fixedDeltaTime;
        timeScale = Time.timeScale;
    }

    // Update is called once per frame
    void Update () {
        if (CrossPlatformInputManager.GetButton("Fire1"))
        {
            recording = false;
        }
        else
        {
            recording = true;
        }


        if ((isPaused && Input.GetKeyDown(KeyCode.P)) ||
            Input.GetKeyDown(KeyCode.R) || resumeNow)
        {
            Resume();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }

	}

    void PauseGame()
    {
        Time.timeScale = 0f;
        Time.fixedDeltaTime = 0f;
        isPaused = true;
    }

    void Resume()
    {
        isPaused = false;
        Time.timeScale = timeScale;
        Time.fixedDeltaTime = fixedDeltaTime;
    }

    private void OnApplicationPause(bool pause)
    {
        isPaused = pause;
        resumeNow = true;
    }
}
