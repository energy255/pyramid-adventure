using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManger : Singleton<GameManger>
{
    public float money;
    public float BagLevel;
    public float OxygenLevel;
    public float TimeScore;
    public bool FreeBuy;
    public bool Playing = false;
    public bool Clear = false;

    public List<ItemType> Items;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        StartManager.instance.Load();
    }

    void Update()
    {
        if (Playing) TimeScore += Time.deltaTime;

        Key();
    }

    private void Key()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            PlayerController.instance.Hp = PlayerController.instance.MaxHp;
            PlayerController.instance.O2 = PlayerController.instance.MaxO2;
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            FreeBuy = !FreeBuy;
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            Time.timeScale = Time.timeScale == 1 ? Time.timeScale = 0 : Time.timeScale = 1;
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            int index = Random.Range(0, Items.Count);
            Inventory.instance.AddItem(Items[index]);
        }
        if (Input.GetKeyDown(KeyCode.F7))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            print("dfs");
        }
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            PlayerController.instance.O2 -= PlayerController.instance.MaxO2 / 6;
        }
    }
}
