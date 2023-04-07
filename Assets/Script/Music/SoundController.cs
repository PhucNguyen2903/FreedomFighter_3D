
using System.Collections.Generic;
using UnityEngine;


public class SoundController : MonoBehaviour
{
    private static SoundController instance;
    public static SoundController Instance => instance;

    [SerializeField] private List<Sounds> sounds;
    [SerializeField] private List<Sounds> musics;

    public float SoundVolume;
    public float MusicVolume;

    private void Awake()
    {
        SoundController.instance = this;

        for (int i = 0; i < sounds.Count; i++)
        {
            sounds[i].audio = gameObject.AddComponent<AudioSource>();
            sounds[i].audio.clip = sounds[i].clip;
            sounds[i].audio.priority = 1;
            sounds[i].audio.playOnAwake = false;
            sounds[i].audio.volume = sounds[i].volume;
        }

        for (int i = 0; i < musics.Count; i++)
        {
            musics[i].audio = gameObject.AddComponent<AudioSource>();
            musics[i].audio.clip = musics[i].clip;
            musics[i].audio.playOnAwake = false;
            musics[i].audio.volume = musics[i].volume;
        }
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        SoundVolume = PlayerPrefs.GetFloat("soundvolume", 0.5f);
        MusicVolume = PlayerPrefs.GetFloat("musicvolume", 0.5f);

        if (PlayerPrefs.GetInt("SoundOn", 1) == 1)
        {
            SoundUpdate(false);// Mute is false
        }
        else
        {
            SoundUpdate(true); // Mute is true
        }

        if (PlayerPrefs.GetInt("MusicOn", 1) == 1)
        {
            MusicUpdate(false);
        }
        else
        {
            MusicUpdate(true);
        }

        PlaySound(SoundName.Login,true);

    }

    public void PlaySound(SoundName name, bool loop)
    {
        Sounds sound = sounds.Find(x => x.name == name);
        sound.audio.volume = SoundVolume;
        sound.audio.loop = loop;
        sound?.audio.Play();
    }

    public void PlayMusic(SoundName name, bool loop = false)
    {
        if (!IsMusic())
        {
            return;
        }

        Sounds music = musics.Find(x => x.name == name);
        if (music != null)
        {
            music.audio.priority = 128;
            music.audio.loop = loop;
            music.audio.volume = MusicVolume;
            music.audio.Play();
        }
    }

    public void StopSound(SoundName name)
    {
        Sounds sound = sounds.Find(x => x.name == name);
        sound?.audio.Stop();
    }

    public void StopMusic(SoundName name)
    {
        Sounds music = musics.Find(x => x.name == name);
        music?.audio.Stop();
    }

    public void SoundUpdate(bool mute)
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            sounds[i].audio.mute = mute;
        }
    }

    public void MusicUpdate(bool mute)
    {
        for (int i = 0; i < musics.Count; i++)
        {
            musics[i].audio.mute = mute;
        }
    }
    public void MusicVolumeUpdate()
    {
        for (int i = 0; i < musics.Count; i++)
        {
            musics[i].audio.volume = MusicVolume;
        }
    }

    public void SoundVolumeUpdate()
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            sounds[i].audio.volume = SoundVolume;
        }
    }
    public void SoundVolumeUpdate(float SoundvolumeValue)
    {
        SoundVolume = SoundvolumeValue;
        for (int i = 0; i < sounds.Count; i++)
        {
            sounds[i].audio.volume = SoundVolume;
        }
    }



    public bool IsSound()
    {
        return true;
    }

    public bool IsMusic()
    {
        return true;
    }


}

[System.Serializable]
public class Sounds
{
    public SoundName name;
    [Range(0, 1)]
    public float volume = 1;
    public AudioClip clip;
    [HideInInspector] public AudioSource audio;
}

public enum SoundName
{
   Login, Lobby, InRoom, InGame
}