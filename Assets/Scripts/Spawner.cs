using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public AStar pathfinder;
    public Node startNode;
    public Node goal;

    public GameObject ufoPrefab;
    public int waveSize = 1;
    public float spawnTimer = 0;
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
        }
    }

    public void SkipWave()
    {
        GameManager.score += spawnTimer * 10;
        spawnTimer = 0;
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
        for (int i = 0; i < waveSize; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.1f);
        }
        //increase wave size
        waveSize *= 2;
    }
}
