using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Player : MonoBehaviour
{

    #region Move
    [Header("Move")]
    [SerializeField] private float jumpForce;
    private Rigidbody2D rb;
    #endregion 
    #region HP
    [Space(15f)]
    [Header("Hp")]
    private int hp;
    [SerializeField] private GameObject[] hpSprite;
    public int HP
    {
        get { return hp; }
        set
        {
            if (isTutorial) return;

            int curHp = hp;

            hp = value;

            if (hp <= 0)
            {
                GameOver();
            }

            if (curHp > hp)
            {
                Hit();
            }
            for (int i = 0; i < hpSprite.Length; i++)
            {
                hpSprite[i].gameObject.SetActive(((hp - 1) >= i));
            }
        }
    }

    #endregion
    #region Attacks

    [Space(15f)]
    [Header("Atk")]
    [SerializeField] private ParticleSystem atkPc;
    [SerializeField] private float atkCool;
    [SerializeField] private Image atkCoolImage;
    private CircleCollider2D atkRange;
    private float atkT;
    public bool atkOn;
    public bool isAttack;
    private float AtkCurT
    {
        get { return atkT; }
        set
        {
            atkT = value;

            if (atkT >= atkCool)
            {
                atkT = atkCool;
                atkOn = true;

                atkRange.enabled = true;
            }
            atkCoolImage.fillAmount = atkT / atkCool;
        }
    }



    [Space(15f)]
    [Header("Skill")]
    [SerializeField] float maxSkillGauge;
    [SerializeField] float skillDuration;
    [SerializeField] private Image skillGaugeImage;
    private float skillGauge;
    public float SkillGauge
    {
        get { return skillGauge; }
        set
        {

            if (isSkillOn) return;

            skillGauge = value;

            if (skillGauge >= maxSkillGauge)
            {
                if (isTutorial && TutorialMG.Instance.mainDialogueNum == 6)
                {
                    TutorialMG.Instance.MainDialoguePrint3();

                    return;
                }


                isSkillOn = true;
                if (!isAttack)
                    StartCoroutine(Skill());
                skillGauge = maxSkillGauge;
            }

            skillGaugeImage.fillAmount = skillGauge / maxSkillGauge;
        }
    }

    private bool isSkillOn;

    #endregion
    #region ETC
    [Space(15f)]
    [Header("ETC")]
    [SerializeField] private GameObject spriteImage;
    public bool isInvincibility;
    private bool isGameOver;
    public bool isTutorial = false;

    [SerializeField] private GameObject boomRange;
    private bool isBoom;
    private bool isPlane;

    #endregion

    [SerializeField] GameObject pauseMenuCanvas;
    private SpriteRenderer spriteRenderer;
    public Sprite ogSprite;
    public Sprite jumpSprite;
    public Sprite HogSprite;
    public Sprite HjumpSprite;
    public GameObject specificObject;
    [SerializeField] private float anitime;

    private guard Guard;
    [SerializeField] private GameManager g;

    void Start()
    {
        hp = 3;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = specificObject.GetComponent<SpriteRenderer>(); ;
        atkRange = atkPc.gameObject.GetComponent<CircleCollider2D>();
        Guard = GetComponentInChildren<guard>();

        AtkCurT = atkCool;
    }
    void Update()
    {
        if (isGameOver) return;

        /*
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && isAttack == false)
            {
                Jump();
            }
        }
        */

        if (Input.GetKeyDown(KeyCode.Space) && isAttack == false && !g.GameIsPaused) { Jump(); }

        if (atkOn == false /*&& isSkillOn == false*/)
            AtkCurT += Time.deltaTime;

        if ((!isSkillOn && !isAttack && !isInvincibility) && (transform.position.y > 4 || transform.position.y < -4)) isPlane = true;
        else isPlane = false;
        if (isPlane) HP--;


        transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y, -4, 4));
    }

    public void SetInvincibility(bool value)
    {
        isInvincibility = value;
    }

    private IEnumerator Attack()
    {
        isAttack = true;
        AudioManager.Instance.PlaySFX("공격");
        //GameManager.instance.speed = 15;
        //GameManager.instance.AtkEvent();
        //atkPc.gameObject.SetActive(true);
        atkPc.Play();
        StartCoroutine(SpriteRot());
        rb.velocity = Vector3.zero;
        rb.gravityScale = 0;


        yield return new WaitForSeconds(0.5f);

        if (isTutorial && TutorialMG.Instance.mainDialogueNum == 4)
        {
            TutorialMG.Instance.MainDialoguePrint2();
        }

        rb.gravityScale = 1;

        // atkPc.gameObject.SetActive(false);
        isAttack = false;

        AtkCurT = 0;
        atkOn = false;
        //GameManager.instance.speed = 1;
        //GameManager.instance.AtkEvent();

        if (isSkillOn == false)
            atkRange.enabled = false;
        else
        {
            StartCoroutine(Skill());
        }
    }

    private IEnumerator SpriteRot(float maxT = 0.5f)
    {
        float t = 0;
        while (t < maxT)
        {
            t += Time.deltaTime;
            spriteImage.transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(360f, 0f, t / maxT));
            yield return null;
        }
    }

    private bool ouch = false;
    private void Hit()
    {
        AudioManager.Instance.PlaySFX("피격_플레이어");
        StartCoroutine(HitEvent());
    }
    private IEnumerator HitEvent(int loopCnt = 5, float fade = 0.2f)
    {
        isInvincibility = true;
        ouch = true;
        spriteRenderer.sprite = HogSprite;
        spriteImage.GetComponent<SpriteRenderer>().DOFade(0, fade).SetLoops(loopCnt, LoopType.Yoyo);

        yield return new WaitForSeconds((fade * loopCnt));
        spriteImage.GetComponent<SpriteRenderer>().DOFade(1, fade);
        isInvincibility = false;
        ouch = false;
    }

    private Coroutine jumpCoroutine;
    private void Jump()
    {
        // Stop the previous jump animation if it's still running
        if (jumpCoroutine != null)
        {
            StopCoroutine(jumpCoroutine);
        }

        // Start a new jump animation
        jumpCoroutine = StartCoroutine(AnimateJump());
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private IEnumerator AnimateJump()
    {
        AudioManager.Instance.PlaySFX("점프");
        if (ouch)
        {
            spriteRenderer.sprite = HjumpSprite;
            yield return new WaitForSeconds(anitime);
            spriteRenderer.sprite = HogSprite;
        }
        else
        {
            spriteRenderer.sprite = jumpSprite;
            yield return new WaitForSeconds(anitime);
            spriteRenderer.sprite = ogSprite;
        }

        // Set jumpCoroutine to null to indicate that the animation has completed
        jumpCoroutine = null;
    }


    public IEnumerator Skill()
    {
        isSkillOn = true;
        atkRange.enabled = true;
        isInvincibility = true;

        StartCoroutine(SkillGaugeFill());

        GameManager.instance.speed = 15;
        GameManager.instance.TimeEvent();


        yield return new WaitForSeconds(skillDuration);

        if (isTutorial && TutorialMG.Instance.mainDialogueNum == 7)
        {
            TutorialMG.Instance.MainDialoguePrint4();
        }


        isSkillOn = false;

        if (atkOn == false)
        {
            atkRange.enabled = false;
        }
        SkillGauge = 0;

        GameManager.instance.speed = 1;
        GameManager.instance.TimeEvent();
        StartCoroutine(blink());
    }

    private IEnumerator blink(int loopCnt = 2, float fade = 0.2f)
    {
        spriteImage.GetComponent<SpriteRenderer>().DOFade(0, fade).SetLoops(loopCnt, LoopType.Yoyo);
        yield return new WaitForSeconds((fade * loopCnt));
        spriteImage.GetComponent<SpriteRenderer>().DOFade(1, fade);
        isInvincibility = false;
    }

    private IEnumerator SkillGaugeFill()
    {
        float t = 0;
        float gauge = 0;
        while (t < skillDuration)
        {
            t += Time.deltaTime;
            gauge = Mathf.Lerp(maxSkillGauge, 0f, t / skillDuration);

            skillGaugeImage.fillAmount = gauge / maxSkillGauge;

            yield return null;
        }
    }

    public void ShildOn()
    {
        Guard.ActivateShield();
    }

    private Coroutine bomb;

    public void BoomItemOn()
    {
        StartCoroutine(BoomEvent());
    }

    private IEnumerator BoomEvent()
    {
        isBoom = true;
        boomRange.SetActive(true);

        yield return new WaitForSeconds(0.25f);

        boomRange.SetActive(false);
        isBoom = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && atkOn)
        {
            if (!isAttack && !isBoom)
            {
                StartCoroutine(Attack());
            }
        }
    }

    private void GameOver()
    {
        isGameOver = true;
        GameManager.instance.GameOver();
    }

}

