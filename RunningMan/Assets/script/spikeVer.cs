using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeVer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] LayerMask Player;
    [SerializeField] float timeUnExpand, timeExpand;
    private player player;
    Direction movingDir;
    RaycastHit2D hit;
    float posY;
    bool var = false;
    void Start()
    {
        StartCoroutine(ExpansionCycle());
        player = FindAnyObjectByType<player>();
    }

    // Update is called once per frame
    void Update()
    {
        posY =transform.position.y - player.transform.position.y;
        
    }
    IEnumerator ExpansionCycle()
    {

        Object go = new Object();
        while (var)
        {
            yield return new WaitForSeconds(timeUnExpand);
         
            yield return new WaitForSeconds(timeExpand);
            if (posY > 0)
            {
                go = Instantiate(Resources.Load("prefab/spike"), transform.position + Vector3.down, Quaternion.identity);
                yield return new WaitForSeconds(timeExpand);
                var = false;
            }
            else if (posY < 0)
            {
                go = Instantiate(Resources.Load("prefab/spike"), transform.position + Vector3.up, Quaternion.identity);
                yield return new WaitForSeconds(timeExpand);
                var = false;
            }
            
            Destroy(go);


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
