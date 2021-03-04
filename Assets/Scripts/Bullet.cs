using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void FixedUpdate()
    {
        Border.Instance.checkPosition(transform);
    }
}
