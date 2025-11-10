using System;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float time;

    public float fullDayLength;
    public float startTime = 0.4f;
    private float timeRate;
    public Vector3 noon;

    [Header("Sun")]
    public Light sun;

    public AnimationCurve sunIntensity;

    [Header("Moon")]
    public Light moon;

    public AnimationCurve moonIntensity;

    // [Header("Other Lighting")]
    // public AnimationCurve lightingIntensityMultiplier;
    // public AnimationCurve reflectionIntensityMultiplier;

    private Material skyBox;

    private void Awake()
    {
        skyBox = RenderSettings.skybox;
    }

    private void Start()
    {
        timeRate = 1.0f / fullDayLength;
        time = startTime;
    }

    private void Update()
    {
        time = (time + timeRate * Time.deltaTime) % 1.0f;

        float skyBoxBlend = (time <= 0.5f) ? (time / 0.5f) : ((1f - time) / 0.5f);
        skyBox.SetFloat("_Blend", skyBoxBlend);

        // 그림자 때문에 로테이션 하긴 해야함!!
        sun.intensity = 1.0f - (skyBoxBlend);
        
        
        //
        // UpdateLighting(sun, sunColor, sunIntensity);
        // UpdateLighting(moon, moonColor, moonIntensity);
        //
        // RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(time);
        // RenderSettings.reflectionIntensity = reflectionIntensityMultiplier.Evaluate(time);

    }

    void UpdateLighting(Light lightSource, Gradient colorGradiant, AnimationCurve intensityCurve)
    {
        float intensity = intensityCurve.Evaluate(time);

        // lightSource.transform.eulerAngles = (time - (lightSource == sun ? 0.25f : 0.75f)) * noon * 4.0f;
        lightSource.color = colorGradiant.Evaluate(time);
        lightSource.intensity = intensity;

        GameObject go = lightSource.gameObject;
        if (lightSource.intensity == 0 && go.activeInHierarchy)
            go.SetActive(false);
        else if (lightSource.intensity > 0 && !go.activeInHierarchy)
            go.SetActive(true);
    }
}
