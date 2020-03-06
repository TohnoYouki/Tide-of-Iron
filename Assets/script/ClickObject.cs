using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObject : MonoBehaviour {
    public Vector2 point1;
    public Vector2 point2;
    public Vector2 position;
    public bool a;
    private void FixedUpdate()
    {
        a = if_a();
    }
    public bool if_a()
    {
        position = Input.mousePosition;
        if (((position.x < point1.x) && (position.x > point2.x)) || ((position.x > point1.x) && (position.x < point2.x)))
            if (((position.y < point1.y) && (position.y > point2.y)) || ((position.y > point1.y) && (position.y < point2.y)))
                return true;
        return false;
    }
}
