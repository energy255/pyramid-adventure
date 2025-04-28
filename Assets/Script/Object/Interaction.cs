using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interaction : MonoBehaviour
{
    [SerializeField]
    protected GameObject InteractionKey;
    protected bool onInput;
    private Quaternion Rotation;

    protected void Start()
    {
        Rotation = transform.rotation;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        onInput = Input.GetKeyDown(KeyCode.X);

        InteractionKey.SetActive(PlayerController.instance.InteractionObject == gameObject);
        transform.rotation = Rotation;
    }
}
