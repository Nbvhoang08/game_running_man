using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesMoving : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform aPoint,bPoint;
    Vector3 target;
    [SerializeField] private float speed;
    void Start()
    {
        transform.position= aPoint.position;
        target = bPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed*Time.deltaTime);
        if(Vector2.Distance(transform.position, aPoint.position) < 0.1f)
        {
            target = bPoint.position;
        }
        else if(Vector2.Distance(transform.position, bPoint.position) < 0.1f)  
        {
            target = aPoint.position;
        }
    }
}
