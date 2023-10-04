using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using Object = UnityEngine.Object;

public class ExpandBall : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] LayerMask Player;
    [SerializeField] float timeUnExpand, timeExpand ;
    private player player;
    Direction movingDir;
    RaycastHit2D hit;
    float posX, posY;
    bool var = false;
    

    void Start()
    {
        StartCoroutine(ExpansionCycle());
        player = FindAnyObjectByType<player>();
        
    }
    void Update()
    {
       
        posX = transform.position.x - player.transform.position.x;
        posY = player.transform.position.y - player.transform.position.y;
       
    }
    
    IEnumerator ExpansionCycle()
    {
        
        Object go = new Object() ;
        while (var)
        {
            yield return new WaitForSeconds(timeUnExpand);

            if (player.movingHorizontally)
            {
                if (posX < 0)
                {
                    go = Instantiate(Resources.Load("prefab/spike"), transform.position + Vector3.right, Quaternion.identity);
                   
                    yield return new WaitForSeconds(timeExpand);
                    Destroy(go);
                    var = false;
                }
                else if (posX > 0)
                {
                    go = Instantiate(Resources.Load("prefab/spike"), transform.position + Vector3.left, Quaternion.identity);
                    
                    yield return new WaitForSeconds(timeExpand);
                    Destroy(go);
                    var = false;
                }
            }
            else    
            {
                if (posY > 0)
                {
                    go = Instantiate(Resources.Load("prefab/spike"), transform.position + Vector3.up, Quaternion.identity);
                   
                    yield return new WaitForSeconds(timeExpand);
                    Destroy(go);
                    var = false;
                }
                else if (posY < 0)
                {
                    go = Instantiate(Resources.Load("prefab/spike"), transform.position + Vector3.down, Quaternion.identity);

                    yield return new WaitForSeconds(timeExpand);
                    Destroy(go);
                    var = false;
                }
            }
            

        }
        

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.CompareTag("Player"))
        {
            var = true;
            StartCoroutine(ExpansionCycle());
        }
        
     }
}
