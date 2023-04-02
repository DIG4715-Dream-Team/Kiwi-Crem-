using UnityEngine.AI;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    public float range;
    public Transform centerPoint;

    public bool trackingPlayer;
    private GameObject player;
    private PlayerController Player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Player = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (trackingPlayer == true)
        {
            agent.SetDestination(player.transform.position);
            if (Player.CompareTag("HiddenPlayer"))
            {
                trackingPlayer = false;
            }
        }
        else if (trackingPlayer == false)
        {
            Patrolling();
        }
    }

    private void Patrolling()
    {
        if (agent.remainingDistance <= agent.stoppingDistance && trackingPlayer == false)
        {
            Vector3 point;
            if (RandomPoint(centerPoint.position, range, out point))
            {
                Debug.DrawLine(transform.position, point, Color.red, 5.0f);
                Debug.DrawRay(point, Vector3.up, Color.red, 5.0f);
                agent.SetDestination(point);
            }
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.gameObject.CompareTag("Player"))
        {
            Debug.DrawLine(transform.position, Player.transform.position, Color.magenta, 5.0f);
            Debug.DrawRay(Player.transform.position, Vector3.up, Color.magenta, 5.0f);
            trackingPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other != null && other.gameObject.CompareTag("Player"))
        {
            trackingPlayer = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.HealthManagement(-5);
            trackingPlayer = false;
            agent.SetDestination(centerPoint.transform.position);
        }
    }
}