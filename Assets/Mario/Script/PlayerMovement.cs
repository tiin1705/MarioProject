using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
   
    public GameObject fireball;
    //chuyển cảnh
    private bool isIdle= false;
    private bool isRight = true;
    private AudioSource audioSource;
    
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 0f;
    [SerializeField] private float velo = 0f;

    [SerializeField] private float falling = 0f;
    [SerializeField] private float lowJump = 0f;
    float dirX = 0f;
    float dirY = 0f;
    

    private Vector2 velocity;

    //check mạng
    private bool isAlive;
  
    public Text LifeText;
    private int life;
    private Vector2 originalPosition;
    public GameObject GameOver;
    //menu 
    

    public GameObject menu;
    public bool isPaused = false;
    public Text menuTimeText;


    //level up
    public LevelUp_MushRoom LevelUp_MushRoom;
    public int Level = 0;
    public bool Trans = false;
    public float Dotre;
    BoxCollider2D collider2D;

    // FireBall
    public GameObject fireBall;
    private enum MovementState {idle,running, jumping, Idle_LV2, Run_LV2, Jump_LV2 }
    
    private MovementState state = MovementState.idle; 
    // Start is called before the first frame update
    private void Start()
    {
        life = 3;
        LifeText.text = life + "";
        originalPosition = transform.localPosition;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        //chay qua trai, phai
        dirX = Input.GetAxisRaw("Horizontal");
        //nhảy
        Jump();
        //animation
        UpdateAnimationState();
        //menu
        gameMenu();
        //fireball
        MarioFireBall();
       
        if (Trans == true)
        {
            switch (Level)
            {
                case 0:StartCoroutine(MarioLevelDown()); 
                    Trans = false;
                    break;
               
                case 1: StartCoroutine(MarioEatMushroom());
                    Trans = false ;
                    break;
                case 2: StartCoroutine(MarioEatFlower());
                    Trans = false;
                    break;
                default:   Trans = false; break;

            }
        }
       




    }
    public void CheckLife()
    {
        if (life > 0 && Level == 0)
        {
            life--;
            LifeText.text = life + "";
            // SceneManager.LoadScene(0);
            transform.localPosition = originalPosition;
        }
        else 
        
        if(life>0 && Level == 1)
        {
            Level--;
            StartCoroutine(MarioLevelDown() );
        }
        else
        {
            Debug.Log("GameOver");
            GameOver.SetActive(true);

        }
    }
   
    public void LoadScene0()
    {
        SceneManager.LoadScene(0);
    }

    //running animation
    private void UpdateAnimationState()
    {
        MovementState state;

        if(dirX > 0f)
        {
           
            state = MovementState.running;
            anim.SetBool("isJumping", false);
           // StartCoroutine(Flip());
            sprite.flipX = false;
            isRight = true;

        }
        else if(dirX < 0f) 
        {
            
            state = MovementState.running;
            anim.SetBool("isJumping", false);
            // StartCoroutine(Flip());
            sprite.flipX = true;
            isRight= false;
        }
        else 
        {
            anim.SetBool("isJumping", false);
            state = MovementState.idle;
        }
        if(rb.velocity.y > .1f)
        {
            anim.SetBool("isJumping", true);
            state = MovementState.jumping;
        }
        anim.SetInteger("state", (int)state);
    }

    //animation lv2
    
    private void Jump()
    {
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        // velocity.x = Mathf.MoveTowards(velocity.x, dirX * moveSpeed, moveSpeed * Time.deltaTime);
        //Jump 
        if (Input.GetKeyDown("space"))
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce);
            
            
         

        }
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (falling - 1)* Time.deltaTime ;
           
        }
        else
        {
            if(rb.velocity.y>0 && !Input.GetKey("space"))
                {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJump - 1) * Time.deltaTime;
             
            }
        }
    }
    //bắt sự kiện 2 box collider va chạm với nhau
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy_Mushroom"))
        {
            //kiểm tra va chạm ở đâu
            Vector3 direction = collision.GetContact(0).normal;
            float directionX = direction.x;
            float directionY = direction.y;
            if (directionX > 0)
            {
                //mario hồi sinh
                isAlive = false;
               CheckLife();
                
            }else if (directionX < 0)
            {
                //mario  hồi sinh
                isAlive = false;
                CheckLife();
            }
        }
        if (collision.gameObject.CompareTag("Plant"))
        {
            Vector3 direction = collision.GetContact(0).normal;
            float directionX = direction.x;
            float directionY = direction.y;
            if (directionX > 0)
            {
                //mario hồi sinh
                isAlive = false;
                CheckLife();

            }
            else if (directionX < 0)
            {
                //mario  hồi sinh
                isAlive = false;
                CheckLife();
            }
            if (directionY > 0)
            {
                //mario hồi sinh
                isAlive = false;
                CheckLife();
            }
            else if(directionY < 0)
            {
                //mario hồi sinh
                isAlive = false;
                CheckLife();
            }
        }
       
    }
    //bắt sự kiện 2 box collider có is trigger true
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var nane = collision.gameObject.tag;
        if (name.Equals("Coin"))
        {
            //Tăng tiền
            //Xóa xu
            Destroy(collision.gameObject);
            //Phát nhạc
            PlaySound("Sound/smb3_coin");
        }
           
    }
    private void PlaySound(string name)
    {
        audioSource.PlayOneShot(Resources.Load<AudioClip>(name));
    }
    //biến hình
   
   
    //menu
    private void gameMenu()
    {
        //bật menu
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!isPaused)
            {
                isPaused = true;
                menu.SetActive(true);
                Time.timeScale = 0;
                menuTimeText.text = "Your score ";
            }
            

        }
    }
    public void ResumeGame()
    {
        Debug.Log(">>>>>>>>>>> (ResumeGame)");
        isPaused = false;
        menu.SetActive(false);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Debug.Log(">>>>>>>>>>> (QuitGame)");
        Application.Quit();
    }

    private void MarioFireBall()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GameObject fb = Instantiate(fireBall);
            fb.transform.position = new Vector3(
                    transform.position.x + (isRight ? 1f : -1f),
                    transform.position.y
                );

            Debug.Log(">>>>>>>.(FireBall)");
            fb.GetComponent<FireBall>()
                .SetSpeed(isRight ? 15 : -15);
          
        }
    }

    
   public void NextScene()
    {
        SceneManager.LoadScene(1);
    }
    IEnumerator MarioEatMushroom()
    {
        float Dotre = 0.1f;
        GetComponent<BoxCollider2D>().size = new Vector2(1f, 2f);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV0"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV1"), 1);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV2"), 0);
        yield return new WaitForSeconds(Dotre);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV0"), 1);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV1"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV2"), 0);
        yield return new WaitForSeconds(Dotre);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV0"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV1"), 1);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV2"), 0);
        yield return new WaitForSeconds(Dotre);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV0"), 1);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV1"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV2"), 0);
        yield return new WaitForSeconds(Dotre);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV0"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV1"), 1);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV2"), 0);
        yield return new WaitForSeconds(Dotre);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV0"), 1);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV1"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV2"), 0);
        yield return new WaitForSeconds(Dotre);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV0"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV1"), 1);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV2"), 0);
        yield return new WaitForSeconds(Dotre);


    }
    IEnumerator MarioEatFlower()
    {
        float Dotre = 0.1f;
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV0"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV1"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV2"), 1);
        yield return new WaitForSeconds(Dotre);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV0"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV1"), 1);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV2"), 0);
        yield return new WaitForSeconds(Dotre);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV0"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV1"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV2"), 1);
        yield return new WaitForSeconds(Dotre);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV0"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV1"), 1);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV2"), 0);
        yield return new WaitForSeconds(Dotre);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV0"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV1"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV2"), 1);
        yield return new WaitForSeconds(Dotre);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV0"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV1"), 1);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV2"), 0);
        yield return new WaitForSeconds(Dotre);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV0"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV1"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV2"), 1);
        yield return new WaitForSeconds(Dotre);

    }
    IEnumerator MarioLevelDown()
    {
        
        float Dotre = 0.1f;
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV0"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV1"), 1);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV2"), 0);
        yield return new WaitForSeconds(Dotre);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV0"), 1);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV1"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV2"), 0);
        yield return new WaitForSeconds(Dotre);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV0"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV1"), 1);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV2"), 0);
        yield return new WaitForSeconds(Dotre);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV0"), 1);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV1"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV2"), 0);
        yield return new WaitForSeconds(Dotre);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV0"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV1"), 1);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV2"), 0);
        yield return new WaitForSeconds(Dotre);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV0"), 1);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV1"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV2"), 0);
        yield return new WaitForSeconds(Dotre);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV0"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV1"), 1);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV2"), 0);
        yield return new WaitForSeconds(Dotre);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV0"), 1);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV1"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("MarioLV2"), 0);
        yield return new WaitForSeconds(Dotre);
        GetComponent<BoxCollider2D>().size = new Vector2(0.8f, 1f);
    }


}
