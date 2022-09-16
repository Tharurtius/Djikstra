using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaser : MonoBehaviour
{
    private LineRenderer lr;
    private int layerMask;
    [SerializeField] private float cooldown = 2;
    [SerializeField] private float current = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        //setup the line renderer
        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, transform.position);
        //layermask
        layerMask = 1 << 6;
        //change pathfinding
        Collider[] nodes = Physics.OverlapSphere(transform.position, 10, 1 << 7);
        foreach (Collider node in nodes)
        {
            node.GetComponent<Node>().TowerClose += 10;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //cooldown for laser
        if (GameManager.state == GameManager.GamePlayStates.Game)//keep shooting if game is not over
        {
            current -= Time.deltaTime;
        }
        //check for targets
        Collider[] targets = Physics.OverlapSphere(transform.position, 10, layerMask);
        if (current <= 0 && targets.Length > 0)//if there is a target available
        {
            current = cooldown;
            StartCoroutine(Shoot(targets[0].gameObject));
        }
    }
    //pew pew
    IEnumerator Shoot(GameObject target)
    {
        lr.SetPosition(1, target.transform.position);
        StartCoroutine(target.transform.root.GetComponent<EnemyAI>().Die());
        yield return new WaitForSeconds(0.5f);
        lr.SetPosition(1, transform.position);
    }
}
