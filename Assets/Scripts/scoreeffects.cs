using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreeffects : MonoBehaviour
{
    public GameObject scoreEffectPrefab;

    public void DisplayScore(int score, Vector3 itemPosition)
    {
        // Instantiate a new score effect at the item's position
        GameObject newScoreEffect = Instantiate(scoreEffectPrefab, Vector3.zero, Quaternion.identity);
        scoreeffect scoreEffectScript = newScoreEffect.GetComponent<scoreeffect>();

        // Set the text of the score effect
        scoreEffectScript.scoreEffectText.text = "+" + score.ToString();

        // Set the anchored position of the RectTransform to the world position
        RectTransform rectTransform = scoreEffectScript.GetComponent<RectTransform>();
        Vector2 screenPos = Camera.main.WorldToScreenPoint(itemPosition);
        rectTransform.anchoredPosition = screenPos;

        // Parent the score effect to the object this script is attached to
        newScoreEffect.transform.SetParent(transform);
    }

}

