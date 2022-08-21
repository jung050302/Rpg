using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Portal : MonoBehaviour
{
    public GameObject keyImage;
    public GameObject player;
     
     
    bool a = false;
    Image black;
    void Start()
    {
         
         
        black = GameObject.Find("black").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (a)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                 
                StartCoroutine(FadeOut());
                a = false; 
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            keyImage.SetActive(true);
            keyImage.transform.position = new Vector2(0.45f, collision.gameObject.transform.position.y + 3.5f);
            a = true;
        }
         
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            keyImage.SetActive(false);
            a = false;
        }
    }
    IEnumerator FadeOut() 
    {
        //text.color = new Color(text.color.r, text.color.g, text.color.b, 255);
        player.SetActive(false);
        while (black.color.a < 1.0f)
        {
            black.color = new Color(black.color.r, black.color.g, black.color.b, black.color.a + (Time.deltaTime / 3.0f));
            yield return null;
        }
         
        player.transform.position = new Vector2(1.52f, 91.29f);
        
        while (black.color.a > 0.0f)
        {
            black.color = new Color(black.color.r, black.color.g, black.color.b, black.color.a - (Time.deltaTime / 3.0f));
            yield return null;
        }
        player.SetActive(true);
    }
}
