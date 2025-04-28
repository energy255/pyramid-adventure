using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleWall : ClearNoSpawn
{
    public PuzzleButton PuzzleButton;
    public Animator Animator;

    void Update()
    {
        if (PuzzleButton.isClear && !IsClear)
        {
            IsClear = true;
            Animator.SetTrigger("Clear");
            CameraShake.instance.SetUp(2f, 0.03f);
            SoundManager.instance.PuzzleClear();
        }
    }
}
