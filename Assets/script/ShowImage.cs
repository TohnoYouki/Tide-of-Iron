using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowImage : MonoBehaviour {
    private Select select;
    public Image[] position;
    public const int maxlength=18;
    public Slider[] slider = new Slider[maxlength];
    public Image[] fill_image;
    private void Start()
    {
        select = GameObject.FindGameObjectWithTag("select").GetComponent<Select>();
    }
    private Health health;
    private void Update()
    {
        int number = -1, i;
        for (i = 0; i < position.Length; i++)
            position[i].gameObject.SetActive(false);
        for (i=0;i<select.alltanks.Count;i++)
        {
            if (select.alltanks[i].select)
            {
                
                number++;
                if (number < maxlength)
                {
                    position[number].gameObject.SetActive(true);
                    health = select.alltanks[i].GetComponent<Health>();
                    position[number].sprite = health.this_image;
                    slider[i].value = health.life / health.initial_life;
                    fill_image[i].color = health.color();
                }
            }
        }
    }

}
