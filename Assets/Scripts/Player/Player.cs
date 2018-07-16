using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public float torchCounter, sprintCounter, powerCounter;

    [HideInInspector]
    public bool powerMode;

    public FirstPersonController firstPersonController;
    public Game game;
    public Text BatteryText;
    public Text SprintText;
    public Text PickupText;
    public GameObject InGameMenuObj;
    public GameObject WinMenuObj;
    public GameObject LoseMenuObj;
    public GameObject torch;

    private TouchRayBox TouchRayBox;
    private bool allowTorch;
    private bool toggleTorch;
    private Pickup item;

    public void Start()
    {
        TouchRayBox = GetComponent<TouchRayBox>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            item = other.gameObject.GetComponent<Pickup>();
            PickupText.text = item.NearbyText;
            PickupText.enabled = true;
        }

        if (other.CompareTag("Gate"))
        {
            ReachGate();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            item = null;
            PickupText.text = "";
            PickupText.enabled = false;
        }
    }

    public void Update()
    {
        Torch();
        Pickup();
        InGameMenu();
        RecoverSprint();

        if (!powerMode)
        {
            Sprint();
        }
        else
        {
            PowerSprint();

            if (Time.time > powerCounter)
            {
                powerMode = false;
            }
        }
    }

    public void Attacked()
    {
        LoseMenuObj.SetActive(true);
        firstPersonController.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Invoke("StopTime", 2f);
    }

    public void LoseGame()
    {
        LoseMenuObj.SetActive(true);
        firstPersonController.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }

    public void StopTime()
    {
        Time.timeScale = 0;
    }

    public void ReachGate()
    {
        Time.timeScale = 0;
        WinMenuObj.SetActive(true);
        firstPersonController.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void InGameMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale != 0)
            {
                Time.timeScale = 0;
                InGameMenuObj.SetActive(true);
                firstPersonController.enabled = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else if(InGameMenuObj.activeInHierarchy)
            {
                Time.timeScale = 1;
                InGameMenuObj.SetActive(false);
                firstPersonController.enabled = true;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    private void Torch()
    {
        // Toggle
        toggleTorch = Input.GetKeyDown(KeyCode.Q);
        if (toggleTorch && allowTorch && Time.timeScale != 0)
        {
            torch.SetActive(!torch.activeInHierarchy);
            TouchRayBox.Repel = torch.activeInHierarchy;
            if (torch.activeInHierarchy)
            {
                torchCounter -= 0.5f;
            }
        }

        // Timer
        if (torch.activeInHierarchy)
        {
            torchCounter -= Time.deltaTime;
        }

        // Counter
        if (torchCounter <= 0)
        {
            torch.SetActive(false);
            TouchRayBox.Repel = false;
            allowTorch = false;
            BatteryText.color = Color.red;
        }
        else if (torchCounter >= 1)
        {
            allowTorch = true;
            BatteryText.color = Color.green;
        }

        // Text
        BatteryText.text = "Battery:" + torchCounter.ToString("N0");
    }

    private void Pickup()
    {
        // Pickup
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (item)
            {
                item.PickUp(this);
            }
        }
    }

    private void Sprint()
    {
        // Timer
        if (!firstPersonController.m_IsWalking)
        {
            sprintCounter -= Time.deltaTime;
        }
    }

    private void PowerSprint()
    {
        firstPersonController.allowSprint = true;
    }

    private void RecoverSprint()
    {
        // Counter
        if (sprintCounter < game.sprintStart && (firstPersonController.m_IsWalking || powerMode))
        {
            sprintCounter += Time.deltaTime * 1.5f;
        }

        if (sprintCounter <= 0)
        {
            firstPersonController.allowSprint = false;
            SprintText.color = Color.red;
        }
        else if (sprintCounter >= 3)
        {
            firstPersonController.allowSprint = true;
            SprintText.color = Color.green;
        }

        SprintText.text = "Sprint:" + sprintCounter.ToString("N0");
    }
}
