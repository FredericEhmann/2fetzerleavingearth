using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float parallaxSpeed;
    [SerializeField] private List<SpriteRenderer> spriteRenderers;
    private float spriteHeight;
    private Vector3 startPos;
    public static Color color;


    void Start()
    {
        color = Constants.GetThisLevelsBackgroundColor();
        if (Constants.isSurvivalMode()) {
            color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }
        GetComponent<SpriteRenderer>().color = color;
        for (int i = 0; i < spriteRenderers.Count; i++)
        {
            spriteRenderers[i].color = color;
        }
        startPos = transform.position;
        spriteHeight = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    private void Update()
    {
        transform.Translate(Vector3.down*parallaxSpeed*Time.deltaTime);
        if (transform.position.y < startPos.y - spriteHeight) {
            transform.position = startPos;
        }
        if (Constants.isSurvivalMode())
        {
            color.b = Random.Range(Mathf.Max(0, color.b - 0.0004f), Mathf.Min(color.b + 0.0004f, 1));
            color.r = Random.Range(Mathf.Max(0, color.r - 0.0004f), Mathf.Min(color.r + 0.0004f, 1));
            color.g = Random.Range(Mathf.Max(0, color.g - 0.0004f), Mathf.Min(color.g + 0.0004f, 1));
            GetComponent<SpriteRenderer>().color = color;
            for (int i = 0; i < spriteRenderers.Count; i++)
            {
                spriteRenderers[i].color = color;
            }
        }
    }

}
