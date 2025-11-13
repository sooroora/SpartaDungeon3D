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
    public Color dayAmbientColor;

    [Header("Moon")]
    public Light moon;

    public AnimationCurve moonIntensity;
    public Color nightAmbientColor;

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

        float angleX = 0;
        float angleY = 0;

        if (time <= 0.5f)
        {
            RenderSettings.ambientLight = Color.Lerp(dayAmbientColor, nightAmbientColor, time / 0.5f);

            float t = time / 0.5f;
            sun.intensity = Mathf.Lerp(1.0f, 0.0f, t*1.2f);
            
            angleX = Mathf.Lerp(90f, -90f, t);
            angleY = Mathf.Lerp(0, 180, t);
        }
        else
        {
            RenderSettings.ambientLight = Color.Lerp(nightAmbientColor, dayAmbientColor, (time - 0.5f) / 0.5f);

            float t = (time - 0.5f) / 0.5f;
            
            sun.intensity = Mathf.Lerp(0f, 1.0f, t);
            
            angleX = Mathf.Lerp(-90f, 90f, t);
            angleY = Mathf.Lerp(180f, 360f, t);
        }

        sun.transform.rotation = Quaternion.Euler(angleX, angleY, 0f);
    }
}
