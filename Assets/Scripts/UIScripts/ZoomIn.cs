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
    [SerializeField] CinemachineVirtualCamera cam;
    [SerializeField] CinemachineGroupComposer composer;
    Vector3 position;
    internal GameObject Explosion=null;
    [SerializeField] Transform left;
    [SerializeField] Transform right;
    private LegacyLensSettings originalSettings;

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
            float k = composer.m_MaximumOrthoSize;
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
                composer.m_MaximumOrthoSize = 5000;
                composer.m_MinimumOrthoSize = 6.5f;
                composer.m_GroupFramingSize = 20;
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
            composer.m_MaximumOrthoSize = k;
            if (composer.m_MinimumOrthoSize > 1)
            {
                composer.m_MinimumOrthoSize-=0.3f * Time.deltaTime * 2000;
            }

            if (composer.m_GroupFramingSize > 2)
            {
                composer.m_GroupFramingSize-=0.3f * Time.deltaTime*2000;
            }
        }
    }
    public void Show(Vector3 position)
    {
        originalSettings = cam.m_Lens;
        this.position = position;
        StartZoom = true;
        if (WhoKilled != null)
        {
            WhoKilled.gameObject.SetActive(true);
            targetGroup.AddMember(WhoKilled, 100, 0.4f);
        }
        targetGroup.RemoveMember(left);
        targetGroup.RemoveMember(right);
        composer.m_FrameDamping = 500;
        composer.m_HorizontalDamping = 500;
        composer.m_VerticalDamping = 500;
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
