using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// управление ракетой + логика анимации взрыва ракеты
public class RocketMove : MonoBehaviour
{
    [Header("Сontrol settings")]
    [SerializeField]
    float rotationSpeed;
    [SerializeField]
    float maxSpeed;
    [SerializeField]
    float force;

    [Space(15)]
    [SerializeField]
    ParticleSystem fire;
    [SerializeField]
    ParticleSystem explosionPart;
    [SerializeField]
    GameObject rocketBody;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.isGame)
        {
            Control();
            Border.Instance.checkPosition(transform);
        }
    }

    void Control()
    {
        //поворот
        transform.Rotate(0, 0, -Input.GetAxis("Horizontal") * rotationSpeed * Time.fixedDeltaTime);

        // движение вперёд
        var forceVec = transform.up * force * Mathf.Clamp(Input.GetAxis("Vertical"), 0f, 1f);
        rb.AddForce(forceVec, ForceMode.Force);

        // ограничение ускорения движения
        var velocityClamp = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(rb.velocity.y, -maxSpeed, maxSpeed));
        rb.velocity = velocityClamp;

        // анимация огня из сопла ракеты
        fire.startLifetime = 0.1f * Mathf.Clamp(Input.GetAxis("Vertical"), 0f, 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet") || other.CompareTag("Asteroid"))
        {
            if (GameManager.Instance.isGame)
            {
                GameManager.Instance.RocketCollision();
                StartCoroutine(Explosion());
            }
        }
    }
    // логика взрыва ракеты
    IEnumerator Explosion()
    {
        fire.Stop();
        explosionPart.Play();
        rocketBody.SetActive(false);
        rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(3);

        transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, 0), Vector3.up);
        fire.Play();
        transform.position = Vector2.zero;
        rocketBody.SetActive(true);
    }
}
