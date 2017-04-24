//@Author Justin Scott Bieshaar
//Portfolio: www.justinbieshaar.com 
//Contact: contact@justinbieshaar.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager :MonoBehaviour {

    public static GameManager instance;
    public static bool paused = false;

    [SerializeField] private CharacterMovement character;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text timeText;
    [SerializeField] private Text highscoreText;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private Animator pause;

    public float totalTimePlayed { get; private set; }
    private float timePlayed = 0;
    private int minutePlayed = 0;
    public float score { get; private set; }

    private bool died = false;

    void Awake () {
        instance = this;
    }

	void Start () {
        if (character == null)
            character = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();
        timePlayed = 0;

        highscoreText.text = ""+ PlayerPrefs.GetInt("Score") + " :Highscore" ;

    }

    void Update () {

        if (Input.GetKeyUp(KeyCode.Escape)) {
            paused = !paused;

            if (paused)
                pause.SetInteger("state", 1);
            else
                pause.SetInteger("state", 0);
        }
        if (GameManager.paused) return;
        totalTimePlayed += Time.deltaTime;
        AddScore();
        scoreText.text = "" + (int)score;

        if (timePlayed < 60) {
            timePlayed += Time.deltaTime;
        } else {
            minutePlayed++;
            timePlayed = 0;
        }

        if (minutePlayed < 10) {
            if (timePlayed < 10)
                timeText.text = "0" + minutePlayed + ":0" + (int)timePlayed;
            else
                timeText.text = "0" + minutePlayed + ":" + (int) timePlayed;
        } else {

            if (timePlayed < 10)
                timeText.text = "" + minutePlayed + ":0" + (int) timePlayed;
            else
                timeText.text = "" + minutePlayed + ":" + (int) timePlayed;
        }
    }

    void AddScore () {
        if (died) return;
        if (character.getMovevementZPercentage < 0)
            score += 1 * -character.getMovevementZPercentage;
        else score += 1 * character.getMovevementZPercentage;
    }

    public void Die () {
        explosion.Play();
        character.Die();
        died = true;

        if(PlayerPrefs.GetInt("Score")< (int)score)
            PlayerPrefs.SetInt("Score", (int) score);
    }

    public void Resume () {
        paused = false;
        pause.SetInteger("state", 0);
    }

    public void OptionsButton () {
        pause.SetInteger("options", 1);
    }

    public void BackButton () {
        pause.SetInteger("options", 0);
    }
}
