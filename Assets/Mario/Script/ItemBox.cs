using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemBox : MonoBehaviour
{

    public float speed; //tốc độ    
    public float bounce; //độ nẩy
    public Vector2 originalPosition; //vị trí ban đầu
    public Sprite emptyItemBox;
    private bool canChange; // Va chạm khối
    
    public float ItemSpeedUp;
    public float ItemBounce;

    //các biến để gắn item
    public bool mushroom = false;
    public bool coin = false;
    public bool flower = false;
    public GameObject pre_mushroom;
    public GameObject pre_flower;
    public GameObject pre_coin;
    //lấy cấp độ của mario hiện tại 
    GameObject Player;
    public Vector3 originalPositionn;




    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    // Start is called before the first frame update

    void Start()
    {
        originalPosition = transform.position;
        originalPositionn = transform.position;
        canChange = true;
    }
   
    //bắt va chạm itembox
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!canChange) return;
        canChange = false;
        if (collision.gameObject.CompareTag("Player")&& collision.contacts[0].normal.y>0)
        {
           

            ItemGoUp();
            //chuyển sang khối khác
            GetComponent<SpriteRenderer>().sprite = emptyItemBox;
            GetComponent<Animator>().enabled = false;
            //nẩy lên và rơi xuống
            
            
            
        }
    }
    //nẩy lên và rớt xuống
    IEnumerator BounceUpandDown()
    {
        //nẩy lên
        while(true)
        {

            Debug.Log(">>>>>>>>(Bounce)");

            transform.position = new Vector3(
                transform.position.x,
                transform.position.y + speed * Time.deltaTime
                );
            if (transform.position.y > originalPosition.y + bounce)
                break;
            yield return null;

        }
        //rơi xuống
        while (true)
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y - speed * Time.deltaTime
                );
            if (transform.position.y < originalPosition.y)
            {
                transform.position = originalPosition;
                break;
            }
            
            yield return null;
        }
      
     

    }    

    private void ItemGoUp()
    {
        StartCoroutine(BounceUpandDown());
        if (mushroom)
        {
            MushroomAndFlower();
            Debug.Log(">>>>>>>>(Mushroom)");    
        }else if (coin)
        {
            Coin();
            Debug.Log(">>>>>>>>(Coin)");

        }


    }
    private void MushroomAndFlower()
    {

        Debug.Log(">>>>>(mushroom and flower");
        int CurrentLevel = Player.GetComponent<PlayerMovement>().Level;
        GameObject Mushroom = null;
        if (CurrentLevel == 0) Mushroom = (GameObject)Instantiate(pre_mushroom);
        else Mushroom = (GameObject)Instantiate(pre_mushroom);
        Mushroom.transform.SetParent(this.transform.parent);
        Mushroom.transform.localPosition = new Vector2(originalPositionn.x, originalPositionn.y + 1f);

    }
    private void Coin()
    {
        GameObject Coin = (GameObject)Instantiate(pre_coin);
        Coin.transform.SetParent(this.transform.parent);
        Coin.transform.localPosition = new Vector2(originalPositionn.x, originalPositionn.y + 1f);
        StartCoroutine(CoinBounce(Coin));
    }
    IEnumerator CoinBounce(GameObject Coin)
    {
        while (true)
        {

            Debug.Log(">>>>>>>>(CoinBounce)");
                    
            Coin.transform.position = new Vector3(
                Coin.transform.position.x,
                Coin.transform.position.y + ItemSpeedUp * Time.deltaTime
                );
            if (Coin.transform.position.y > originalPosition.y + 2f)
                break;
            yield return null;

        }

    }
}

    
