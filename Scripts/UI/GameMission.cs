using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMission : MonoBehaviour
{
    public float gameTimer;
    public Text gameTimerText;
    public GameObject gameOverPanel;
    public GameObject missionPanel;
    public Animator animSTART;
    public Animator animGAMEOVER;
    private int Timer;

    // Start is called before the first frame update
    void Start()
    {
        missionPanel.SetActive(true);
        animSTART.SetBool("LevelStart", true);
    }
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(LevelStart());
    }
    IEnumerator LevelStart()
    {    
        yield return new WaitForSeconds(5.0f);
        if (gameTimer > 0)
        {
            missionPanel.SetActive(false);
            gameTimer -= Time.deltaTime;
            Timer = (int)(float)gameTimer;
            gameTimerText.text = "TIME LEFT : " + Timer.ToString();
        }
        else
        {
            missionPanel.SetActive(false);
            gameOverPanel.SetActive(true);
            animGAMEOVER.SetBool("GameOver", true);
            Debug.Log("GAME OVER!!");
            StartCoroutine(NewGame());
        }
    }
    IEnumerator NewGame()
    {
        yield return new WaitForSeconds(10.0f);
        Debug.Log("NEW GAME!");
        SceneManager.LoadScene("MAIN MENU");
    }
}
