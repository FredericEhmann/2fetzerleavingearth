using NUnit.Framework.Interfaces;
using UnityEngine;

public class BossDeath : BossBaseState
{

    public override void RunState()
    {
        EndGameManager.getInstance().StopBossMusic();
        if (EndGameManager.getInstance().possibleWin)
        {
            EndGameManager.getInstance().StartResolveSequence();
        }
        gameObject.SetActive(false);
    }
  
}
