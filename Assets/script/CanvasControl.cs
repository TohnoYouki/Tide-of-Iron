using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasControl : MonoBehaviour {
    private Quaternion localrotation;
    private void Start()
    {
        localrotation = transform.parent.localRotation;
    }
    private void Update()
    {
        transform.rotation = localrotation;
    }
}
