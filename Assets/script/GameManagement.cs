using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour {
    public GameObject bosstank;
    public Select select;
    public int min_money;
    public Text text;
    public float tol_time = 2.0f;
    private float time = 0.0f;
    private bool start = false;
    private void OnEnable()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }
    private void Update()
    {
        if (start)
            time += Time.deltaTime;
        else
            time = 0.0f;
        if (time >= tol_time)
            SceneManager.LoadScene("Start", LoadSceneMode.Single);

        if (!bosstank)
        {
            start = true;
            text.gameObject.SetActive(true);
            text.text = "Congratulation! You Win!";
        }
        else
        if ((select.alltanks.Count == 0) && (select.GetComponent<management>().GetMoney() < min_money))
        {
            start = true;
            text.gameObject.SetActive(true);
            text.text = "Sorry! You Lose!";
        }
    }
}
