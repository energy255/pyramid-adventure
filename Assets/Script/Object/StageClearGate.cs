using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClearGate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0;
            PlayManager.instance.StageClearPanel.SetActive(true);
        }
    }
}
