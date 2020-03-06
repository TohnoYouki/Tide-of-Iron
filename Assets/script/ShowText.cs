using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowText : MonoBehaviour {
    private Select select;
    public Text attack;
    public Text armor;
    public Text life;
    public Image image;
    public Slider slider;
    public Image fill_image;
    private void Start()
    {
        select = GameObject.FindGameObjectWithTag("select").GetComponent<Select>();
    }
    private void Update()
    {
        Health health;
        Tower tower;
        if (!select.select_target)
        {
            attack.gameObject.SetActive(false);
            armor.gameObject.SetActive(false);
            life.gameObject.SetActive(false);
            image.gameObject.SetActive(false);
            slider.gameObject.SetActive(false);
        }
        else
        {
            GameObject target = select.select_target.gameObject;
            attack.gameObject.SetActive(true);
            armor.gameObject.SetActive(true);
            life.gameObject.SetActive(true);
            slider.gameObject.SetActive(true);
            health = target.GetComponent<Health>();
            tower = target.GetComponent<Tower>();
            if (health)
            {
                armor.text = "Armor: " + health.armor.ToString();
                life.text = "Life: " + health.life.ToString();
                if (health.this_image)
                {
                    image.sprite = health.this_image;
                    image.gameObject.SetActive(true);
                }
                else
                    image.gameObject.SetActive(false);
                slider.value = health.life / health.initial_life;
                fill_image.color = health.color();
            }
            else
            {
                armor.text = "Armor: 0";
                life.text = "Life: 0";
                image.gameObject.SetActive(false);
                slider.value = 0.0f;
            }
            if (tower)
                attack.text = "Attack: " + tower.shell.GetComponent<TankShell>().shellpower.ToString();
            else
                attack.text = "Attack: 0";
        }
    }
}
