using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueButton : MonoBehaviour
{
    private string SceneName;

    public void Start()
    {
        SceneName = PlayerPrefs.GetString("SavedLevel", "LevelOne");
    }

    public void OnClick()
    {
        SceneManager.LoadScene(SceneName);
    }
}
