using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public GameObject HowTo;
    public GameObject Accueil;

    public void PlayTheGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowHowTo()
    {
        HowTo.SetActive(true);
        Accueil.SetActive(false);
    }

    public void HideHowTo()
    {
        HowTo.SetActive(false);
        Accueil.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
