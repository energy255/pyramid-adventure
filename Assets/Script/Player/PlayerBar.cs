using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBar : MonoBehaviour
{
    public Image HpBar;
    public Image O2Bar;


    void Update()
    {
        HpBar.fillAmount = PlayerController.instance.Hp / PlayerController.instance.MaxHp;
        O2Bar.fillAmount = PlayerController.instance.O2 / PlayerController.instance.MaxO2;
    }
}
