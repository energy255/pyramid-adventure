using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingUI : MonoBehaviour
{
    public List<Text> Name = new();
    public List<Text> Score = new();

    public InputField InputField;
    public Button Regster;
    public Button Back;

    public bool OnRegster;


    void Start()
    {
        Regster.onClick.AddListener(() => RegsterSetting());
        Back.onClick.AddListener(() => Totitle());
        GameManger.instance.Playing = false;
    }

    private void Update()
    {
        UILoad();
    }

    private void Totitle()
    {
        FadeInOut.instance.FadeIn("Title");
        GameManger.instance.TimeScore = 0;
        GameManger.instance.Clear = false;
    }

    private void RegsterSetting()
    {
        if (!GameManger.instance.Clear || OnRegster) return;

        OnRegster=true;
        RankingManager.instance.Regster(InputField.text, GameManger.instance.TimeScore);
    }

    private void UILoad()
    {
        for (int i = 0; i < RankingManager.instance.rankDatas.Count; i++)
        {
            Name[i].text = RankingManager.instance.rankDatas[i].Name;
            float second = RankingManager.instance.rankDatas[i].time;
            Score[i].text = $"{Mathf.FloorToInt(second / 3600):D2}:{Mathf.FloorToInt(second / 60 % 60):D2}:{Mathf.FloorToInt(second % 60):D2}";
        }
    }
}
