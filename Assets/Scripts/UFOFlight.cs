using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UFOFlight : MonoBehaviour
{
    public float speed = 5f;
    public AStar pathfinder;

    public List<Node> path;

    public Node currentNode;
    public Node goal;
    // Update is called once per frame

    private void Start()
    {
        //StartCoroutine(Wait());
        path = pathfinder.GetComponent<AStar>().FindShortestPath(currentNode, goal);
    }

    void Update()
    {
        if (path.Count > 0)
        {
            GotoPoint(path[0].transform.position);
        }
        else
        {
            currentNode = FindCurrentNode();
            goal = FindRandomNode();
            path = pathfinder.GetComponent<AStar>().FindShortestPath(currentNode, goal);
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

    private Node FindCurrentNode()
    {
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("Node");
        GameObject closest = null;
        foreach (GameObject node in nodes)
        {
            if (closest == null || Vector3.Distance(transform.position, closest.transform.position) > Vector3.Distance(transform.position, node.transform.position))
            {
                closest = node;
            }
        }
        return closest.GetComponent<Node>();
    }

    private Node FindRandomNode()
    {
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("Node");
        return nodes[Random.Range(0, nodes.Length - 1)].GetComponent<Node>();
    }

    //IEnumerator Wait()
    //{
    //    while (true)
    //    {
    //        if (pathfinder.complete)
    //        {
    //            foreach (Node node in pathfinder.shortestPath)
    //            {
    //                path.Add(node);
    //            }
    //            //Debug.Log("Finished waiting");
    //            break;
    //        }
    //        else
    //        {
    //            //Debug.Log("Waiting");
    //            yield return null;
    //        }
    //    }
    //}
}