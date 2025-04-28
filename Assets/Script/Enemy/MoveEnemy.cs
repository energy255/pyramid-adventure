using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    public GameObject DeadParticle;
    public float Speed;
    public float Damage;


    void Update()
    {
        if (!PlayerController.instance.IsHide) Move();
    }

    private void Move()
    {
        var pos = PlayerController.instance.transform.position - transform.position;

        RotationObject(pos.x, pos.z, gameObject);
        transform.position += new Vector3(pos.x, 0, pos.z).normalized * Time.deltaTime * Speed;
    }

    private void RotationObject(float PosX, float PosZ, GameObject rotationObject)
    {
        Vector3 Pos = new Vector3(PosX, 0, PosZ).normalized;

        float angle = Mathf.Atan2(Pos.x, Pos.z) * Mathf.Rad2Deg;

        if (Pos != Vector3.zero)
            rotationObject.transform.eulerAngles = new Vector3(0, Mathf.LerpAngle(rotationObject.transform.eulerAngles.y, angle, Time.deltaTime * 10f), 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Attack());
            PlayerController.instance.Attacked(Damage);
        }

        if (other.gameObject.CompareTag("AttackCollider"))
        {
            Instantiate(DeadParticle, transform.position, DeadParticle.transform.rotation);
            gameObject.SetActive(false);
        }
    }

    private IEnumerator Attack()
    {
        GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(1);
        GetComponent<BoxCollider>().enabled = true;

        yield return null;
    }
}
