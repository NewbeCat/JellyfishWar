using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreItem : Item
{
    [SerializeField] private int scoreValue = 500;

    protected override void ItemEvent()
    {
        GameManager.instance.Score += scoreValue;
        scoreEffects.DisplayScore(scoreValue, transform.position);
        base.ItemEvent();
    }
}

