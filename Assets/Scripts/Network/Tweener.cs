using UnityEngine;

public class Tweener : MonoBehaviour
{
    [SerializeField] float delay;
    private void OnEnable()
    {
        LeanTween.scale(gameObject, new Vector3(0.01f, 0.01f, 0.01f), 0.001f);
        LeanTween.scale(gameObject, new Vector3(0.05f, 0.05f, 0.05f), 0.4f)
        .setDelay(delay)
        .setEase(LeanTweenType.easeInOutCirc)
        .setLoopPingPong();
    }
}
