using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Spawn setting")]
    [SerializeField]
    List<GameObject> asteroidsPref;

    [Header("Asteroid setting")]
    [SerializeField]
    float startAsteroidSpeed;
    [SerializeField]
    private float tumble;

    List<GameObject> asteroids;

    // спавн общий и для старта игры и для разрушения астероидов (из 1 появляются 2 )
    // для каждого size == 2 скрипт spawn нужно вызывать отдельно
    // маленькие астероиды создаются за 1 вызов
    public void Spawn(int size, Vector2 position)
    {
        // size = 2 - самый большой размер астероида
        // он можен появиться только в старте игры или при новом этапе
        // поэтому, если size == 2, нужна рандомная позиция по краю экрана
        // иначе берётся позиция старого астероида
        if (size >= 2)
            position = setStartPosition(Random.Range(0, 5));
        
        var length = size >= 2 ? 1 : 2;

        for (int i = 0; i < length; i++)
        {
            var newAsteroid = Instantiate(asteroidsPref[size], transform);
            var rb = newAsteroid.GetComponent<Rigidbody>();

            newAsteroid.transform.position = position;

            var forceVec = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * Random.Range(startAsteroidSpeed / 2, startAsteroidSpeed);
            rb.AddForce(forceVec);
            
            rb.angularVelocity = Random.insideUnitSphere * tumble;

            newAsteroid.GetComponent<Asteroid>().setSize(size);
        }
    }

    // установка рандомной позиции по краю экрана 
    Vector2 setStartPosition(int edge)
    {
        Vector2 position;

        position.x = edge == 2 || edge == 4 ? Random.Range(0f, 1f) : edge == 1 ? 0 : 1;
        position.y = edge == 1 || edge == 3 ? Random.Range(0f, 1f) : edge == 4 ? 0 : 1;

        return Border.Instance.cam.ViewportToWorldPoint(position);
    }
}
