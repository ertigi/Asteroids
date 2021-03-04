using System.Collections;
using UnityEngine;

// основная игровая логика

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    int startLifes;
    [SerializeField]
    int startSize;
    [SerializeField]
    int startAsteroidsQuantity;

    [SerializeField]
    AsteroidSpawner asteroidSpawner;
    [SerializeField]
    Menu menu;

    int asteroidsQuantity;
    int stage = 0;
    int stageGrowth = 2;

    int points;

    int lifes;

    // флаг, отвечающий за возможность управления ракетой, стрельбы 
    // т.к. он используется в других классах, выставил такие модификаторы доступа
    public bool isGame { get; private set; }


    private void Awake()
    {
        Instance = this;
    }
    // Действие при нажатии кнопки "Play"
    public void StartGame()
    {
        lifes = startLifes;
        points = 0;
        CreateLevel();
        isGame = true;
    }

    // создание больших астероидов в старте игры и при начале нового этапа ( все астероиди уничтожены )
    void CreateLevel()
    {
        asteroidsQuantity = startAsteroidsQuantity + (stage * stageGrowth);

        for (int i = 0; i < asteroidsQuantity; i++)
        {
            asteroidSpawner.Spawn(startSize, Vector2.zero);
        }
    }

    // обработка столкновений астероидов с пулями 
    public void AsteroidCollision(int size, Vector2 position)
    {
        AddPoints(size);
        asteroidsQuantity--;
        if (size > 0)
        {
            asteroidSpawner.Spawn(--size, position);
            asteroidsQuantity += 2;
        }

        if(asteroidsQuantity <= 0)
        {
            stage++;
            CreateLevel();
        }
    }

    // добавление очков при попадании пулей в астероид
    void AddPoints(int size)
    {
        points += size == 2 ? 20 : size == 1 ? 50 : 100;
        menu.PointUpdate(points.ToString());
    }

    // обработка столкновения ракеты с пулей или астероидом ( подсчёт кол-ва жизней )
    public void RocketCollision()
    {
        isGame = false;
        menu.LifesUpdate(lifes);

        if(lifes > 1)
            StartCoroutine(RocketExplosion(3));
        else
        {
            menu.GameOver(points);
        }
        
        lifes--;

    }
    
    
    IEnumerator RocketExplosion(float timer)
    {
        yield return new WaitForSeconds(timer);
        isGame = true;
    }
}
