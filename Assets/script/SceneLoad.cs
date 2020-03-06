using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour {
    public GameObject hiden;
    public void LoadGame()
    {
        start = true;
    }
    public void exit()
    {
        Application.Quit();
    }
    private float time;
    public float tol_time;
    public float camera_size;
    private Camera maincamera;
    private bool start = false;
    private float distance;
    private float a;
    private void OnEnable()
    {
        start = false;
        maincamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        distance = maincamera.orthographicSize - camera_size;
        a = 4 * distance / (tol_time * tol_time);
    }
    private void FixedUpdate()
    {
        if (!start)
            time = 0.0f;
        else
        {
            hiden.SetActive(false);
            time += Time.deltaTime;
            if (time < tol_time / 2)
                maincamera.orthographicSize = camera_size + distance - 0.5f * a * time * time;
            else
                maincamera.orthographicSize = camera_size + 0.5f * a * (tol_time - time) * (tol_time - time);
            if (time >= tol_time)
            {
                start = false;
                SceneManager.LoadScene("Tank", LoadSceneMode.Single);
            }
        }
    }
}
