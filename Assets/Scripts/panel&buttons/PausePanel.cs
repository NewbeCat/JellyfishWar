using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    [SerializeField] Button resumeBtn;
    [SerializeField] Button retryBtn;
    [SerializeField] Button settBtn;
    [SerializeField] Button exitBtn;
    [SerializeField] GameObject pauseMenuCanvas;
    [SerializeField] GameObject settingsMenuCanvas;

    private void Start()
    {
        resumeBtn.onClick.AddListener(() => ResumeBtn());
        retryBtn.onClick.AddListener(() => RetryBtn());
        settBtn.onClick.AddListener(() => SettBtn());
        exitBtn.onClick.AddListener(() => ExitBtn());
    }

    public void ResumeBtn()
    {
        GameManager gameManager = GameManager.instance;

        if (gameManager.GameIsPaused)
        {
            Time.timeScale = 1f;
            gameManager.GameIsPaused = false;
            pauseMenuCanvas.SetActive(false);
        }
        else
        {
            Time.timeScale = 0f;
            gameManager.GameIsPaused = true;
            pauseMenuCanvas.SetActive(true);
        }
    }
    public void SettBtn()
    {
        settingsMenuCanvas.SetActive(true);
        pauseMenuCanvas.SetActive(false);
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

    private IEnumerator DelayedAction(System.Action action)
    {
        AudioManager.Instance.StopMusic();
        yield return new WaitForSeconds(0.5f); // Adjust the delay time as needed
        action.Invoke();
    }

}