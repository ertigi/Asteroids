using UnityEngine;

public class Border : MonoBehaviour
{
    public static Border Instance;

    public Camera cam { get; private set; }
    int offsetZ = 20;

    private void Awake()
    {
        Instance = this;
        cam = Camera.main;
    }

    // проверка нахождения объекта (ракета, астероид, пуля) в пределах экрана
    public void checkPosition(Transform obj)
    {
        
        if (cam.WorldToViewportPoint(obj.position).x <= 0)
        {
            var newPos = new Vector3(1, cam.WorldToViewportPoint(obj.position).y, offsetZ);
            obj.position = cam.ViewportToWorldPoint(newPos);
        } 
        else if (cam.WorldToViewportPoint(obj.position).x >= 1)
        {
            var newPos = new Vector3(0, cam.WorldToViewportPoint(obj.position).y, offsetZ);
            obj.position = cam.ViewportToWorldPoint(newPos);
        }

        if (cam.WorldToViewportPoint(obj.position).y <= 0)
        {
            var newPos = new Vector3(cam.WorldToViewportPoint(obj.position).x, 1, offsetZ);
            obj.position = cam.ViewportToWorldPoint(newPos);
        }
        else if (cam.WorldToViewportPoint(obj.position).y >= 1)
        {
            var newPos = new Vector3(cam.WorldToViewportPoint(obj.position).x, 0, offsetZ);
            obj.position = cam.ViewportToWorldPoint(newPos);
        }
    }
}
