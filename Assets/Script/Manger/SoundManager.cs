using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource AudioSource;

    public AudioClip PuzzleClearSound;
    public AudioClip GameOverSound;

    public void PuzzleClear()
    {
        AudioSource.PlayOneShot(PuzzleClearSound);
    }
    public void GameOver()
    {
        AudioSource.Stop();
        AudioSource.PlayOneShot(GameOverSound);
    }
    
}
