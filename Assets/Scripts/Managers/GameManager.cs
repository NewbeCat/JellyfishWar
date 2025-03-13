using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake() => instance = this;
    #region ETC
    [Header("ETC")]

    public MapMG map;
    public Player player;
    public SpawnMG spawn;
    #endregion


    #region GameOver
    [Space(15f)]
    [Header("GameOver")]

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pause;
    [SerializeField] private GameObject clickimg;
    public bool isGameOver;
    public bool GameIsPaused = true;
    public bool startsc = true;
    #endregion


    #region Score
    [Space(15f)]
    [Header("Score")]

    [SerializeField] private TextMeshProUGUI scoreTxt;
    public float speed = 1;
    private float score;
    public float Score
    {
        get { return score; }
        set
        {
            score = value;

            scoreTxt.text = ((int)score).ToString("00000000");

        }
    }
    #endregion

    void Start()
    {
        player = FindObjectOfType<Player>();
        map = FindObjectOfType<MapMG>();
        spawn = FindObjectOfType<SpawnMG>();
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (startsc && Input.GetKeyDown(KeyCode.Space))
        {
            startsc = false;
            clickimg.SetActive(false);
            AudioManager.Instance.PlaySFX("게임 스타트");
            GameIsPaused = false;
            pause.SetActive(true);
            Time.timeScale = 1f;
            AudioManager.Instance.PlayMusic("스테이지");
        }

        if (!startsc && !GameIsPaused && !isGameOver)
        {
            Score += speed / 10;
        }
    }
    public void TimeEvent()
    {
        map.SpdEvent();
        spawn.GameSpdEvent();
    }

    public void GameOver()
    {
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.StopSFX();
        AudioManager.Instance.PlaySFX("endbeats");
        AudioManager.Instance.PlaySFX("게임오버_플레이어");
        pause.SetActive(false);
        GameIsPaused = true;
        isGameOver = true;
        Time.timeScale = 0f;

        gameOverPanel.SetActive(true);
    }

}
