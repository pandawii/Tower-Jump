using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //scripts
    player PlayerScript;
    levelgen lvlgenscript;
    //GameObjects
    [SerializeField] GameObject GameOverMenu;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject Tilt;
    [SerializeField] GameObject FirstPlatform;
    [SerializeField] GameObject PauseButtonObject;
    [SerializeField] GameObject BoostBar;
    [SerializeField] GameObject pressAnyWhereToStart;
    [SerializeField] GameObject ScoreText;
    [SerializeField] GameObject Camera;
    //Transform
    Transform positionTORestart;
    Transform Player_transform;
    Rigidbody2D rb;

   public bool IsGameLost;
    bool IsGameStarted;
    bool Ispaused;

   
    void Awake()
    {
        //Refrences
        PlayerScript = GameObject.Find("Player").GetComponent<player>();
        positionTORestart = GameObject.Find("PositionToRestart").transform;
        Player_transform = GameObject.Find("Player").transform;
        rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        lvlgenscript = GameObject.Find("LevelGenerator").GetComponent<levelgen>();

        PlayerScript.enabled = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        GameOverMenu.SetActive(false);
        lvlgenscript.enabled = false;
        pressAnyWhereToStart.SetActive(false);
        ScoreText.SetActive(false);
        PauseMenu.SetActive(false);
        IsGameStarted = false;
        Ispaused = false;

    }

    void GameStartedMainCameraEvent()
    {
        BoostBar.SetActive(true);
        Tilt.SetActive(true);
        pressAnyWhereToStart.SetActive(true);
       

    }
    void Update()
    {
        if (Input.anyKey&& !IsGameStarted)
        {
            
            IsGameStarted=true;
        }
        if (IsGameStarted&& !Ispaused)
        {
            GameStarted();
        }
        if (Player_transform.transform.position.y < positionTORestart.position.y) 
        {
             GameLost();
        }
        if (Player_transform.transform.position.y>5f)
        {
            ScoreText.SetActive(true);
        }
    }
    private void GameStarted()
    {
        Time.timeScale = 1f;
        rb.bodyType = RigidbodyType2D.Dynamic;
        PauseButtonObject.SetActive(true);
        lvlgenscript.enabled = true;
        Tilt.SetActive(false);
        if (!IsGameLost)
        {
            FirstPlatform.AddComponent<FirstPlatformJump>();
        }
    }
    private void GameLost()
    {
        Time.timeScale = 1f;
        GameOverMenu.SetActive(true);
        PlayerScript.enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
        IsGameLost = true;
        PauseButtonObject.SetActive(false);
        BoostBar.SetActive(false);
        IsGameLost = true;        
    }

    public void PauseButton()
    {
        if (IsGameLost==false)
        {
            Ispaused=true;
            Camera.GetComponent<camera>().enabled = false;
            PlayerScript.enabled = false;
            rb.bodyType = RigidbodyType2D.Static;
            PauseMenu.SetActive(true);
            PauseButtonObject.SetActive(false);
            Debug.Log("Paused");
        }
    }
    public void ResumeButton()
    {
        Camera.GetComponent<camera>().enabled = true;
        Time.timeScale = 1f;
        PlayerScript.enabled = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        PauseMenu.SetActive(false);
        PauseButtonObject.SetActive(true);
        Debug.Log("Pressed");
    }
    public void RestartButton()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("base");
    }
        
    public void MainMenuButton()
    {
        SceneManager.LoadScene("StartMenu");
        Time.timeScale = 1f;
    }
}
