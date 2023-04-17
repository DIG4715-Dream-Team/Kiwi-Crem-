using UnityEngine;
using UnityEngine.AI;

public class AngelController : MonoBehaviour
{
    public NavMeshAgent agent;
    public float range;
    public Transform centerPoint;

    public bool trackingPlayer = false;
    private GameObject player;
    private PlayerController Player;

    [SerializeField] private float ProjectileTimer;

    private bool Fired = false;
    private bool startTimer = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Player = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (trackingPlayer == true)
        {
            transform.LookAt(player.transform.position);
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

        Timer();
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
            transform.GetChild(5).GetComponent<AngelProjectileController>().FireProjectile();
            Fired = true;
            ProjectileTimer = 5f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other != null && other.gameObject.CompareTag("Player"))
        {
            trackingPlayer = false;
        }
    }

    private void Timer()
    {
        if (Fired == true)
        {
            Fired = false;
            startTimer = true;
        }

        if (startTimer == true)
        {
            ProjectileTimer = ProjectileTimer -= Time.deltaTime;
            if (ProjectileTimer <= 0.1f)
            {
                transform.GetChild(5).GetComponent<AngelProjectileController>().FireProjectile();
                Fired = true;
                ProjectileTimer = 5f;
            }
        }
    }
}
