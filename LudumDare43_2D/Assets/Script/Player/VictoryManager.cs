using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VNEngine;

public class VictoryManager : MonoBehaviour {

    VNSceneManager scenemanager;
    public GameObject VictoryConversation;

    public void Win()
    {
        scenemanager = GameObject.FindObjectOfType<VNSceneManager>();
        scenemanager.transform.parent.gameObject.SetActive(true);
        scenemanager.Start_Conversation(VictoryConversation);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Win();
        }
    }
}
