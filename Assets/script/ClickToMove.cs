using UnityEngine;
using System.Collections.Generic;

//example
[RequireComponent(typeof(PolyNavAgent))]
public class ClickToMove : MonoBehaviour
{
    public bool select;
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
    private float attack_distance;
    public bool path;
    private Tower tower;
    private void Start()
    {
        attack_distance = gameObject.GetComponent<Tower>().attack_distance;
        select = false;
        tower = gameObject.GetComponent<Tower>();
        target = null;
        targetposition = transform.position;
    }
    private bool hastarget;
    public void ChangeTarget(Collider2D x, Vector2 y)
    {
        target = x;
        targetposition = y;
        SetTower();
        if (x)
            hastarget = true;
        else
            hastarget = false;
    }

    private void FixedUpdate()
    {
        if (target)
            agent.MySetDestination(target.transform.position);
        else if (hastarget)
            agent.MySetDestination(transform.position);
        else
            agent.MySetDestination(targetposition);
        if ((target) && (agent.remainingDistance <= attack_distance))
        {
            agent.Stop();
            targetposition = transform.position;
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
