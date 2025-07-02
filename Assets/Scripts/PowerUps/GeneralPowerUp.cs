using UnityEngine;

public class GeneralPowerUp : MonoBehaviour

{


    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    

}
