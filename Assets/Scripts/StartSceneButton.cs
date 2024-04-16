using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void Str()
    {
        // Scene을 로드하기 전에 AudioManager 인스턴스를 찾습니다.
        AudioManager audioManager = FindObjectOfType<AudioManager>();

        // AudioManager 인스턴스를 찾은 경우
        if (audioManager != null)
        {
            // AudioManager에서 오디오를 재생합니다.
            audioManager.PlayClickSound();
        }

        SceneManager.LoadScene("StartScene");
    }
}
