﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Countdown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI uiText;
    [SerializeField] private float mainTimer;

    private float timer;
    private bool canCount = true;
    private bool doOnce = false;

    void Start() {
        timer = mainTimer;    
    }

    void Update() {
        if (timer >= 0.0f && canCount) {
            timer -= Time.deltaTime;
            uiText.text = timer.ToString("0.0");
        } else if (timer <= 0.0f && !doOnce) {
            canCount = false;
            doOnce = true;
            uiText.text = "0.0";
            timer = 0.0f;
            Debug.Log("Return to main menu from timer!");
            PhotonNetwork.Disconnect();
            SceneManager.LoadScene(0);
        }
    }
}
