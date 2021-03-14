using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Main")]
    public float health;
    private float prevHealth;
    private bool dead;
    private Rigidbody[] ragdoll;
    public TextMesh healthText;
    private Transform player;
    [Header("Animation")]
    public Animator anim;
    private string currentAnim;


    private void Start()
    {
        PlayAnim("IdleStay");
        ragdoll = GetComponentsInChildren<Rigidbody>();
        RagdollFreeze(true);
        prevHealth = health;
        CheckHealth();
        player = FindObjectOfType<PlayerController>().transform;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.position) > 10)
        {
            healthText.gameObject.SetActive(false);
        }
        else
        {
            healthText.gameObject.SetActive(true);
        }
    }

    void Death()
    {
        anim.enabled = false;
        RagdollFreeze(false);
        FindObjectOfType<GameController>().OnEnemyKill();
    }

    void PlayAnim(string clip)
    {
        if (currentAnim != clip)
        {
            anim.CrossFade(clip, 0.1f);
            currentAnim = clip;
        }
    }

    void RagdollFreeze(bool state)
    {
        for (int i = 0; i < ragdoll.Length; i++)
        {
            ragdoll[i].isKinematic = state;
        }
    }

    public void Damage(float damage)
    {
        health -= damage;
        CheckHealth();
        anim.Play("Hit", -1, 0f);
        //PlayAnim("Hit");
    }

    void CheckHealth()
    {
        if (health > 0)
        {
            healthText.text = health.ToString("##");
        }

        if (health > 30)
            healthText.color = Color.white;

        if (health < 30)
            healthText.color = Color.red;

        if (health <= 0 && !dead)
        {
            healthText.text = "";
            Death();
            dead = true;
        }
    }
}