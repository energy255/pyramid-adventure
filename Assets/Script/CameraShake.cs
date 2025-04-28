using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : Singleton<CameraShake>
{
    private Vector3 originPos;

    public void SetUp(float time, float strength)
    {
        originPos = transform.position;
        StartCoroutine(Shake(time, strength));
    }

    private IEnumerator Shake(float time, float strength)
    {
        float t = 0;

        while (time > t)
        {
            t += Time.deltaTime;
            Vector3 pos = strength * Random.insideUnitSphere;
            transform.position = originPos + new Vector3(pos.x, 0, pos.z);

            yield return null;
        }

        while (Vector3.Distance(originPos, transform.position) > 0.05f)
        {
            transform.position = Vector3.Lerp(transform.position, originPos, Time.deltaTime * 10f);
            yield return null;
        }
        transform.position = transform.parent.position;

        yield return null;
    }
}
