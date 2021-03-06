﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyScript : Photon.MonoBehaviour
{
    public int index = 1;

    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine) 
        {
            switch (index) 
            {
                case 1: // Head
                    transform.position = ViveManager.Instance.head.transform.position;
                    transform.rotation = ViveManager.Instance.head.transform.rotation; 
                    break;
                case 2: // LeftHand
                    transform.position = ViveManager.Instance.leftHand.transform.position;
                    transform.rotation = ViveManager.Instance.leftHand.transform.rotation;
                    break;
                case 3: // RightHand
                    transform.position = ViveManager.Instance.rightHand.transform.position;
                    transform.rotation = ViveManager.Instance.rightHand.transform.rotation;
                    break;
            }
        }
    }
}
