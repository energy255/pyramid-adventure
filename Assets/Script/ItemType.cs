using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Item
{
    gold,
    hp,
    O2,
    speedSmall,
    speedBig,
    hide,
    find
}


[CreateAssetMenu]
public class ItemType : ScriptableObject
{
    public Sprite Image;
    public Item item;
    public float price;
    public float weight;

    public void Use()
    {
        switch (item)
        {
            case Item.gold:
                break;
            case Item.hp:
                PlayerController.instance.Hp += PlayerController.instance.MaxHp / 4;
                break;
            case Item.O2:
                PlayerController.instance.O2 += PlayerController.instance.MaxO2 / 3;
                break;
            case Item.speedSmall:
                PlayerController.instance.StartCoroutine(PlayerController.instance.SpeedUp(0.5f, 3));
                break;
            case Item.speedBig:
                PlayerController.instance.StartCoroutine(PlayerController.instance.SpeedUp(1f, 5));
                break;
            case Item.hide:
                PlayerController.instance.StartCoroutine(PlayerController.instance.OnHide(5));
                break;
            case Item.find:
                PlayerController.instance.FindSetting();
                break;
        }
    }
}
