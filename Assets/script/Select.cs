using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select : MonoBehaviour {
    private Vector2 point1, point2, oldpoint1, oldpoint2, targetpoint;
    private Vector2 drawpoint1, drawpoint2;
    public Image draw_image;
    public GameObject target_image;
    public Canvas target_canvas;
    public ClickObject clickobject;
    private bool if_draw, if_draw_target;
    public List<ClickToMove> alltanks = new List<ClickToMove>();
    private void Start()
    { 
        targetpoint = oldpoint1 = oldpoint2 = point1 = point2 = new Vector2(0, 0);
        if_draw_target = if_draw = false;
    }

    public float enemy_select_radius = 1.0f;
    public LayerMask enemy_layermask;
    private Vector2 targetposition;
    private Collider2D target = null;
    public Collider2D select_target = null;
    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(1) && clickobject.if_a())
        {
            if ((target) && (target.tag=="tank"))
                target.GetComponent<Health>().enemyselect = false;
            targetpoint = Input.mousePosition;
            targetposition = Camera.main.ScreenToWorldPoint(targetpoint);
            target = Physics2D.OverlapCircle(targetposition, enemy_select_radius, enemy_layermask);
            for (int i = 0; i < alltanks.Count; i++)
                if (alltanks[i].select)
                    alltanks[i].ChangeTarget(target, targetposition);
            if ((target) && (target.tag == "tank") && (target.gameObject.layer != gameObject.layer))
                target.GetComponent<Health>().enemyselect = true;
            else
                if_draw_target = true;
        }
    }
    private int first = -1;
    private ClickToMove y;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && clickobject.if_a())
        {
            drawpoint1 = Input.mousePosition;
            point1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if_draw = true;
            select_target = Physics2D.OverlapCircle(point1, enemy_select_radius);
            point2 = point1;
        }
        if (Input.GetMouseButton(0))
        {
            drawpoint2 = Input.mousePosition;
            point2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
            if_draw = false;

        if (point1 != point2)
        {
            if (point1 != oldpoint1 || point2 != oldpoint2)
            {
                first = -1;
                for (int i = 0; i < alltanks.Count; i++)
                {
                    Vector2 position = alltanks[i].GetComponent<Rigidbody2D>().transform.position;
                    alltanks[i].select = false;
                    if (((position.x < point1.x) && (position.x > point2.x)) || ((position.x > point1.x) && (position.x < point2.x)))
                        if (((position.y < point1.y) && (position.y > point2.y)) || ((position.y > point1.y) && (position.y < point2.y)))
                        {
                            first = i;
                            alltanks[i].select = true;
                        }
                }
            }
        }
        else
        {
            first = -1;
            for (int i = 0; i < alltanks.Count; i++)
                alltanks[i].select = false;
            if (select_target && (select_target.gameObject.layer == gameObject.layer))
            {
                y = select_target.GetComponent<ClickToMove>();
                if (y)
                    y.select = true;
                first = -1;
                for (int i = 0; i < alltanks.Count; i++)
                    if (alltanks[i].select)
                    {
                        first = i;
                        break;
                    }
            }
        }
        oldpoint1 = point1;
        oldpoint2 = point2;
        if (first != -1)
            select_target = alltanks[first].GetComponent<Collider2D>();
    }

    public void change_first()
    {
        first = -1;
        for (int i = 0; i < alltanks.Count; i++)
            if (alltanks[i].select)
            {
                first = i;
                break;
            }
    }

    private void OnGUI()
    {
        if (if_draw)
        {
            draw_image.gameObject.SetActive(true);
            Draw(drawpoint1, drawpoint2);
        }
        else
            draw_image.gameObject.SetActive(false);
        if (if_draw_target)
        {
            Draw_Target(targetpoint);
            if_draw_target = false;
        }
    }

    void Draw_Target(Vector2 point)
    {
        GameObject x;
        x = Instantiate(target_image, target_canvas.transform) as GameObject;
        x.GetComponent<RectTransform>().position = point;
    }

    void Draw(Vector2 position1,Vector2 position2)
    {
        Vector2 position = (position1 + position2) / 2;
        draw_image.GetComponent<RectTransform>().position = position;
        float x = Mathf.Abs(position1.x - position2.x);
        float y = Mathf.Abs(position1.y - position2.y);
        draw_image.GetComponent<RectTransform>().sizeDelta = new Vector2(x, y);
    }
}
