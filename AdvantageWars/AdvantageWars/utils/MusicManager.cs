using SFML.Audio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


public class MusicManager
{
    private readonly string defaultMusic = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "resources" + Path.DirectorySeparatorChar + "sounds" + Path.DirectorySeparatorChar + "MasterVertex.ogg";
    private List<Music> music;
    private int currentSong;

    private static MusicManager instance;

    private MusicManager()
    {
        music = new List<Music>();
        currentSong = 0;
        Music m = new Music(defaultMusic);
        m.Loop = true;
        music.Add(m);
        SetVolume(3);
    }

    public void SetVolume(int newVolume)
    {
        for (int i = 0; i < music.Count; i++)
        {
            music[i].Volume = newVolume;
        }
    }

    public void Play()
    {
        music[currentSong].Play();
    }

    public static MusicManager GetInstance()
    {
        if (instance == null)
        {
            instance = new MusicManager();
        }
        return instance;
    }
}
