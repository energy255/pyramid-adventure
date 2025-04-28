using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PushBlock : Interaction
{
    public List<GameObject> PushBlocks = new();
    public List<GameObject> CantPushBlocks = new();
    public GameObject OffBox;
    public bool isOpen;


    protected override void Update()
    {
        base.Update();

        if (!onInput || PlayerController.instance.InteractionObject != gameObject) return;

        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        GameObject PushingPos = NearPushBlock(PlayerController.instance.transform.position, true);
        Vector3 NextPos = PushingPos.transform.position;

        if (CantPushBlocks.IndexOf(PushingPos) != -1) yield break;

        OffBox.SetActive(false);
        while (Vector3.Distance(NextPos, transform.position) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, NextPos, Time.deltaTime * 10f);
            yield return null;

        }
        transform.position = NextPos;

        OffBox.SetActive(true);
        CantPushBlocks = new();

        yield return null;
    }

    private GameObject NearPushBlock(Vector3 nearObjectPos, bool reverse)
    {
        float currentdis = -1;
        GameObject nearBlock = null;

        foreach (var pushBlock in PushBlocks)
        {
            float dis = Vector3.Distance(pushBlock.transform.position, nearObjectPos);

            if (currentdis == -1)
            {
                currentdis = dis;
                nearBlock = pushBlock;
            }
            if (currentdis > dis && !reverse)
            {
                currentdis = dis;
                nearBlock = pushBlock;
            }
            if (currentdis < dis && reverse)
            {
                currentdis = dis;
                nearBlock = pushBlock;
            }
        }

        return nearBlock;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            CantPushBlocks.Add(NearPushBlock(other.ClosestPoint(transform.position), false));
        }
    }
}
