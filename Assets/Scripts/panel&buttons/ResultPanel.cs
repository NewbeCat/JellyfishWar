using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class ResultPanel : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI currentScoreText;
    [SerializeField] TextMeshProUGUI previousHighScoreText;

    [SerializeField] Button retryBtn;
    [SerializeField] Button exitBtn;

    int previousHighScore = 0;

    [SerializeField] GameObject playerimg;
    [SerializeField] GameObject guardimg;
    [SerializeField] GameObject player;
    private SpriteRenderer playerRenderer;
    private SpriteRenderer guardRenderer;
    private Color playerColor;
    private Color guardColor;
    private void Start()
    {
        retryBtn.onClick.AddListener(() => RetryBtn());
        exitBtn.onClick.AddListener(() => ExitBtn());
        playerRenderer = playerimg.GetComponent<SpriteRenderer>();
        guardRenderer = guardimg.GetComponent<SpriteRenderer>();
        playerColor = playerRenderer.color;
        guardColor = guardRenderer.color;
    }

    private void OnEnable()
    {
        ScoreCalculation();
    }

    private void ScoreCalculation()
    {
        int curScore = ((int)GameManager.instance.Score);

        bool isFirstPlay = !PlayerPrefs.HasKey("HighScore");

        string curScoretxt = "";
        string previousScoretxt = "";


        if (isFirstPlay == false)
        {
            previousHighScore = PlayerPrefs.GetInt("HighScore");

            if (curScore > previousHighScore)
            {
                PlayerPrefs.SetInt("HighScore", curScore);

                curScoretxt = $"BEST! {curScore}";
                previousScoretxt = $"{previousHighScore}";
            }
            else
            {
                curScoretxt = $"{curScore}";
                previousScoretxt = $"BEST! {previousHighScore}";
            }
            previousHighScoreText.text = previousScoretxt;
        }
        else
        {
            PlayerPrefs.SetInt("HighScore", curScore);
            previousHighScoreText.gameObject.SetActive(false);
            curScoretxt = $"BEST! {curScore}";
        }

        currentScoreText.text = curScoretxt;
    }

    private void RetryBtn()
    {
        AudioManager.Instance.StopMusic();
        SceneManager.LoadScene("Test");
    }

    private void ExitBtn()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
