using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PolyNavAgent))]
public class Enemy_Movement : MonoBehaviour {
    private PolyNavAgent _agent;
    public PolyNavAgent agent
    {
        get
        {
            if (!_agent)
                _agent = GetComponent<PolyNavAgent>();
            return _agent;
        }
    }

    public Vector2 targetposition;
    public Collider2D target;
    private Select select;
    private float attack_distance;
    public bool path;
    private Tower tower;
    private void Start()
    {
        attack_distance = gameObject.GetComponent<Tower>().attack_distance;
        tower = gameObject.GetComponent<Tower>();
        target = null;
        targetposition = transform.position;
        select = GameObject.FindGameObjectWithTag("select").GetComponent<Select>();
    }
    private int index;
    private float min = 0.0f;
    private Vector2 x, y;
    private void FixedUpdate()
    {
        if (tower.attack_gameobject)
        {
            agent.MySetDestination(transform.position);
            agent.Stop();
        }
        else
        {
            if (!target)
            {
                index = -1;
                y = transform.position;
                for (int i=0;i<select.alltanks.Count;i++)
                {
                    x = select.alltanks[i].transform.position;
                    if (i == 0)
                    {
                        min = (y - x).sqrMagnitude;
                        index = 0;
                    }
                    else if ((y - x).sqrMagnitude < min)
                    {
                        min = (y - x).sqrMagnitude;
                        index = i;
                    }
                }
                if (index == -1)
                    target = null;
                else
                    target = select.alltanks[index].GetComponent<Collider2D>();
            }
            if (target)
                agent.MySetDestination(target.transform.position);
            if ((target) && (agent.remainingDistance <= attack_distance))
            {
                agent.Stop();
                targetposition = transform.position;
            }
        }
        path = agent.hasPath;
    }

    public void SetTower()
    {
        if (target)
            tower.target_gameobject = target.gameObject;
        else
            tower.target_gameobject = null;
    }
}
