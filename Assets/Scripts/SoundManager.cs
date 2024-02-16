using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager
{
    public Dictionary<string, AudioClip> sounds;
    public AudioSource soundPlayer;
    
    public void Init()
    {
        soundPlayer = GameObject.Find("SoundPlayer").GetOrAddComponent<AudioSource>();
        
        sounds = new Dictionary<string, AudioClip>();

        // Resources/Sounds 폴더에서 모든 AudioClip을 가져와서 dictionary에 저장합니다.
        AudioClip[] audioClips = Resources.LoadAll<AudioClip>("Sounds");
        foreach (AudioClip clip in audioClips)
        {
            sounds.Add(clip.name, clip);
            Debug.Log(clip.name);
        }
    }
    
    

    public void PlaySound(string soundName)
    {
        // 입력된 이름을 사용하여 해당 음원을 실행합니다.
        if (sounds.ContainsKey(soundName))
        {
            Debug.Log("play sound: " + soundName);
            soundPlayer.PlayOneShot(sounds[soundName], 1);
        }
        else
        {
            Debug.LogWarning("음원 " + soundName + "을(를) 찾을 수 없습니다.");
        }
    }
}