using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploration : MonoBehaviour {
    public float Exploration_life_time=2.0f;
    public GameObject tankexploration;
    private GameObject x;
    public void play(Vector3 position)
    {
        if (tankexploration)
        {
            x = Instantiate(tankexploration, transform);
            x.transform.position = position;
            x.GetComponent<AudioSource>().Play();
            x.GetComponent<ParticleSystem>().Play();
            Destroy(x, Exploration_life_time);
        }
    }
}
