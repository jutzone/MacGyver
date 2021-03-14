using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float damage = 50;
    public GameObject hitParticle;

    void Start()
    {      
        Destroy(gameObject, 2.1f);
    }

    public void SetLine(Vector3 endPos)
    {
        LineRenderer line = GetComponentInChildren<LineRenderer>();
        line.SetPosition(0, transform.position);
        line.SetPosition(1, endPos);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponentInParent<EnemyController>())
        {
            collision.transform.GetComponentInParent<EnemyController>().Damage(Random.Range(damage / 1.5f, damage));
        }
        else
        {
            if (!collision.transform.CompareTag("Coll"))
            {
                Instantiate(hitParticle, collision.contacts[0].point, Quaternion.identity);
            }
        }
        Destroy(gameObject);
    }
}