using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundButton : MonoBehaviour
{
    public AudioControl AudioControl;
    public Text ButtonText;
    private string SceneName;

    public void Start()
    {
        if(!PlayerPrefs.HasKey("SavedAudio"))
        {
            AudioControl.Unmute();
            ButtonText.text = "Sound : On";
            PlayerPrefs.GetString("SavedAudio", "On");
        }

        if (PlayerPrefs.GetString("SavedAudio", "On") == "On")
        {
            AudioControl.Unmute();
            ButtonText.text = "Sound : On";
        }
        else
        {
            AudioControl.Mute();
            ButtonText.text = "Sound : Off";
        }
    }

    public void OnClick()
    {
        if (PlayerPrefs.GetString("SavedAudio", "On") == "On")
        {
            AudioControl.Mute();
            ButtonText.text = "Sound : Off";
            PlayerPrefs.SetString("SavedAudio", "Off");
        }
        else
        {
            AudioControl.Unmute();
            ButtonText.text = "Sound : On";
            PlayerPrefs.SetString("SavedAudio", "On");
        }
    }
}
