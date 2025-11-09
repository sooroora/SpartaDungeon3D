using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraEffectController : MonoBehaviour
{
    private PostProcessVolume volume;
    Coroutine hitIndicatorCoroutine;
    private Vignette vignette;


    private void Awake()
    {
        volume = GetComponentInChildren<PostProcessVolume>();
        volume.profile.TryGetSettings(out vignette);
    }

    public void ShowHitIndicator()
    {
        StopHitIndicator();
        hitIndicatorCoroutine = StartCoroutine(HitIndicatorCoroutine());
    }

    public void StopHitIndicator()
    {
        if (hitIndicatorCoroutine != null)
            StopCoroutine(hitIndicatorCoroutine);
    }

    IEnumerator HitIndicatorCoroutine()
    {
        float effectSpeed = 1.0f;
        bool isIncrease = true;
        vignette.active = true;

        while (vignette.active)
        {
            if (isIncrease)
            {
                vignette.intensity.value += (Time.deltaTime * effectSpeed);
                if (vignette.intensity.value >= 0.3f)
                    isIncrease = false;
            }
            else
            {
                vignette.intensity.value -= (Time.deltaTime * effectSpeed);
                if (vignette.intensity.value <= 0.0f)
                {
                    vignette.active = false;
                }
            }
            yield return null;
        }
    }
}
