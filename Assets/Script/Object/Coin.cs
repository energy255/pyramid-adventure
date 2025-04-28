using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : ClearNoSpawn
{
    public GameObject GetParticle;
    public ItemType CoinItem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IsClear = true;
            Inventory.instance.AddItem(CoinItem);
            Instantiate(GetParticle, transform.position, GetParticle.transform.rotation);
            gameObject.SetActive(false);
        }
    }
}
