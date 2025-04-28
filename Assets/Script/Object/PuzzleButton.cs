using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButton : MonoBehaviour
{
    public GameObject ClearObject;
    public bool isClear;

    private void OnTriggerEnter(Collider other)
    {
        if (ClearObject == other.gameObject)
        {
            isClear = true;
        }
    }
}
