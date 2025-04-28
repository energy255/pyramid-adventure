using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Chest : Interaction
{
    public List<ItemType> Items = new();
    public Animator animator;
    public bool isOpen;


    protected override void Update()
    {
        if (!isOpen) base.Update();

        if (!onInput || PlayerController.instance.InteractionObject != gameObject) return;

        if (!isOpen) Open();
    }

    private void Open()
    {
        isOpen = true;
        animator.SetTrigger("Open");

        int index = Random.Range(0, Items.Count);
        Inventory.instance.AddItem(Items[index]);

        GetComponent<BoxCollider>().enabled = false;
        InteractionKey.SetActive(false);

        if (PlayerController.instance.Objective == gameObject) PlayerController.instance.FindArrowPart.SetActive(false);
    }
}
