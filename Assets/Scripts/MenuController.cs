//@Author Justin Scott Bieshaar
//Portfolio: www.justinbieshaar.com 
//Contact: contact@justinbieshaar.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

    [SerializeField] private GameObject fadeImage;

    public void StartButton () {
        fadeImage.SetActive(true);
        GetComponent<Animator>().SetInteger("state", 2);
    }
    public void QuitButton () {
        Application.Quit();
    }

    public void OptionsButton () {
        GetComponent<Animator>().SetInteger("state", 1);
    }

    public void BackButton () {
        GetComponent<Animator>().SetInteger("state", 0);
    }
    public void StartGame () {

        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
