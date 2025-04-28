using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class ClearChest : Interaction
{
    public Animator animator;


    protected override void Update()
    {
        base.Update();

        if (!onInput || PlayerController.instance.InteractionObject != gameObject) return;

        Open();
    }

    private void Open()
    {
        animator.SetTrigger("Open");
    }

    public void Clear()
    {
        Time.timeScale = 0f;
        PlayManager.instance.StageClearPanel.SetActive(true);
        GameManger.instance.Clear = true;
    }
}
