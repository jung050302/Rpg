using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossPortal2 : MonoBehaviour
{
    public GameObject keyImage;
    public GameObject player;
    public Image black;
    public Transform portal;
    bool a;
     
    void Start()
    {
        a = false;
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
            keyImage.transform.position = new Vector2(112.56f, collision.gameObject.transform.position.y + 3.5f);
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

        player.transform.position = new Vector2(portal.position.x, portal.position.y);

        while (black.color.a > 0.0f)
        {
            black.color = new Color(black.color.r, black.color.g, black.color.b, black.color.a - (Time.deltaTime / 3.0f));
            yield return null;
        }
        player.SetActive(true);
    }
}
