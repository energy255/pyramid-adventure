using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public float speed;
    public float MaxHp;
    public float MaxO2;
    [HideInInspector]
    public float Hp;
    [HideInInspector]
    public float O2;

    public GameObject RotationPart;
    public GameObject BlindPart;
    public GameObject HoldPart;
    public GameObject PutPart;
    public GameObject FindArrowPart;
    public GameObject SlashPart;
    public GameObject HideLightPart;
    public GameObject HoldObject;
    public GameObject AttackCollider;
    public GameObject AttackedParticle;
    public GameObject DeadUI;
    public GameObject DangerUI;
    [HideInInspector]
    public GameObject CurrentCameraPos;
    [HideInInspector]
    public GameObject InteractionObject;
    public Animator PlayerAnime;
    public Rigidbody Rb;
    public SkinnedMeshRenderer Body;
    public SkinnedMeshRenderer Head;
    public Material BlindMaterial;
    public Material NoBlindMaterial;

    public GameObject Objective;

    public float DecayO2Speed;
    private float SpeedBuff;
    private float SpeedDeBuff;
    public bool IsHide;
    public bool IsDead;

    void Start()
    {
        MaxO2 = MaxO2 + (MaxO2 * GameManger.instance.OxygenLevel);
        Hp = MaxHp;
        O2 = MaxO2;
    }

    void Update()
    {
        if (Hp <= 0 || O2 <= 0)
            Dead();
        else
            PlayerControll();
    }

    private void Dead()
    {
        if (!IsDead)
        {
            PlayerAnime.SetTrigger("Dead");
            IsDead = true;
        }
    }

    private void PlayerControll()
    {
        float axisX = Input.GetAxis("Horizontal");
        float axisZ = Input.GetAxis("Vertical");

        PlayerAnime.SetFloat("X", axisX);
        PlayerAnime.SetFloat("Z", axisZ);
        RotationObject(axisX, axisZ, RotationPart);
        Attack();
        Hide();
        Find();
        SpeedDeBuff = Inventory.instance.OnDebuff ? -1 : 0;
        DangerUI.SetActive((MaxHp / 4) >= Hp || (MaxO2 / 4) >= O2 ? true : false);
        O2 -= Time.deltaTime * DecayO2Speed;

        Rb.MovePosition(Rb.position + new Vector3(axisX, 0, axisZ) * (speed + SpeedBuff + SpeedDeBuff) * Time.deltaTime);
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0)) PlayerAnime.SetTrigger("Attack");
    }

    private void RotationObject(float PosX, float PosZ, GameObject rotationObject)
    {
        Vector3 Pos = new Vector3(PosX, 0, PosZ).normalized;

        float angle = Mathf.Atan2(Pos.x, Pos.z) * Mathf.Rad2Deg;

        if (Pos != Vector3.zero)
            rotationObject.transform.eulerAngles = new Vector3(0, Mathf.LerpAngle(rotationObject.transform.eulerAngles.y, angle, Time.deltaTime * 10f), 0);
    }

    public void Attacked(float Damage)
    {
        Hp -= Damage;
        CameraShake.instance.SetUp(0.1f, 0.1f);
        Instantiate(AttackedParticle, transform.position, AttackedParticle.transform.rotation);
        StartCoroutine(Blind());

    }

    private void Hide()
    {
        HideLightPart.SetActive(!IsHide);
        var blind = IsHide ? BlindMaterial : NoBlindMaterial;
        Body.material = blind;
        Head.material = blind;
    }

    public void FindSetting()
    {
        if (PlayManager.instance.FindChest.Count <= 0) return;

        List<GameObject> chests = PlayManager.instance.FindChest;
        List<GameObject> notOpenChest = chests.Where(a => true != chests[chests.IndexOf(a)].GetComponent<Chest>().isOpen).ToList();

        int index = Random.Range(0, notOpenChest.Count);
        Objective = notOpenChest[index];
        FindArrowPart.SetActive(true);
    }

    private void Find()
    {
        if (Objective == null) return;

        Vector3 pos = Objective.transform.position - transform.position;
        RotationObject(pos.x, pos.z, FindArrowPart);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CameraPos"))
        {
            CurrentCameraPos = other.gameObject;
        }

        if (other.gameObject.CompareTag("InteractionObject"))
        {
            InteractionObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("InteractionObject"))
        {
            InteractionObject = null;
        }
    }

    private IEnumerator Blind()
    {
        int Count = 10;
        float delay = 0.1f;
        bool blind = false;

        for (int i = 0; i < Count; i++)
        {
            BlindPart.SetActive(blind);
            blind = !blind;
            yield return new WaitForSeconds(delay);   
        }
        BlindPart.SetActive(true);

        yield return null;
    }

    public IEnumerator SpeedUp(float amount, float time)
    {
        SpeedBuff = amount;
        yield return new WaitForSeconds(time);
        SpeedBuff = 0;

        yield return null;
    }

    public IEnumerator OnHide(float time)
    {
        IsHide = true;
        yield return new WaitForSeconds(time);
        IsHide = false;

        yield return null;
    }
}
