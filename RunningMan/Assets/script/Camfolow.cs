using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camfolow : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public Vector3 offset;
    public float speed ;
    void Start()
    {
        target = Object.FindAnyObjectByType<player>().transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,target.position + offset,Time.deltaTime*speed);
    }
}
