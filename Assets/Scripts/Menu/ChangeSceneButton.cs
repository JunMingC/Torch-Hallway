﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneButton : MonoBehaviour
{
    public string SceneName;

    public void OnClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneName);
    }
}
