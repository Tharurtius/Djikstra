using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOFlight : MonoBehaviour
{
    public float speed = 5f;
    public AStar pathfinder;

    public List<Node> path;
    // Update is called once per frame

    private void Start()
    {
        StartCoroutine(Wait());
    }

    void Update()
    {
        if (path.Count > 0)
        {
            GotoPoint(path[0].transform.position);
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
        }
    }

    IEnumerator Wait()
    {
        while (true)
        {
            if (pathfinder.complete)
            {
                foreach (Node node in pathfinder.shortestPath)
                {
                    path.Add(node);
                }
                //Debug.Log("Finished waiting");
                break;
            }
            else
            {
                //Debug.Log("Waiting");
                yield return null;
            }
        }
    }
}