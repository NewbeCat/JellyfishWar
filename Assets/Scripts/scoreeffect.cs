using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class scoreeffect : MonoBehaviour
{
    public TextMeshProUGUI scoreEffectText;
    public float fadeInTime = 0.3f;
    public float showDuration = 0.7f;
    public float fadeOutTime = 0.3f;

    void Start()
    {
        StartCoroutine(ShowScoreEffect());
    }

    IEnumerator ShowScoreEffect()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeInTime)
        {
            float alpha = Mathf.Lerp(0f, 170f / 256f, elapsedTime / fadeInTime);
            scoreEffectText.color = new Color(scoreEffectText.color.r, scoreEffectText.color.g, scoreEffectText.color.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Pause for a moment
        yield return new WaitForSeconds(showDuration);

        // Fade out to the right
        elapsedTime = 0f;
        while (elapsedTime < fadeOutTime)
        {
            float alpha = Mathf.Lerp(170f / 256f, 0f, elapsedTime / fadeOutTime);
            scoreEffectText.color = new Color(scoreEffectText.color.r, scoreEffectText.color.g, scoreEffectText.color.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
