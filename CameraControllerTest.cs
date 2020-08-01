using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerTest : MonoBehaviour
{
    public Transform camTarget;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = camTarget.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = camTarget.position - offset;
        transform.LookAt(camTarget);
    }
}
