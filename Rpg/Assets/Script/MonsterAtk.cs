using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterAtk : MonoBehaviour
{
    Monster mst;
    public bool attack;
    public GameObject Damagetext;

    int i = 0;
     
    [SerializeField] Camera camera;
    void Start()
    {
         
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        attack = false;
        mst = GameObject.FindGameObjectWithTag("Monster").GetComponent<Monster>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (attack)
            {
                 
                if (i < 1)
                {
                    collision.gameObject.GetComponent<Player>().Damage(mst.atk);
                    i++;
                }
                 

                GameObject _text = Instantiate(Damagetext);
                _text.transform.position = camera.WorldToScreenPoint(new Vector2(GameObject.Find("Player").transform.position.x, GameObject.Find("Player").transform.position.y + 3));
                _text.GetComponent<Text>().text= GameObject.Find("Player").GetComponent<Player>().Damage(mst.atk).ToString();
                attack = false;
               
            }
             

        }
    }
}
