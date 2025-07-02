using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.XR;

public enum CrazyBossState { 
    enter,
    fire,
    special,
    death
}

public class CrazyBossController : MonoBehaviour
{
    [SerializeField] private CrazyBossEnter bossEnter;
    [SerializeField] private CrazyBossFire bossFire;
    [SerializeField] private bool test;
    [SerializeField] private CrazyBossState testState;
    [SerializeField] private CrazyBossSpecial bossSpecial;
    [SerializeField] private CrazyBossDeath bossDeath;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (test)
        {
            ChangeState(testState);
        }
        else {
            ChangeState(CrazyBossState.enter);
        }
    }

    public void ChangeState(CrazyBossState state) {
        switch (state) {
            case CrazyBossState.enter:
                Debug.Log("EnterState");
                bossEnter.RunState();
                break;
            case CrazyBossState.fire:
                Debug.Log("FireState");
                bossFire.RunState();
                break;
            case CrazyBossState.special:
                Debug.Log("SpecialState");
                bossSpecial.RunState();
                break;
            case CrazyBossState.death:
                Debug.Log("DeathState");
                bossEnter.StopState();
                bossFire.StopState();
                bossSpecial.StopState();
                bossDeath.RunState();
                break;
        }
    }

}
