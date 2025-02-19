using UnityEngine;

public class projectile : MonoBehaviour
{
    public float lifeTime = 3f; 

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject); // Destruir el proyectil al impactar
    }
}
