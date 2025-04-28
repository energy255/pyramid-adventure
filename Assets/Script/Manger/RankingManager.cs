using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class RankingManager : Singleton<RankingManager>
{
    public List<RankData> rankDatas = new();
    

    void Start()
    {
        Load();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F8))
        {
            ResetData();
        }
    }

    private static void ResetData()
    {
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.DeleteKey($"RankName{i}");
        }
    }

    public void Regster(string Name, float time)
    {
        rankDatas.Add(new RankData(Name, time));

        Sort();
        Save();
        rankDatas.RemoveAt(5);
    }


    private void Load()
    {
        for (int i = 0; i < 5; i++)
        {
            if (PlayerPrefs.HasKey($"RankName{i}"))
                rankDatas.Add(new RankData(PlayerPrefs.GetString($"RankName{i}"), PlayerPrefs.GetFloat($"RankTimeScore{i}")));
            else
                rankDatas.Add(new RankData("---", 359999));
        }
    }

    private void Save()
    {
        for (int i = 0; i < rankDatas.Count; i++)
        {
            PlayerPrefs.SetString($"RankName{i}", rankDatas[i].Name);
            PlayerPrefs.SetFloat($"RankTimeScore{i}", rankDatas[i].time);
        }
    }

    private void Sort()
    {
        rankDatas = rankDatas.OrderBy(a => a.time).ToList();
    }
}

[System.Serializable]
public class RankData : ScriptableObject
{
    public string Name;
    public float time;

    public RankData(string Name, float time)
    {
        this.Name = Name;
        this.time = time;
    }

}



