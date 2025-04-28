using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : Singleton<Inventory>
{
    public List<Image> ItemSlots = new();
    public List<ItemType> Bagitems = new();
    public Image Bag;
    public Text BagWeight;
    public Sprite nullimage;
    public GameObject selcet;
    public float TotalPrice;
    public float CurrentWeight;
    public float MaxWeight;
    public int MaxSlotCount;
    private int selectIndex = -1;
    public bool OnDebuff;
    
    void Start()
    {
        MaxSlotCount = (int)(4 + (2 * GameManger.instance.BagLevel));
        MaxWeight = MaxWeight + (150 * GameManger.instance.BagLevel);
        AddItem(null);
    }

    void Update()
    {
        Select();
        UI();
        OnDebuff = (CurrentWeight / MaxWeight) >= 1;
    }

    private void UI()
    {
        Bag.fillAmount = CurrentWeight / MaxWeight;
        BagWeight.text = $"{CurrentWeight}/{MaxWeight}";
    }

    private void Select()
    {
        selcet.SetActive(selectIndex != -1);

        if (Input.GetKeyDown(KeyCode.E) && selectIndex < MaxSlotCount - 1) selectIndex++;
        if (Input.GetKeyDown(KeyCode.Q) && selectIndex > -1) selectIndex--;

        if (selectIndex == -1) return;

        selcet.transform.position = ItemSlots[selectIndex].transform.position;

        if (Input.GetKeyDown(KeyCode.Return) && Bagitems.Count > selectIndex)
        {
            Bagitems[selectIndex].Use();
            if (Bagitems[selectIndex].item != Item.gold) Bagitems.RemoveAt(selectIndex);
            selectIndex = -1;
            AddItem(null);
        }
    }


    public void AddItem(ItemType item)
    {
        float price = 0;
        float weight = 0;

        if (MaxSlotCount <= Bagitems.Count) return;

        if (item != null) Bagitems.Add(item);

        for (int i = 0; i < MaxSlotCount; i++)
        {
            ItemSlots[i].sprite = Bagitems.Count <= i ? nullimage : Bagitems[i].Image;
            if (Bagitems.Count > i)
            {
                price += Bagitems[i].price;
                weight += Bagitems[i].weight;
            }
        }

        TotalPrice= price;
        CurrentWeight = weight;
    }
}
