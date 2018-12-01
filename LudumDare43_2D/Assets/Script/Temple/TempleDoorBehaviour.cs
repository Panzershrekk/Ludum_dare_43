using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleDoorBehaviour : MonoBehaviour {
    // public
    public GameObject dialogueCanvas;
    public GameObject conv;
    public GameObject successItemConv;
    public GameObject failItemConv;

    public VNEngine.VNSceneManager sceneManager;

    // private
    private PlayerBehavior player;


    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();
    }

    public void TryDropItem(int itemIdx)
    {
        if (player.TrySacrificeItem(itemIdx))
        {
            sceneManager.Start_Conversation(successItemConv);            
        }
        else
        {
            sceneManager.Start_Conversation(failItemConv);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            dialogueCanvas.SetActive(true);
            sceneManager.Start_Conversation(conv);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            dialogueCanvas.SetActive(false);
        }
    }
}
