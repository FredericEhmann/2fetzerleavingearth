using System;
using NUnit.Framework.Interfaces;
using UnityEngine;

public class CrazyBossDeath : CrazyBossBaseState
{

    public override void RunState()
    {
        try
        {
            WinCondition.GetInstance()?.RemoveEnemy(this.gameObject);
            Destroy(GetComponent<CrazyBossEnter>().s1.gameObject);
            Destroy(GetComponent<CrazyBossEnter>().s2.gameObject);
            Destroy(GetComponent<CrazyBossEnter>().s3.gameObject);
            Destroy(GetComponent<CrazyBossEnter>().s4.gameObject);
            Destroy(GetComponent<CrazyBossEnter>().s5.gameObject);
            Destroy(GetComponent<CrazyBossEnter>().lineRenderer.gameObject);
            Destroy(GetComponent<CrazyBossEnter>().lineRenderer2.gameObject);
        }
        catch (Exception e)
        {

        }
        gameObject.SetActive(false);
    }
  
}
