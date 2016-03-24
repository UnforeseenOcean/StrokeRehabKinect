﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public InteractionManager intManager;
	public GameObject gamePage;
    public GameObject focusSelectionPage;
	public GameObject titlePage;
    public AvatarController avatarCont;
	public AlternateController altControl;
    public bool focusSelected = false;
	// Use this for initialization
	void Start () {

        focusSelectionPage.SetActive(false);
        gamePage.SetActive(false);
        titlePage.SetActive(true);
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(avatarCont.gameObject);
		InvokeRepeating ("CheckController", 0.1f, 0.1f);
	}
	
	// Update is called once per frame
	void Update () {

//        Debug.Log(intManager.userActive);
	
	
	}

	void CheckController()
	{
		if (!altControl.noKinect) {
			if (intManager.userActive && !focusSelected) {
				titlePage.SetActive (false);
                focusSelectionPage.SetActive(true);
                CheckStatus();
            } else if(focusSelected) {
               
				focusSelectionPage.SetActive (false);
				gamePage.SetActive (true);
			}
		} else {
			titlePage.SetActive (false);
			gamePage.SetActive (true);
		}
	}

    void CheckStatus()
    {
        Debug.Log("left" + intManager.leftHandPos.y);
        Debug.Log("right" + intManager.rightHandPos.y);
        if (intManager.leftHandPos.y > intManager.rightHandPos.y)
        {
            avatarCont.activeJointPos = intManager.leftHandPos;
            Debug.Log("left");
            focusSelectionPage.transform.GetChild(0).gameObject.SetActive(false);
            focusSelectionPage.transform.GetChild(1).gameObject.SetActive(true);
            focusSelectionPage.transform.GetChild(3).gameObject.SetActive(true);
            StartCoroutine("ActivateLeft");

        }
        else
        {
            avatarCont.activeJointPos = intManager.rightHandPos;
            Debug.Log("right");
            focusSelectionPage.transform.GetChild(0).gameObject.SetActive(false);
            focusSelectionPage.transform.GetChild(2).gameObject.SetActive(true);
            focusSelectionPage.transform.GetChild(3).gameObject.SetActive(true);
            StartCoroutine("ActivateRight");
        }
    }

    IEnumerator ActivateLeft()
    {
        yield return new WaitForSeconds(2f);
        if (intManager.leftHandPos.y > intManager.rightHandPos.y)
        {
            focusSelected = true;
            focusSelectionPage.transform.GetChild(2).gameObject.SetActive(false);
        }

            yield return null;
    }
    IEnumerator ActivateRight()
    {
        yield return new WaitForSeconds(2f);
        if (intManager.rightHandPos.y > intManager.leftHandPos.y)
        {
            focusSelected = true;
            focusSelectionPage.transform.GetChild(2).gameObject.SetActive(false);
        }

            yield return null;
    }

    public void ActivateApp(int appNumber)
    {
        switch(appNumber)
        { 

            case 1:
                Application.LoadLevel("DrawingChallenge");
                break;
            case 2:
                Application.LoadLevel("CardMatching");
                break;
            case 3:
                Application.LoadLevel("ConductorMusic");
                break;
        }
    }
}
