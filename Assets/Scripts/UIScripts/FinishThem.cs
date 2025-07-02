using TMPro;
using UnityEngine;

public class FinishThem : MonoBehaviour
{
    private static FinishThem instance = null;
    [SerializeField] AudioSource finishThemSound = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static FinishThem GetInstance() {
        return instance;
    }
    // Update is called once per frame
    void Update()
    {
        if (GetComponent<TextMeshProUGUI>().alpha > 0.01f)
        {
            GetComponent<TextMeshProUGUI>().fontSize = Random.Range(95, 105);
            GetComponent<TextMeshProUGUI>().alpha += Random.Range(-0.004f, +0.001f);
        }
        else {
            GetComponent<TextMeshProUGUI>().alpha = 0;
        }
    }

    public void Show()
    {
        if (!EndGameManager.getInstance().gameOver)
        {
            GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.8f);
            finishThemSound.Play();
        }
    }

    public void Hide() {
        GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 0);

    }
    private void OnDestroy()
    {
        instance = null;
    }
}
