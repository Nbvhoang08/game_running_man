using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Apple : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Animator anim ;
    private string CurrentAnimName;
    /*public AudioSource soundCollected;*/

    void Start()
    {
        changeAnim("idle");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void changeAnim(string animName)
    {
        if (CurrentAnimName != animName)
        {
            UnityEngine.Debug.Log(animName);
            anim.ResetTrigger(animName);
            CurrentAnimName = animName;
            anim.SetTrigger(animName);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            changeAnim("disaple");
           /* soundCollected.Play();*/
            StartCoroutine(DestroyAfterDelay(1.0f));
            
        }
        
    }
    IEnumerator DestroyAfterDelay(float delay)
    {
        // Chờ một khoảng thời gian
        yield return new WaitForSeconds(delay);

        // Sau khi chờ xong, hủy đối tượng
        Destroy(gameObject);
    }
}







