using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMG : MonoBehaviour
{
    [SerializeField] private ScrollMap[] bgs;
    [SerializeField] private ScrollMap[] tops;
    [SerializeField] private ScrollMap[] bottoms;
    [SerializeField] private Vector2 startPos;
    [SerializeField] private float endX;
    [SerializeField] private Vector2 tstartX;
    [SerializeField] private float tendX;
    [SerializeField] private Vector2 bstartX;
    [SerializeField] private float bendX;
    [SerializeField] private float spd;

    private void Start()
    {
        endX = bgs[0].transform.position.x;
        startPos = bgs[bgs.Length -1].transform.position;

        tendX = tops[0].transform.position.x;
        tstartX = tops[tops.Length -1].transform.position;

        bendX = bottoms[0].transform.position.x;
        bstartX = bottoms[bottoms.Length -1].transform.position;

        foreach (var bg in bgs)
        {
            bg.spd = spd;
            bg.startPos = startPos;
            bg.endX = endX;
        }
        foreach (var top in tops)
        {
            top.spd = spd;
            top.startPos = tstartX;
            top.endX = endX;
        }
        foreach (var bottom in bottoms)
        {
            bottom.spd = spd;
            bottom.startPos = bstartX;
            bottom.endX = endX;
        }
    }

    public void SpdEvent()
    {
        float mSpd = GameManager.instance.speed;
        foreach (var bg in bgs)
        {
            bg.mapSpd = mSpd;
        }

        foreach (var top in tops)
        {
            top.mapSpd = mSpd;
        }

        foreach (var bots in bottoms)
        {
            bots.mapSpd = mSpd;
        }
    }
}


