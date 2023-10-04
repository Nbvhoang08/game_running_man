using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeHor : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] LayerMask Player;
    [SerializeField] float timeUnExpand, timeExpand;
    private player player;
    Direction movingDir;
    RaycastHit2D hit;
    float posX;
    bool var = false;
    void Start()
    {
        StartCoroutine(ExpansionCycle());
        player = FindAnyObjectByType<player>();
    }

    // Update is called once per frame
    void Update()
    {
        posX = transform.position.x - player.transform.position.x;
       
    }
    IEnumerator ExpansionCycle()
    {

        Object go = new Object();
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
