using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 5f;
    public AStar pathfinder;

    public List<Node> path;

    public Node currentNode;
    public Node goal;
    [SerializeField] private GameObject explosion;
    private void Start()
    {
        path = pathfinder.GetComponent<AStar>().FindShortestPath(currentNode, goal);
    }

    void Update()
    {
        if (GameManager.state == GameManager.GamePlayStates.Game)
        {
            if (path.Count > 0)
            {
                GotoPoint(path[0].transform.position);
            }
        }
    }

    void GotoPoint(Vector3 endGoal)
    {
        if (Vector3.Distance(transform.position, endGoal) > Time.deltaTime * speed)
        {
            Vector3 direction = (endGoal - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
        else
        {
            transform.position = endGoal;
            path.Remove(path[0]);
            if (path.Count == 0)
            {
                GameManager.lives--;
                Destroy(gameObject);
            }
        }
    }

    public IEnumerator Die()
    {
        GameObject boom = Instantiate(explosion, transform.position, Quaternion.identity);
        float time = 0.5f;
        transform.GetChild(0).gameObject.SetActive(false);
        GameManager.money += 10;
        GameManager.score += 10;
        while (time > 0)
        {
            time -= Time.deltaTime;
            boom.transform.localScale *= 1.05f;
            yield return null;
        }
        Destroy(boom);
        Destroy(gameObject);
    }
}