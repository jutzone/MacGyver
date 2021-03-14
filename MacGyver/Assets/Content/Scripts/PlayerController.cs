using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public GameController game;

    [Header("Moving")]
    public float characterSpeed;
    public Transform shootColl;
    
    private NavMeshAgent agent;
    [Header("Shooting")]
    public Transform gunPusher;
    public GameObject bullet;
    [Header("Animation")]
    public Animator anim;
    private string currentAnim;

    private void Awake()
    {
        game = FindObjectOfType<GameController>();

        agent = GetComponent<NavMeshAgent>();
        agent.speed = characterSpeed;

        shootColl.SetParent(null);
    }

    void Update()
    {
        agent.SetDestination(game.waypoints[game.currentWaypoint].position);

        if (agent.remainingDistance < agent.stoppingDistance)
        {
            RotateToWaypoint(game.waypoints[game.currentWaypoint].rotation);
            PlayAnim("Idle");
        }
        else
        {
            PlayAnim("Run");
        }

        if (Input.GetMouseButtonDown(0) && game.gameStarted)
        {
            if (game.canTapNextWaypont)
            {
                if (game.currentWaypoint < game.waypoints.Length - 1)
                {
                    game.currentWaypoint++;
                    game.OnTapNextWaypont();
                }
            }
            else
            {
                if (agent.remainingDistance < agent.stoppingDistance)
                {
                    Shoot();
                }
            }
        }

        shootColl.position = transform.position;
        shootColl.rotation = transform.rotation;
    }

    void Shoot()
    {
        RaycastHit hit;
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        GameObject newGameObject = Instantiate(bullet, gunPusher.position, Quaternion.identity);
        if (Physics.Raycast(mouseRay, out hit))
        {
            newGameObject.transform.position = gunPusher.position;
            newGameObject.GetComponent<Rigidbody>().AddForce((hit.point - newGameObject.transform.position) * 1000);
            newGameObject.GetComponent<BulletController>().SetLine(hit.point);
        }
    }

    void PlayAnim(string clip)
    {
        if (currentAnim != clip)
        {
            anim.CrossFade(clip, 0.1f);
            currentAnim = clip;
        }
    }

    void RotateToWaypoint(Quaternion rot)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 5 * Time.deltaTime);
    }
}