using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public AudioSource quietDesertAudio;
    public AudioSource desertFightAudio;

    // Use this for initialization
    void Start () {
        desertFightAudio.volume = 0;
    }
	
    // On execute 50 fois un changement de volume avec 0.1 seconde par changement donc 5 secondes pour le fade de 0 à 100% vol
    IEnumerator FadeAudios(AudioSource audioIn, AudioSource audioOut)
    {
        for (int i = 0; i < 50; i++)
        {
            audioIn.volume += 0.02f;
            audioOut.volume -= 0.02f;

            CheckVolume(audioIn);
            CheckVolume(audioOut);

            // Ca c'est pour dire qu'on execute un morceau de boucle tous les 0.1 secondes
            yield return new WaitForSeconds(.1f);
        }
    }

    public void CheckVolume(AudioSource audio)
    {
        if (audio.volume > 1f) audio.volume = 1;
        if (audio.volume < 0f) audio.volume = 0f;
    }

    // Exemple
    public void LaunchDesertFight()
    {
        var quietToFightCoroutine = FadeAudios(desertFightAudio, quietDesertAudio);
        StartCoroutine(quietToFightCoroutine);
    }

    public void LaunchQuietDesert()
    {
        var fightToQuietCoroutine = FadeAudios(quietDesertAudio, desertFightAudio);
        StartCoroutine(fightToQuietCoroutine);
    }


}
