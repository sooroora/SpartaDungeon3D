using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

// 옛날에 만든거
// string으로 찾아야해서 불편함. 나중에 enum 같은걸로 바꿔야함!!
public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    
    public float MasterVolume
    {
        get => _masterVolume;
        set => _masterVolume = value;
    }

    public float SfxVolume
    {
        get => _sfxVolume;
        private set => _sfxVolume = value;
    }

    public float BgmVolume
    {
        get => _bgmVolume;
        private set => _bgmVolume = value;
    }

    private float _sfxVolume = 1.0f;
    private float _bgmVolume = 1.0f;
    private float _masterVolume = 1.0f;


    public static SoundManager Instance
    {
        get => instance;
        private set => instance = value;
    }

    Dictionary<ESfxName, AudioClipGroup> SFXTable = new Dictionary<ESfxName, AudioClipGroup>();
    Dictionary<EBgmName, AudioClipGroup> BGMTable = new Dictionary<EBgmName, AudioClipGroup>();

    AudioSource bgmAudioSource;
    private List<AudioSource> sfxAudioSources;


    private void Awake()
    {
        if (instance == null)
        {
            gameObject.name = "SoundManager";
            DontDestroyOnLoad(gameObject);
            instance = this;

            bgmAudioSource  = gameObject.AddComponent<AudioSource>();
            sfxAudioSources = new List<AudioSource>();

            // 일단 풀에 10개 넣어
            for (int i = 0; i < 10; ++i)
            {
                AudioSource aSource = gameObject.AddComponent<AudioSource>();
                aSource.playOnAwake = false;
                sfxAudioSources.Add(aSource);
            }

            instance.ReadSoundClips();
            SetSavedVolume();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void ReadSoundClips()
    {
        AudioClipGroup[] bgmClips = Resources.LoadAll<AudioClipGroup>("Sounds/BGM");
        AudioClipGroup[] sfxClips = Resources.LoadAll<AudioClipGroup>("Sounds/SFX");

        SFXTable = ((ESfxName[])Enum.GetValues(typeof(ESfxName))).ToDictionary(part => part,
            part => (AudioClipGroup)null);

        BGMTable = ((EBgmName[])Enum.GetValues(typeof(EBgmName))).ToDictionary(part => part,
            part => (AudioClipGroup)null);

        for (int i = 0; i < bgmClips.Length; i++)
        {
            if (Enum.TryParse(bgmClips[i].name, out EBgmName bgmName))
            {
                BGMTable[bgmName] = bgmClips[i];
            }
        }

        for (int i = 0; i < sfxClips.Length; i++)
        {
            if (Enum.TryParse(sfxClips[i].name, ignoreCase: true, out ESfxName sfxName))
            {
                SFXTable[sfxName] = sfxClips[i];
            }
        }

        // 오마갓 C# 버전이 안맞대!!!!!!!!
        // 프로젝트 세팅을 바꿔야 한대
        //Enum.GetValues<SoundName>().ToDictionary(part => part, part => null);
    }

    private void Update()
    {
        //테스트
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            PlaySfxRandom(ESfxName.Click, Random.Range(0.1f, 1f));
        }

        if (Input.GetKeyDown(KeyCode.Equals))
        {
            AllSfxStop();
        }
    }

    public void PlaySfxRandom(ESfxName sfxName, float volume = 1.0f)
    {
        AudioClip aClip = null;
        if (SFXTable.TryGetValue(sfxName, out AudioClipGroup clip))
        {
            aClip = clip.GetRandomClip();
        }

        PlaySfxOnce(aClip, volume);
    }
    
    
    public void PlaySfxOnce(ESfxName sfxName, float volume = 1.0f, int idx = 0)
    {
        AudioClip aClip = null;
        if (SFXTable.TryGetValue(sfxName, out AudioClipGroup clip))
        {
            aClip = clip.GetClip(idx);
        }

        PlaySfxOnce(aClip, volume);
    }

    void PlaySfxOnce(AudioClip aClip, float _volume = 1.0f)
    {
        if (aClip != null)
        {
            AudioSource audioSource = GetSFXAudioSource();
            if(audioSource == null)
                return;
            
            audioSource.Stop();
            audioSource.volume = GetVolume(SfxVolume, _volume);
            audioSource.clip   = aClip;
            audioSource.Play();
        }
    }

    public void PlayBgm(EBgmName bgmName, float _volume = 1.0f, bool _loop = true, float pitch = 1.0f)
    {
        AudioClip aClip = null;
        if (BGMTable.TryGetValue(bgmName, out AudioClipGroup clip))
        {
            aClip = clip.GetClip();
        }

        if (aClip != null)
        {
            bgmAudioSource.Stop();
            bgmAudioSource.volume = GetVolume(BgmVolume,_volume);
            bgmAudioSource.loop   = _loop;
            bgmAudioSource.clip   = aClip;
            bgmAudioSource.pitch = pitch;
            bgmAudioSource.Play();
        }
    }

    public void SetNowBgmPitch(float pitch)
    {
        bgmAudioSource.pitch = pitch;
    }

    public void AllStop()
    {
        BgmStop();
        AllSfxStop();
    }

    public void BgmStop()
    {
        bgmAudioSource.Stop();
    }

    public void AllSfxStop()
    {
        foreach (AudioSource aSource in sfxAudioSources)
        {
            aSource.Stop();
        }
    }

    AudioSource GetSFXAudioSource()
    {
        AudioSource shortestSource     = null;
        float       shortestRemainTime = float.MaxValue;
        
        foreach (AudioSource audioSource in sfxAudioSources)
        {
            if (audioSource.isPlaying == false)
            {
                return audioSource;
            }
            
            // 제일 짧게 남은거 끊고 재생하는 방식
             
            float remainTime = audioSource.clip.length - audioSource.time;
            if (remainTime <= shortestRemainTime)
            {
                shortestRemainTime = remainTime;
                shortestSource = audioSource;
            }
        }

        return shortestSource;
        
        // 스킵하지 않고 audioSource 추가하는 버전
        // AudioSource newAudioSource = gameObject.AddComponent<AudioSource>();
        // sfxAudioSources.Add(newAudioSource);
        // return newAudioSource;
    }

    void SetBgmVolume()
    {
        bgmAudioSource.volume = GetVolume(BgmVolume, 1.0f);
    }

    void SetSavedVolume()
    {
        MasterVolume = PlayerPrefs.GetFloat("MasterVolume", 1.0f); 
        SfxVolume = PlayerPrefs.GetFloat("SfxVolume", 1.0f);
        BgmVolume = PlayerPrefs.GetFloat("BGMVolume", 1.0f);
        
        
    }
    
    float GetVolume(float settingVolume, float customVolume)
    {
        return MasterVolume * settingVolume * customVolume;
    }

    public void SetMasterVolume(float volume)
    {
        MasterVolume = Mathf.Clamp(volume,0,1.0f);
        PlayerPrefs.SetFloat("MasterVolume", MasterVolume);
        SetBgmVolume();
    }

    public void SetSfxVolume(float volume)
    {
        SfxVolume = Mathf.Clamp(volume,0,1.0f);
        PlayerPrefs.SetFloat("SfxVolume", MasterVolume);
    }

    public void SetBgmVolume(float volume)
    {
        BgmVolume = Mathf.Clamp(volume,0,1.0f);
        PlayerPrefs.SetFloat("BgmVolume", MasterVolume);
        SetBgmVolume();
    }
}