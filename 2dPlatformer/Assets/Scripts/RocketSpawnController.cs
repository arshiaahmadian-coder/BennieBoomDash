using System.Collections;
using UnityEngine;

public class RocketSpawnController : MonoBehaviour
{
    [SerializeField] private Transform frontSpawnPoint;
    [SerializeField] private Transform backSpawnPoint;
    [SerializeField] private Transform deg45pawnPoint1;
    [SerializeField] private Transform deg45pawnPoint2;
    [SerializeField] private Transform deg45pawnPoint3;
    [SerializeField] private Transform deg45pawnPoint4;
    [SerializeField] private Transform deg45pawnPoint5;
    [SerializeField] private GameObject[] rockets;
    [SerializeField] private float begingTimer;
    [SerializeField] private float levelupDistance;
    private float time = 0;
    private float timer;
    private int level = 1;
    private bool spanInLoop = true;
    public bool gotFirstCoin = false;

    private int maxIndexToSpawn = 0;
    private int just1Rocket = -1;
    private int maxSpawnInEachLoop = 1;
    private int spawnTogather = 1;
    private bool spawnTogatherFlag = false;
    private int bombrain = 5;

    public static RocketSpawnController instance;
    private void Awake() { instance = this; }

    private void Start()
    {
        timer = begingTimer;
    }

    private void FixedUpdate()
    {
        if (!gotFirstCoin) return;
        // timer reset
        time += Time.deltaTime;
        if (time >= timer)
        {
            time = 0;
            if (spanInLoop)
            {
                if (maxSpawnInEachLoop == 1) SpawnRocket();
                else
                {
                    StopAllCoroutines();
                    StartCoroutine(SpawnWithDelay());
                }
            }
        }

        // level increase manager
        if (PlayerMovement.instance.transform.position.x >= level * levelupDistance)
        {
            LevelIncreasor();
        }
    }

    IEnumerator SpawnWithDelay()
    {
        for (int i = 0; i < maxSpawnInEachLoop; i++) {
            SpawnRocket();
            yield return new WaitForSeconds(1);
        }
    }

    private void SpawnRocket()
    {
        if (just1Rocket >= 0)
        {
            Spawn(just1Rocket);
        } else if (maxIndexToSpawn == 0)
        {
            Spawn(maxIndexToSpawn);
        } else if (maxIndexToSpawn > 0)
        {
            int index = getRandomNumber(maxIndexToSpawn);
            Spawn(index);
        }
    }

    private void Spawn(int index)
    {
        if (index == 1)
        {
            if(spawnTogather == 1)
                Instantiate(rockets[index], deg45pawnPoint1.position, rockets[index].transform.rotation);
            if (spawnTogather == 2)
            {
                Instantiate(rockets[index], deg45pawnPoint1.position, rockets[index].transform.rotation);
                Instantiate(rockets[index], deg45pawnPoint2.position, rockets[index].transform.rotation);
            } if (spawnTogather == 5 && bombrain > 0)
            {
                Instantiate(rockets[index], deg45pawnPoint1.position, rockets[index].transform.rotation);
                Instantiate(rockets[index], deg45pawnPoint2.position, rockets[index].transform.rotation);
                Instantiate(rockets[index], deg45pawnPoint3.position, rockets[index].transform.rotation);
                Instantiate(rockets[index], deg45pawnPoint4.position, rockets[index].transform.rotation);
                Instantiate(rockets[index], deg45pawnPoint5.position, rockets[index].transform.rotation);
                bombrain--;
            }

            if (spawnTogatherFlag)
            {
                spawnTogather = (spawnTogather == 1) ? 2 : 1;
            }
        } else
        {
            Instantiate(rockets[index], frontSpawnPoint.position, rockets[index].transform.rotation);
        }
    }

    private int getRandomNumber(int max)
    {
        return Random.Range(0, max + 1);
    }

    private void LevelIncreasor()
    {
        StopAllCoroutines();
        level += 1;

        if (level == 3)
        {
            timer = 3;
        } else if (level == 5) // just spawn 45 deg rocket
        {
            just1Rocket = 1;
        } else if (level == 6) // 1 or 2
        {
            just1Rocket = -1;
            maxIndexToSpawn = 1;
        } else if (level == 8) // just 3
        {
            just1Rocket = 2;
        } else if (level == 9) // 1 or 2 or 3
        {
            just1Rocket = -1;
            maxIndexToSpawn = 2;
        } else if (level == 11) // each loop 2 rockets
        {
            maxSpawnInEachLoop = 2;
            timer = 4;
        } else if (level == 13) // each loop 3 rocket
        {
            maxSpawnInEachLoop = 3;
            timer = 5;
        } else if (level == 15) // 22 - 1s - 12 - 3s
        {
            just1Rocket = 2;
            maxSpawnInEachLoop = 2;
            timer = 3;
            spawnTogatherFlag = true;
            spawnTogather = 2;
        } else if (level == 17)
        {
            spawnTogather = 1;
        } else if (level == 18) // just 4
        {
            just1Rocket = 3;
            spawnTogatherFlag = false;
            spawnTogather = 1;
            maxSpawnInEachLoop = 1;
        } else if (level == 19) // 1-4 - 1s - 1-4 - 3s
        {
            just1Rocket = -1;
            maxSpawnInEachLoop = 2;
            maxIndexToSpawn = 3;
            timer = 4;
        } else if (level == 23)
        {
            maxSpawnInEachLoop = 3;
            timer = 5;
        } else if (level == 26)
        {
            timer = 1.5f;
            maxSpawnInEachLoop = 1;
        } else if (level == 27) // bomb rain
        {
            just1Rocket = 1;
            spawnTogather = 5;
            timer = 1;
        } else if (level == 28)
        {
            just1Rocket = -1;
        } else if (level == 32)
        {
            GameManager.instance.Gameover(true);
        }

        // print(level + "; " + timer);
    }
}
