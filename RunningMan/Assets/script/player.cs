using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum Direction
{
    North, South, East, West
}

public class player : MonoBehaviour
{

    [SerializeField] LayerMask groundLayer;
    [SerializeField] float timeAfterTrapKill;
    [SerializeField] private Animator anim;
    [SerializeField] Rigidbody2D rb;
    public AudioSource soundTrack;
    public AudioSource soundGameOver;
    public AudioSource soundMove;
    public AudioSource soundCollected;
    private string CurrentAnimName;
    public float speed;
    public bool movingHorizontally = false, canCheck = true, isDeath = false;
    RaycastHit2D hit;
    Direction movingDir;
    public float Horizontal;
    public float Vertical;
    Vector3 savePoint;
    int score =0;
    int maxScore;
    // Start is called before the first frame update
    void Start()
    {
        soundTrack.Play();
        soundTrack.loop = true;
        rb = GetComponent<Rigidbody2D>();
        OnInit();
        
    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Debug.DrawLine(transform.position, transform.position + Vector3.down * 1.1f, Color.red);
        UnityEngine.Debug.DrawLine(transform.position, transform.position + Vector3.up * 1.1f, Color.red);
        UnityEngine.Debug.DrawLine(transform.position, transform.position + Vector3.right * 1.1f, Color.red);
        UnityEngine.Debug.DrawLine(transform.position, transform.position + Vector3.left * 1.1f, Color.red);
        if (isDeath)
        {
            return;
        }
       
        changeAnim("run");
        if (movingHorizontally)
        {
            if (Physics2D.Raycast(transform.position, transform.position + Vector3.left * 1.1f, groundLayer) || Physics2D.Raycast(transform.position, transform.position + Vector3.right * 1.1f, groundLayer))
            {
                canCheck = true;
                Debug.Log(canCheck + "Cancheck Horizontal");
                
            }
            else
            {
                canCheck = false;
                Debug.Log(canCheck + "Cancheck Horizontal");

            }
        }
        else
        {
            if (Physics2D.Raycast(transform.position, transform.position + Vector3.up * 1.1f, groundLayer) || Physics2D.Raycast(transform.position, transform.position + Vector3.down * 1.1f, groundLayer))
            {
                canCheck = true;
                Debug.Log(canCheck + "Cancheck Vertical");

            }
            else
            {
                canCheck = false;
                Debug.Log(canCheck + "Cancheck Vertical");

            }
        }

        if (canCheck)
        {


            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                movingHorizontally = true;
                soundMove.Play();
                if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    movingDir = Direction.East;
                }
                else
                {
                    movingDir = Direction.West;
                }
            }
            else if (Input.GetAxisRaw("Vertical") != 0)
            {
                soundMove.Play();
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                movingHorizontally = false;
                if (Input.GetAxisRaw("Vertical") > 0)
                {
                    movingDir = Direction.North;
                }
                else
                {
                    movingDir = Direction.South;
                }
            }
        }
    }
    void FixedUpdate()
    {
        switch (movingDir)
        {
            case Direction.North:
                rb.velocity = new Vector2(0, speed * Time.fixedDeltaTime);
                
                break;
            case Direction.South:
                rb.velocity = new Vector2(0, -speed * Time.fixedDeltaTime);
               
                break;
            case Direction.East:
                rb.velocity = new Vector2(speed * Time.fixedDeltaTime, 0);
                
                break;
            case Direction.West:
                rb.velocity = new Vector2(-speed * Time.fixedDeltaTime, 0);
               
                break;
        }
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Horizontal");
        if (Mathf.Abs(Horizontal) > 0.1f)
        {

            transform.localScale = new Vector3(Horizontal * 8, 8, 1);

        }

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
    private void ResetAnimation()
    {

        UnityEngine.Debug.Log("reset");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
       /* if (collision.gameObject.CompareTag("Traps"))
        {
            switch (movingDir)
            {
                case Direction.North:
                    hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), 1.6f, groundLayer);

                    break;
                case Direction.South:
                    hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), 1.6f, groundLayer);
                    break;
                case Direction.East:
                    hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), 1.6f, groundLayer);
                    break;
                case Direction.West:
                    hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 1.6f, groundLayer);
                    break;
            }
            if (hit.collider != null)
            {
                Destroy(gameObject);
            }
        }*/

    }
    private void OnInit()
    {   
        isDeath = false;
        UIManager.instance.setScore(score);


    }
   
    private IEnumerator EndGame(float delay)
    {
        // Chờ một khoảng thời gian
        yield return new WaitForSeconds(delay);

        // Sau khi chờ xong, hủy đối tượng
        UnityEditor.EditorApplication.isPlaying = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Apple")
        {
            score++;
            soundCollected.Play();
            UIManager.instance.setScore(score);
        }
        if (collision.tag == "TrapAfterTime")
        {
            isDeath = true;
            speed = 0;
            soundTrack.Stop();
            soundGameOver.Play();
            changeAnim("disapear");
            StartCoroutine(EndGame(2.5f));

        }
        if (collision.tag == "Bat")
        {
            isDeath = true;
            speed = 0;
            soundTrack.Stop();
            soundGameOver.Play();
            changeAnim("disapear");
            StartCoroutine(EndGame(2.5f));
            
        }
        if(collision.tag == "Cup")
        {
            StartCoroutine(EndGame(1.0f));
        }
    }
}
   
   


