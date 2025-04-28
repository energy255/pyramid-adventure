using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDataManager : Singleton<PlayDataManager>
{
    public GameObject[] ClearNoSpawnObject;
    public int StageNum;

    void Start()
    {
        Load();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ResetData();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Save();
        }
    }


    public void Save()
    {
        for (int i = 0; i < ClearNoSpawnObject.Length; i++)
        {
            PlayerPrefs.SetInt($"{StageNum}ClearObject{i}", ClearNoSpawnObject[i].GetComponent<ClearNoSpawn>().IsClear ? 1 : 0);
        }
    }

    private void Load()
    {
        for (int i = 0; i < ClearNoSpawnObject.Length; i++)
        {
            if (PlayerPrefs.HasKey($"{StageNum}ClearObject{i}"))
            {
                if (PlayerPrefs.GetInt($"{StageNum}ClearObject{i}") == 1) ClearNoSpawnObject[i].SetActive(false);
            }
        }
    }

    public void ResetData()
    {
        for (int i = 0; i < ClearNoSpawnObject.Length; i++)
        {
            if (PlayerPrefs.HasKey($"{StageNum}ClearObject{i}"))
            {
                PlayerPrefs.DeleteKey($"{StageNum}ClearObject{i}");
            }
        }
    }
}
