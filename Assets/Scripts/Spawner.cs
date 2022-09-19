using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public AStar pathfinder;
    public Node startNode;
    public Node goal;

    public GameObject ufoPrefab;
    public int waveSize = 1;
    public float spawnTimer = 0;
    public Toggle auto;
    // Update is called once per frame
    void Update()
    {
        if (GameManager.state == GameManager.GamePlayStates.Game)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0)
            {
                spawnTimer = 20;
                StartCoroutine(SpawnWave());
            }
            else if (spawnTimer <= 18 && auto.isOn)//if auto spawn is on
            {
                SkipWave();
                StartCoroutine(SpawnWave());
                spawnTimer = 20;
            }
        }
    }

    public void SkipWave()
    {
        if (spawnTimer <= 18)//no spamming
        {
            GameManager.score += spawnTimer * 10;
            spawnTimer = 0;
        }
    }

    void SpawnEnemy()
    {
        EnemyAI ufo = Instantiate(ufoPrefab, startNode.transform.position, Quaternion.identity).GetComponent<EnemyAI>();
        ufo.pathfinder = pathfinder;
        ufo.currentNode = startNode;
        ufo.goal = goal;
    }

    IEnumerator SpawnWave()
    {
        int currentSize = waveSize;//next wave will always be bigger
        //increase wave size
        waveSize *= 2;
        for (int i = 0; i < currentSize; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.1f);
        }
    }
}
