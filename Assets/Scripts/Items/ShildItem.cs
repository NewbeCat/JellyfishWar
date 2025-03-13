using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShildItem : Item
{
    [SerializeField] private int scoreValue = 700;

    protected override void ItemEvent()
    {
        GameManager.instance.Score += scoreValue;
        scoreEffects.DisplayScore(scoreValue, transform.position);
        GameManager.instance.player.ShildOn();
        base.ItemEvent();
    }
}
