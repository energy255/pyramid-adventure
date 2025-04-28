using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class PlayManager : Singleton<PlayManager>
{
    public Button Yes;
    public Button No;
    public Button ToTitle;
    public Button DeadToTitle;
    public Button StageClear;

    public GameObject ToTitlePanel;
    public GameObject TotalPricePanel;
    public GameObject StageClearPanel;

    public Text TotalPrice;
    public Text ClearTotalPrice;

    public List<GameObject> FindChest = new();
    public List<GameObject> ResetPosObjects = new();
    public List<Vector3> ResetPos = new();

    public string NextScnenName;


    void Start()
    {
        BtnSetting();
        ResetPos.AddRange(ResetPosObjects.Select(obj => obj.transform.position));
    }

    private void BtnSetting()
    {
        Yes.onClick.AddListener(() => { TotalPricePanel.SetActive(true); });
        No.onClick.AddListener(() => { ToTitlePanel.SetActive(false); Time.timeScale = 1; });
        ToTitle.onClick.AddListener(() => { Totitle(); });
        DeadToTitle.onClick.AddListener(() => { DeadTotitle(); });
        StageClear.onClick.AddListener(() => { Clear(); });
    }

    public void ResetObjectPos()
    {
        foreach (var obj in ResetPosObjects)
        {
            obj.transform.position = ResetPos[ResetPosObjects.IndexOf(obj)];
        }
    }

    private void Totitle()
    {
        Time.timeScale = 1; 
        FadeInOut.instance.FadeIn("Title");
        PlayDataManager.instance.Save();
        GameManger.instance.money += Inventory.instance.TotalPrice;
        GameManger.instance.Playing = false;
        GameManger.instance.TimeScore = 0;
    }
    
    private void Clear()
    {
        Time.timeScale = 1; 
        GameManger.instance.money += Inventory.instance.TotalPrice;
        PlayDataManager.instance.Save();
        FadeInOut.instance.FadeIn(NextScnenName);
    }

    private void DeadTotitle()
    {
        Time.timeScale = 1;
        FadeInOut.instance.FadeIn("Title");
        GameManger.instance.Playing = false;
        GameManger.instance.TimeScore = 0;
    }

    void Update()
    {
        TotalPrice.text = $"TotalPrice : {Inventory.instance.TotalPrice}";
        ClearTotalPrice.text = $"TotalPrice : {Inventory.instance.TotalPrice}";
    }
}
