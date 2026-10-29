using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 25;

    private void OnCollisionEnter(Collision collision)
    {
        Target target = collision.gameObject.GetComponent<Target>();

        if (target != null)
            target.TakeDamage(damage);

        Destroy(gameObject);
    }
}
