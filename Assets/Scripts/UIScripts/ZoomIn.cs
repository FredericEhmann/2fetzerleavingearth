using System;
using TMPro;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class ZoomIn : MonoBehaviour
{
    private static ZoomIn instance = null;
    bool StartZoom = false;
    [SerializeField] CinemachineTargetGroup targetGroup;
    [SerializeField] CinemachineCamera cam; // Updated from CinemachineVirtualCamera
    [SerializeField] CinemachineGroupFraming composer; // Updated from CinemachineGroupComposer
    Vector3 position;
    internal GameObject Explosion=null;
    [SerializeField] Transform left;
    [SerializeField] Transform right;
    private LensSettings originalSettings; // Updated from LegacyLensSettings

    internal Transform WhoKilled = null;

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
    public static ZoomIn GetInstance()
    {
        return instance;
    }
    // Update is called once per frame
    void Update()
    {
        if (StartZoom)
        {
            if (Explosion != null) {
                if (targetGroup.Targets.Count == 1)
                {
                    targetGroup.AddMember(Explosion.transform, 100, 0.4f);
                }

            }
            // Accessing OrthoSizeRange: .y for max, .x for min
            float k = composer.OrthoSizeRange.y;
            if (k > 1)
            {
                k -= 0.02f * Time.deltaTime * 3500;
            }
            else
            {
                targetGroup.RemoveMember(Explosion.transform);
                targetGroup.RemoveMember(WhoKilled);
                targetGroup.AddMember(left, 0, 0);
                targetGroup.AddMember(right, 0, 0);
                StartZoom = false;
                Time.timeScale = 1f;
                // Setting OrthoSizeRange and FramingSize
                composer.OrthoSizeRange = new Vector2(6.5f, 5000f);
                composer.FramingSize = 20f; // GroupFramingSize maps to FramingSize
                return;
            }
            if (k > 3)
            {
                k -= 0.05f*Time.deltaTime * 3500;
            }

            if (k > 5)
            {
                k = 5;
            }
            composer.OrthoSizeRange.y = k; // Adjusting max ortho size
            if (composer.OrthoSizeRange.x > 1) // Adjusting min ortho size
            {
                composer.OrthoSizeRange.x -=0.3f * Time.deltaTime * 2000f;
            }

            if (composer.FramingSize > 2) // Adjusting FramingSize
            {
                composer.FramingSize -=0.3f * Time.deltaTime*2000f;
            }
        }
    }
    public void Show(Vector3 position)
    {
        originalSettings = cam.Lens;
        this.position = position;
        StartZoom = true;
        if (WhoKilled != null)
        {
            WhoKilled.gameObject.SetActive(true);
            targetGroup.AddMember(WhoKilled, 100, 0.4f);
        }
        targetGroup.RemoveMember(left);
        targetGroup.RemoveMember(right);
        // FrameDamping, HorizontalDamping, VerticalDamping map to a single Vector3 Damping
        composer.Damping = new Vector3(500f, 500f, 500f);
    }

    public void Hide()
    {
        StartZoom = false;

    }

    private void OnDestroy()
    {
        instance = null;
    }

    private void OnDisable()
    {
        cam.m_Lens = originalSettings;
    }

}
