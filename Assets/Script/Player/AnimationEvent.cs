using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
  
    public void Dead()
    {
        PlayerController.instance.DeadUI.SetActive(true);
        SoundManager.instance.GameOver();
        Time.timeScale = 0;
    }

    public void StartSlash()
    {
        PlayerController.instance.SlashPart.SetActive(true);
    }

    public void EndSlash()
    {
        PlayerController.instance.SlashPart.SetActive(false);
    }
}
