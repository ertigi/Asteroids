using System.Collections;
using UnityEngine;

// вся логика астероида
// - размер
// - проверка столкновений
// - взрыв
public class Asteroid : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    ParticleSystem explosionPart;

    int size;

    public void setSize(int _size)
    {
        size = _size;
    }

    private void FixedUpdate()
    {
        Border.Instance.checkPosition(transform);
    }
    
    // проверка столкновения астероида с пулей
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            GameManager.Instance.AsteroidCollision(size, transform.position);
            StartCoroutine(Explosion());
            Destroy(other.gameObject);
        }
    }

    // логика взрыва астероида
    IEnumerator Explosion()
    {
        transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, 0), Vector3.up);
        rb.velocity = Vector2.zero;
        explosionPart.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<MeshCollider>().enabled = false;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
