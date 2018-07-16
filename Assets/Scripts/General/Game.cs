using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public GameObject FPSController;
    public AudioControl AudioControl;
    public Player player;
    public Text TimerText;
    public float startAngle;
    public float gameDuration;
    public float torchStart;
    public float sprintStart;

    private float endTime;

    public void Start()
    {
        StartGame();
    }

    public void Update()
    {
        GameTimer();
    }

    public void StartGame()
    {
        FPSController.transform.position = new Vector3(0, 3, 0);
        FPSController.transform.rotation = Quaternion.Euler(0, startAngle, 0);
        player.torchCounter = torchStart;
        player.sprintCounter = sprintStart;
        endTime = Time.time + gameDuration + 0.99f;
        PlayerPrefs.SetString("SavedLevel", SceneManager.GetActiveScene().name);

        if(PlayerPrefs.GetString("SavedAudio", "On") == "On")
        {
            AudioControl.Unmute();
        }
        else
        {
            AudioControl.Mute();
        }
    }

    private void GameTimer()
    {
        if (endTime < Time.time)
        {
            player.LoseGame();
        }

        System.TimeSpan gameTimer = System.TimeSpan.FromSeconds(endTime - Time.time);
        string timerFormatted = gameTimer.Minutes.ToString("D2") + ":" + gameTimer.Seconds.ToString("D2");
        TimerText.text = "Timer: " + timerFormatted;

        if (gameTimer.Minutes >= 1)
        {
            TimerText.color = Color.green;
        }
        else
        {
            TimerText.color = Color.red;
        }
    }
}