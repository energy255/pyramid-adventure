using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potal : MonoBehaviour
{
    public GameObject PosOne;
    public GameObject PosTwo;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            float PosOneDis = Vector3.Distance(PlayerController.instance.transform.position, PosOne.transform.position);
            float PosTwoDis = Vector3.Distance(PlayerController.instance.transform.position, PosTwo.transform.position);

            PlayerController.instance.transform.position = PosOneDis > PosTwoDis ? PosOne.transform.position : PosTwo.transform.position;
            PlayManager.instance.ResetObjectPos();
        }
    }
}
