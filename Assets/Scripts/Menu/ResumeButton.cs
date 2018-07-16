using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResumeButton : MonoBehaviour
{
    public Player player;
    public GameObject InGameMenu;

    public void OnClick()
    {
        Time.timeScale = 1;
        InGameMenu.SetActive(false);
        player.firstPersonController.enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
