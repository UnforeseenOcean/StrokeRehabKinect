﻿using UnityEngine;
using System.Collections;

public class CardManager : MonoBehaviour {

    public GameObject sceneManager;
    public GameObject avatarController;
    public TimerText timerManager;
    public ScoreManager scoreManager;
    public GameObject calibManager;

    public GameObject leftCanvas;
    public GameObject rightCanvas;

    public int correctMatches = 0;
    public int currentLevel = 0;
    public int[] levelTarget;
    public GameObject[] cardCollections;

    public GameObject prompt1;
    int timer;

    void Awake()
    {
        sceneManager = GameObject.Find("SceneManager");
    }
    void Start()
    {
        if (sceneManager != null)
        {
            //  Debug.Log("focus side " + sceneManager.GetComponent<SceneManager>().focusSide);
            if (SceneManager.focusSide == 1)
            {
                avatarController.GetComponent<AvatarController>().activeJoint = 1;
                leftCanvas.SetActive(true);
                rightCanvas.SetActive(false);
                //  focusText.text = "Focus Side: \n Left Arm";
            }
            if (SceneManager.focusSide == 2)
            {
                avatarController.GetComponent<AvatarController>().activeJoint = 2;
                leftCanvas.SetActive(false);
                rightCanvas.SetActive(true);
                // focusText.text = "Focus Side: \n Right Arm";
            }
            else
            {
                avatarController.GetComponent<AvatarController>().activeJoint = 1;
                leftCanvas.SetActive(true);
                rightCanvas.SetActive(false);
                //   focusText.text = "Focus Side: \n Left Arm";
            }

            sceneManager.GetComponent<SceneManager>().kinectManager.GetComponent<KinectManager>().avatarControllers[0] = avatarController.GetComponent<AvatarController>();
            calibManager.GetComponent<CalibrationManager>().kinectManager = sceneManager.GetComponent<SceneManager>().kinectManager.GetComponent<KinectManager>();
        }
        timer = 0;
        prompt1.SetActive(false);

        for (int i = 0; i < cardCollections.Length; i++)
        {
            cardCollections[i].SetActive(false);
        }
        cardCollections[0].SetActive(true);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
