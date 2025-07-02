using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.XR;

public enum BossState { 
    enter,
    fire,
    special,
    death
}

public class BossController : MonoBehaviour
{
    [SerializeField] private BossEnter bossEnter;
    [SerializeField] private BossFire bossFire;
    [SerializeField] private bool test;
    [SerializeField] private BossState testState;
    [SerializeField] private BossSpecial bossSpecial;
    [SerializeField] private BossDeath bossDeath;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (test)
        {
            ChangeState(testState);
        }
        else {
            ChangeState(BossState.enter);
        }
    }

    public void ChangeState(BossState state) {
        switch (state) {
            case BossState.enter:
                Debug.Log("EnterState");
                bossEnter.RunState();
                break;
            case BossState.fire:
                Debug.Log("FireState");
                bossFire.RunState();
                break;
            case BossState.special:
                Debug.Log("SpecialState");
                bossSpecial.RunState();
                break;
            case BossState.death:
                Debug.Log("DeathState");
                bossEnter.StopState();
                bossFire.StopState();
                bossSpecial.StopState();
                bossDeath.RunState();
                break;
        }
    }

}
