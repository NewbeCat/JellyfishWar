using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomItem : Item
{
    [SerializeField] private int scoreValue = 1000;

    protected override void ItemEvent()
    {
        GameManager.instance.Score += scoreValue;
        scoreEffects.DisplayScore(scoreValue, transform.position);
        GameManager.instance.player.BoomItemOn();
        base.ItemEvent();
    }

}
