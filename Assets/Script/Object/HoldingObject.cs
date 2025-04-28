using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingObject : Interaction
{


    protected override void Update()
    {
        base.Update();

        if (!onInput || PlayerController.instance.InteractionObject != gameObject) return;

        if (PlayerController.instance.HoldObject == null) Hold();
        else Put();

    }

    private void Hold()
    {
        transform.parent = PlayerController.instance.HoldPart.transform;
        transform.position = PlayerController.instance.HoldPart.transform.position;
        PlayerController.instance.HoldObject = gameObject;
    }

    private void Put()
    {
        transform.SetParent(null);
        transform.position = PlayerController.instance.PutPart.transform.position;
        PlayerController.instance.HoldObject = null;
    }
}
