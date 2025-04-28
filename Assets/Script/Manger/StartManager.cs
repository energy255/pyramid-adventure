using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartManager : Singleton<StartManager>
{
    public Button start;
    public Button shop;
    public Button shopBack;
    public Button help;
    public Button helpBack;
    public Button ranking;
    public Button gameExit;

    public GameObject FreeBuyText;
    public GameObject ShopPanel;
    public GameObject HelpPanel;
    public Text MyMoney;

    public Button[] Oxygen;
    public Button[] Bag;
    public float[] OxygenPrice;
    public float[] BagPrice;



    void Start()
    {
        start.onClick.AddListener(() => { FadeInOut.instance.FadeIn("Stage1"); GameManger.instance.Playing = true; });
        shop.onClick.AddListener(() => { ShopPanel.SetActive(true); });
        shopBack.onClick.AddListener(() => { ShopPanel.SetActive(false); });
        help.onClick.AddListener(() => { HelpPanel.SetActive(true); });
        helpBack.onClick.AddListener(() => { HelpPanel.SetActive(false); });
        ranking.onClick.AddListener(() => { FadeInOut.instance.FadeIn("Ranking"); });
        gameExit.onClick.AddListener(() => { Save(); Application.Quit(); });
        Shop();
    }

    private void Shop()
    {
        Oxygen[0].onClick.AddListener(() => { OxygenBuy(OxygenPrice, 0); });
        Oxygen[1].onClick.AddListener(() => { OxygenBuy(OxygenPrice, 1); });
        Oxygen[2].onClick.AddListener(() => { OxygenBuy(OxygenPrice, 2); });

        Bag[0].onClick.AddListener(() => { BagBuy(BagPrice, 0); });
        Bag[1].onClick.AddListener(() => { BagBuy(BagPrice, 1); });
    }

    void Update()
    {
        FreeBuyText.SetActive(GameManger.instance.FreeBuy);
        MyMoney.text = $"Money:{GameManger.instance.money}";
    }

    private void OxygenBuy(float[] price, int index)
    {
        int Level = index + 1;

        if (GameManger.instance.FreeBuy && Level > GameManger.instance.OxygenLevel)
        {
            GameManger.instance.OxygenLevel = Level;

        }
        else if (GameManger.instance.money >= price[index] && Level > GameManger.instance.OxygenLevel)
        {
            GameManger.instance.money -= price[index];
            GameManger.instance.OxygenLevel = Level;
        }

        Save();
    }

    private void BagBuy(float[] price, int index)
    {
        int Level = index + 1;

        if (GameManger.instance.FreeBuy && Level > GameManger.instance.BagLevel)
        {
            GameManger.instance.BagLevel = Level;
        }
        else if (GameManger.instance.money >= price[index] && Level > GameManger.instance.BagLevel)
        {
            GameManger.instance.money -= price[index];
            GameManger.instance.BagLevel = Level;
        }

        Save();
    }


    private void Save()
    {
        PlayerPrefs.SetFloat("BagLevel", GameManger.instance.BagLevel);
        PlayerPrefs.SetFloat("OxygenLevel", GameManger.instance.OxygenLevel);
        PlayerPrefs.SetFloat("Money", GameManger.instance.money);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("BagLevel")) GameManger.instance.BagLevel = PlayerPrefs.GetFloat("BagLevel");
        if (PlayerPrefs.HasKey("OxygenLevel")) GameManger.instance.OxygenLevel = PlayerPrefs.GetFloat("OxygenLevel");
        if (PlayerPrefs.HasKey("Money")) GameManger.instance.money = PlayerPrefs.GetFloat("Money");
    }
}
