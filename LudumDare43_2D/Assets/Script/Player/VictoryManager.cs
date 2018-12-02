using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VNEngine;

public class VictoryManager : MonoBehaviour {

    public VNSceneManager scenemanager;
    public GameObject VictoryConversation;
	public AudioSource victoryMusic;

    public void Win()
    {
        scenemanager.transform.parent.gameObject.SetActive(true);
        scenemanager.Start_Conversation(VictoryConversation);
		MusicManager m = GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>();
		m.desertFightAudio.volume = 0;
		m.quietDesertAudio.volume = 0;
		victoryMusic.Play();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
			GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>().stats.moveSpeed = 0;
            Win();
        }
    }
}
