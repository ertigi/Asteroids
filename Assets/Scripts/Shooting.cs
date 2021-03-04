using UnityEngine;

//стрельба 

public class Shooting : MonoBehaviour
{
    [Header("Shooting setting")]
    [SerializeField]
    float startImpulseForce = 30;
    [SerializeField]
    float lifeTime = 1.2f;

    [Space(10)]
    [SerializeField]
    GameObject bulletPref;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("space") && GameManager.Instance.isGame)
        {
            Shoot();
        }
    }
    void Shoot()
    {
        // создание нового объекта
        GameObject newBullet = Instantiate(bulletPref, transform.position, Quaternion.identity, transform);

        // установка начальных координат
        newBullet.transform.localPosition = new Vector2(0, 1.2f);
        newBullet.transform.SetParent(transform.parent);

        // установка вектора силы
        SetStartImpulse(newBullet);

        Destroy(newBullet, lifeTime);
    }

    public void SetStartImpulse(GameObject bullet)
    {
        var bulletRB = bullet.GetComponent<Rigidbody>();
        bulletRB.AddForce(transform.up * startImpulseForce, ForceMode.Impulse);
        bulletRB.velocity += rb.velocity;
    }

}
