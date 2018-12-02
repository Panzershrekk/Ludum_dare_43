using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour {

    private PlayerBehavior player;
    public AudioSource deathMusic;
    public AudioSource deathSound;
    public int checker = 0;

	// Use this for initialization
	void Start () {
        player = this.GetComponent<PlayerBehavior>();
    }

    // Update is called once per frame
    void Update () {
        if (player.stats.isDead && checker == 0)
        {
            checker = 1;
            MusicManager m = GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>();
            m.desertFightAudio.volume = 0;
            m.quietDesertAudio.volume = 0;
            deathMusic.Play();
			deathMusic.volume = 1.0f;
            deathSound.Play();
            StartCoroutine("FadeIntoMenu");
        }
	}

    IEnumerator FadeIntoMenu()
    {
        while (deathMusic.isPlaying)
        {
            yield return new WaitForSeconds(.1f);
        }
        SceneManager.LoadScene(0);
    }
}
