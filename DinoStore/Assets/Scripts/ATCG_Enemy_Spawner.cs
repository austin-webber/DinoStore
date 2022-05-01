using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATCG_Enemy_Spawner : MonoBehaviour
{
    public float startTimeBetweenSpawn;
    private float timeBetweenSpawn;
    public GameObject[] enemies;
    private bool delayExecuted;
    public float startDelay;
    private float timeElapsed;

    private void Awake()
    {
        delayExecuted = false;
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;

        if (!delayExecuted)
        {
            StartCoroutine(StartSpawnDelay(startDelay));
        }
        else if (timeBetweenSpawn <= 0 && delayExecuted && timeElapsed < 25)
        {
            int rand = Random.Range(0, enemies.Length);
            Instantiate(enemies[rand], transform.position, Quaternion.identity);
            timeBetweenSpawn = startTimeBetweenSpawn;
        }
        else if (timeBetweenSpawn <= 0 && delayExecuted && timeElapsed > 25)
        {
            Debug.Log("Speed up");
            int rand = Random.Range(0, enemies.Length);
            Instantiate(enemies[rand], transform.position, Quaternion.identity);
            timeBetweenSpawn = startTimeBetweenSpawn - 0.5f;
        }
        else
        {
            timeBetweenSpawn -= Time.deltaTime;
        }
    }

    private IEnumerator StartSpawnDelay (float delay)
    {
        yield return new WaitForSeconds(delay);

        delayExecuted = true;
    }
}
