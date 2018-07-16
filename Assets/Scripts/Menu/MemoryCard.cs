using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    public GameObject ContinueButton;
    public bool NewGame;

    public void Awake()
    {
        if (!PlayerPrefs.HasKey("SavedLevel") || NewGame)
        {
            PlayerPrefs.DeleteAll();
            ContinueButton.SetActive(false);
        }
        else
        {
            ContinueButton.SetActive(true);
        }
    }
}
