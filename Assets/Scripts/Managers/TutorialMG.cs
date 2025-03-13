using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TutorialMG : MonoBehaviour
{
    [Header("INTRO")]
    [SerializeField] private Image introBG;
    [SerializeField] private List<string> introDialogue;
    [SerializeField] private Text introDialogueText;
    [Space(50f)]
    [Header("CUTSCENE")]
    [SerializeField] private GameObject cutSceneCanvas;
    [SerializeField] private List<string> cutSceneDialogue;
    [SerializeField] private Text cutSceneDialogueText;
    [SerializeField] private GameObject[] cutSceneCharImage;
    [Space(50f)]
    [Header("MAIN")]
    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private Player player;
    [SerializeField] private GameObject enemy;
    [SerializeField] private List<string> mainDialogue;
    [SerializeField] private Text mainDialogueText;


    public static TutorialMG Instance;


    public int mainDialogueNum = 0;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        player.isTutorial = true;
        AudioManager.Instance.StopMusic();
        StartCoroutine(Intro());
    }

    private void EnemySpawn()
    {
    }

    private IEnumerator Intro()
    {
        yield return StartCoroutine(DialoguePrint(introDialogueText, introDialogue, 2f));

        yield return new WaitForSeconds(0.5f);
        introDialogueText.DOFade(0, 0.5f);
        introBG.DOFade(0, 0.5f);
        yield return new WaitForSeconds(0.5f);
        introBG.gameObject.SetActive(false);
        cutSceneCanvas.SetActive(true);
        AudioManager.Instance.PlayMusic("메인");
        yield return StartCoroutine(CutSceneDialoguePrint(cutSceneDialogueText, cutSceneDialogue, 2f));
        AudioManager.Instance.StopMusic();
        yield return new WaitForSeconds(1f);

        print("CutSceneEnd");
        SceneManager.LoadScene("Test");

        // Ʃ�� 
        //mainCanvas.SetActive(true);
        //cutSceneCanvas.SetActive(false);
        //player.gameObject.SetActive(true);


        //yield return StartCoroutine(MainDialoguePrint(0, mainDialogue, 2f));
        //yield return StartCoroutine(MainDialoguePrint(1, mainDialogue, 2f));
        //yield return new WaitForSeconds(5f);
        //yield return StartCoroutine(MainDialoguePrint(2, mainDialogue, 2f));

        ////�� ���� �Ѹ���
        //yield return StartCoroutine(MainDialoguePrint(3, mainDialogue, 2f));
        //yield return StartCoroutine(MainDialoguePrint(4, mainDialogue, 2f));

    }

    private IEnumerator MainDialogue2()
    {
        yield return StartCoroutine(MainDialoguePrint(5, mainDialogue, 2f));
        yield return StartCoroutine(MainDialoguePrint(6, mainDialogue, 2f));
        // hp
        yield return new WaitForSeconds(1f);

        //�� ����

    }
    private IEnumerator MainDialogue3()
    {
        yield return StartCoroutine(MainDialoguePrint(7, mainDialogue, 2f));
    }
    private IEnumerator MainDialogue4()
    {
        yield return StartCoroutine(MainDialoguePrint(8, mainDialogue, 2f));
        yield return StartCoroutine(MainDialoguePrint(9, mainDialogue, 2f));
        yield return StartCoroutine(MainDialoguePrint(10, mainDialogue, 2f));
        yield return StartCoroutine(MainDialoguePrint(11, mainDialogue, 2f));
        yield return StartCoroutine(MainDialoguePrint(12, mainDialogue, 2f));

    }


    private IEnumerator DialoguePrint(Text targetText, List<string> dialogue, float spd)
    {
        WaitForSeconds wait = new WaitForSeconds(spd);

        for (int i = 0; i < dialogue.Count; i++)
        {
            targetText.text = dialogue[i];
            yield return wait;
        }
    }

    private IEnumerator CutSceneDialoguePrint(Text targetText, List<string> dialogue, float spd)
    {
        WaitForSeconds wait = new WaitForSeconds(spd);

        for (int i = 0; i < dialogue.Count; i++)
        {
            cutSceneCharImage[0].SetActive(i != 1);
            cutSceneCharImage[1].SetActive(i == 1);

            targetText.text = dialogue[i];

            yield return wait;
        }
    }

    public IEnumerator MainDialoguePrint(int num, List<string> dialogue, float spd)
    {
        mainDialogueNum = num;

        WaitForSeconds wait = new WaitForSeconds(spd);
        mainDialogueText.text = dialogue[num];
        yield return wait;
    }


    public void MainDialoguePrint2()
    {
        StartCoroutine(MainDialogue2());
    }

    public void MainDialoguePrint3()
    {
        StartCoroutine(MainDialogue3());
        StartCoroutine(player.Skill());
    }

    public void MainDialoguePrint4()
    {
        StartCoroutine(MainDialogue4());
    }

}
