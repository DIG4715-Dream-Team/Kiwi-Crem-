using UnityEngine;

public class ShieldDespawn : MonoBehaviour
{
    [SerializeField] private float despawnTime = 5.1f;
    void Update()
    {
        despawnTime -= Time.deltaTime;
        if (despawnTime < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
