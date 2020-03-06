using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageShow : MonoBehaviour {
    private Health tank;
    public Image image;
    public bool myoldvalue;
    public bool enemyoldvalue;
    public Color enemycolor=Color.red;
    public Color mycolor = Color.green;
    private void Start()
    {
        tank = GetComponentInParent<Health>();
        myoldvalue = tank.select;
        enemyoldvalue = tank.enemyselect;
        image.gameObject.SetActive(myoldvalue | enemyoldvalue);
    }
    private void OnGUI()
    {
        if ((myoldvalue != tank.select) || (enemyoldvalue != tank.enemyselect)) 
        {
            myoldvalue = tank.select;
            enemyoldvalue = tank.enemyselect;
            image.gameObject.SetActive(myoldvalue | enemyoldvalue);
        }
        if (enemyoldvalue)
            image.color = enemycolor;
        if (myoldvalue)
            image.color = mycolor;
    }
}
