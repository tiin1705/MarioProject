using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainCanvas : MonoBehaviour
{
    public Text LifeText;
    private int life;
    private Vector2 originalPosition;
    // Start is called before the first frame update
    void Start()
    {
        life = 3;
        LifeText.text = life + "";
        originalPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        CheckLife();        
    }
    public void CheckLife()
    {
        if(life > 0 )
        {
            life--;
            LifeText.text = life + "";
           // SceneManager.LoadScene(0);
           transform.localPosition = originalPosition; 
        }
        else
        {
            Debug.Log("GameOver");
        }
    }
}
