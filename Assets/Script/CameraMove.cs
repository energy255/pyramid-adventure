using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Vector3 originPos;
    private GameObject CurrentPos;

    void Start()
    {
        originPos = transform.position;
    }

    void Update()
    {
        CurrentPos = PlayerController.instance.CurrentCameraPos;

        if (CurrentPos != null) transform.position = Vector3.Lerp(transform.position, CurrentPos.transform.position + originPos, Time.deltaTime * 10f);
    }
}
