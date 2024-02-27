using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;


    [SerializeField]
    [Tooltip("You should specify the sound effect prefab!!!")]
    GameObject soundEffectPrefab;


    [Header("Audio Clips")]
    [SerializeField] AudioClip attack;
    [SerializeField] AudioClip damage;
    [SerializeField] AudioClip music;

    public enum SoundType
    {
        ATTACK = 0,
        DAMAGE = 1,
        MUSIC = 3
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        instance = this;
    }

    public static void PlaySound(SoundType s)
    {
        instance.privPlaySound(s);
    }

    private void privPlaySound(SoundType s)
    {


        //PlayOneShot will let you use a single audiosource to play many sound effects
        //but you lose the ability to Pause,Rewind, mute, etc. any one of those sounds you played
        /*switch(s)
        {
            case SoundType.ATTACK: audio.PlayOneShot(attack); break;
            case SoundType.DAMAGE: audio.PlayOneShot(damage); break;
            case SoundType.MUSIC: audio.PlayOneShot(music); break;
        }*/

        //An alternative approach
        /*switch (s)
        {
            case SoundType.ATTACK: audio.clip = attack; break;
            case SoundType.DAMAGE: audio.clip = damage; break;
            case SoundType.MUSIC: audio.clip = music; break;
        }*/

        //audio.Play();

        AudioClip clip = null;

        switch (s)
        {
            case SoundType.ATTACK: clip = attack; break;
            case SoundType.DAMAGE: clip = damage; break;
            case SoundType.MUSIC: clip = music; break;
        }

        GameObject soundEffectObject = Instantiate(soundEffectPrefab);
        SoundEffect soundEffect = soundEffectObject.GetComponent<SoundEffect>();
        soundEffect.Init(clip);
        soundEffect.Play();
        //soundEffect.SetVolume()
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)) privPlaySound(SoundType.MUSIC);
        if (Input.GetKeyDown(KeyCode.S)) privPlaySound(SoundType.ATTACK);
    }
}
