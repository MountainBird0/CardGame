using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource backgroundMusicSource; // 배경음악을 재생할 AudioSource
    public AudioSource soundEffectSource; // 효과음을 재생할 AudioSource

    public AudioClip backgroundMusicClip; // 배경음악 클립
    public AudioClip clickSound; // 클릭 효과음 클립

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // 배경음악을 설정하고 재생합니다.
        backgroundMusicSource=GetComponent<AudioSource>();
        soundEffectSource = GetComponent<AudioSource>();
        backgroundMusicSource.clip = backgroundMusicClip;
        backgroundMusicSource.Play();
    }

    // 클릭 효과음 재생 메서드
    public void PlayClickSound()
    {
        if (clickSound != null)
        {
            soundEffectSource.clip = clickSound;
            soundEffectSource.Play();
        }
    }
}
