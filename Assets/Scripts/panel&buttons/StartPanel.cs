using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StartPanel : MonoBehaviour
{
    [SerializeField] Button startBtn;
    [SerializeField] Button settBtn;
    [SerializeField] Button exitBtn;
    [SerializeField] GameObject settingsMenuCanvas;

    [SerializeField] private int introplease = 0;

    private void Start()
    {
        AudioManager.Instance.PlayMusic("메인");
        startBtn.onClick.AddListener(() => StartCoroutine(DelayedAction(RetryBtn)));
        settBtn.onClick.AddListener(() => SettBtn());
        exitBtn.onClick.AddListener(() => StartCoroutine(DelayedAction(ExitBtn)));
    }

    private IEnumerator DelayedAction(System.Action action)
    {
        AudioManager.Instance.StopMusic();
        yield return new WaitForSeconds(1f); // Adjust the delay time as needed
        action.Invoke();
    }

    private const int TutorialPlayedKey = 1;  // An arbitrary number to represent the key

    private void RetryBtn()
    {
        // Check if the tutorial has been played before
        bool tutorialPlayed = GetBool(TutorialPlayedKey);

        if (!tutorialPlayed || introplease == 1)
        {
            // If not played before, go to Tutorial scene
            SceneManager.LoadScene("Tutorial");

            // Set the flag to indicate that the tutorial has been played
            SetBool(TutorialPlayedKey, true);
        }
        else if (tutorialPlayed || introplease == 2)
        {
            SceneManager.LoadScene("Test");
        }
    }

    private bool GetBool(int key)
    {
        return PlayerPrefs.GetInt(key.ToString(), 0) == 1;
    }

    private void SetBool(int key, bool value)
    {
        PlayerPrefs.SetInt(key.ToString(), value ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SettBtn()
    {
        settingsMenuCanvas.SetActive(true);
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