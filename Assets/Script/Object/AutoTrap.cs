using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTrap : MonoBehaviour
{
    public float Damage;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController.instance.Attacked(Damage);
        }
    }
}
