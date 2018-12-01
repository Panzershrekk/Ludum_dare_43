using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VNEngine;

public class WildEventsScript : MonoBehaviour {
    public GameObject dialogueCanvas;
    public VNSceneManager sceneManager;

    public void FireWildEvent(string eventName)
    {
        dialogueCanvas.SetActive(true);
        sceneManager.Start_Conversation(GameObject.Find(eventName));
    }

    public void FinishEvent()
    {
        dialogueCanvas.SetActive(false);
    }
}
