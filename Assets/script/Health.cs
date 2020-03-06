using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    public float armor;
    public float initial_life;
    public float life;
    public Sprite this_image;
    private GameObject myselect;
    public Slider slider;
    public Image fill_image;
    public Color full_health_color = Color.green;
    public Color zero_health_color = Color.red;
    public bool enemyselect;
    public bool select;
    public int enemy_money = 0;
    private management manage;
    private ClickToMove click;
    private void OnEnable()
    {
        manage = GameObject.FindGameObjectWithTag("select").GetComponent<management>();
        slider.maxValue = initial_life;
        myselect = null;
        GameObject[] select = GameObject.FindGameObjectsWithTag("select");
        int i;
        for (i = 0; i < select.Length; i++)
            if (select[i].layer == gameObject.layer)
            {
                myselect = select[i];
                break;
            }
        click = this.gameObject.GetComponent<ClickToMove>();
        if (myselect && click)
            myselect.GetComponent<Select>().alltanks.Add(click);
    }

    public void ChangeArmor(float x)
    {
        armor = x;
    }

    private void OnDisable()
    {
        if (myselect)
        {
            myselect.GetComponent<Select>().alltanks.Remove(this.gameObject.GetComponent<ClickToMove>());
            myselect.GetComponent<Select>().change_first();
        }
    }

    private void Start()
    {
        life = initial_life;
        enemyselect = false;
    }

    private void Update()
    {
        if (click)
            select = click.select;
        SetHealthUI();
    }

    void SetHealthUI()
    {
        slider.value = life;
        fill_image.color = Color.Lerp(zero_health_color, full_health_color, life / initial_life);
    }

    public Color color()
    {
        return fill_image.color;
    }

    public float exploration_life_time=2.0f;
    public float GetDamage(float attack)
    {
        float x = 1.0f / (1.0f + 0.5f * armor) * attack;
        life = life - x;
        if (life > initial_life)
        {
            x = x + life - initial_life;
            life = initial_life;
        }
        if (life <= 0)
        {
            GameObject.FindGameObjectWithTag("exploration").GetComponent<Exploration>().play(transform.position);
            manage.AddMoney(enemy_money);
            Destroy(gameObject);
            x = life + x;
        }
        return x;
    }
}
