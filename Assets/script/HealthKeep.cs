using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKeep : MonoBehaviour {
    public string button_name;
    private void Update()
    {
        if (Input.GetButtonDown(button_name))
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            gameObject.transform.position = position;
            gameObject.GetComponent<ClickToMove>().ChangeTarget(null, transform.position);
        }
    }
}
