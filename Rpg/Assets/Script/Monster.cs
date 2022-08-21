using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public int atk;
    public int defense;
    public int exp;
    public int hp;
    public int maxHp;

    float rdm_x;
    float rdm_y;
    Vector2 pos;
    float timer;
    int rdmTimer;
    float scale;
    public Animator ani;
     
     
    public GameObject weapon;

    float coolTime = 0;

    int i = 0;

    public bool chaseThePlayer;

    bool die;

     
    public GameObject Damagetext;

     
     
    void Start()
    {
         
       chaseThePlayer = false;

        die = false;

        atk = 10;
        defense = 4;
        exp = 20;
        hp = 50;
        maxHp = 50;

        timer = 0;
        rdm_x = Random.Range(-10, 13);
        rdm_y = Random.Range(91, 76);
        rdmTimer = Random.Range(1, 4);
        scale = this.gameObject.transform.localScale.x;

        if (GameObject.Find("Player").GetComponent<Player>().lv >= 10 )
        {

            atk *= (GameObject.Find("Player").GetComponent<Player>().lv / 10)*10;
            defense *= (GameObject.Find("Player").GetComponent<Player>().lv / 10)*10;
            exp *= GameObject.Find("Player").GetComponent<Player>().lv / 10;
            hp *= (GameObject.Find("Player").GetComponent<Player>().lv / 10)*10;
             

        }

    }

    // Update is called once per frame
    void Update()
    {
         

        Vector2 p1 = transform.position;
        Vector2 p2 = GameObject.Find("Player").transform.position;
        Vector2 dir = p1 - p2;
        float d = dir.magnitude;
        float r1 = 1.5f;
        float r2 = 1.5f;
        if (d < r1 + r2)
        {
             
            chaseThePlayer = true;
        }
        else
        {
            chaseThePlayer = false;
        }
        if (i < 1)
        {
            if (hp <= 0)
            {
                GameObject.Find("Player").GetComponent<Player>().AddExp(exp);
                ani.SetTrigger("Die");
                Invoke("del", 2f);
                i++;
                GameObject.Find("MonsterSummon").GetComponent<MonsterSummon>().monsterCount--;
                die = true;
            }
        }
        if (!die)
        {
             

            pos = new Vector2(rdm_x, rdm_y);
            if (!chaseThePlayer)
            {


                timer += Time.deltaTime;
                if (timer >= rdmTimer)
                {
                    if (this.gameObject.transform.position.x != rdm_x && this.gameObject.transform.position.y != rdm_y)
                    {

                        this.gameObject.transform.position = Vector2.MoveTowards
                            (
                                gameObject.transform.position, pos, 3 * Time.deltaTime
                            );
                        ani.SetFloat("RunState", 0.5f);
                    }
                    else if (this.gameObject.transform.position.x == rdm_x && this.gameObject.transform.position.y == rdm_y)
                    {

                        rdm_x = Random.Range(-10, 13);
                        rdm_y = Random.Range(91, 76);
                        rdmTimer = Random.Range(1, 4);
                        timer = 0;
                        ani.SetFloat("RunState", 0f);
                    }
                    else
                    {
                        rdm_x = Random.Range(-10, 13);
                        rdm_y = Random.Range(91, 76);
                        rdmTimer = Random.Range(1, 4);
                        timer = 0;
                        ani.SetFloat("RunState", 0f);
                    }
                }
            }
            else if (chaseThePlayer && GameObject.Find("Player").GetComponent<Player>().playerDie == false)
            {
                coolTime += Time.deltaTime;

                rdm_x = GameObject.Find("Player").transform.position.x + 2f;
                rdm_y = GameObject.Find("Player").transform.position.y;

                this.gameObject.transform.position = Vector2.MoveTowards
                            (
                                gameObject.transform.position, pos, 3 * Time.deltaTime
                            );
                if (this.gameObject.transform.position.x == pos.x && this.transform.position.y == pos.y)
                {
                    ani.SetFloat("RunState", 0f);
                    if (coolTime > 1.5f)
                    {
                        ani.SetTrigger("Attack");
                        weapon.GetComponent<MonsterAtk>().attack = true;
                        coolTime = 0;
                    }

                }
                else
                {
                    ani.SetFloat("RunState", 0.5f);
                }



            }

            if (this.gameObject.transform.position.x < rdm_x)
            {
                this.gameObject.transform.localScale = new Vector2(-scale, this.gameObject.transform.localScale.y);
            }
            else
            {
                this.gameObject.transform.localScale = new Vector2(scale, this.gameObject.transform.localScale.y);
            }
            if (GameObject.Find("Player") != null)
            {
                if (GameObject.Find("Player").GetComponent<Player>().playerDie == true)
                {
                    chaseThePlayer = false;
                }
            }
             

        }

    }
     
    private void OnTriggerEnter2D(Collider2D collision)
    {
         
        if (collision.gameObject.name == "L_Weapon_Player")
        {
            if (GameObject.Find("Player").GetComponent<Player>().playerAtk)
            {
                Damages(GameObject.Find("Player").GetComponent<Player>().atk);
                GameObject _text = Instantiate(Damagetext);
                _text.transform.position = GameObject.Find("Main Camera").GetComponent<Camera>().WorldToScreenPoint(
                    new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y+3)
                    );
                _text.GetComponent<Text>().text = (Damages(GameObject.Find("Player").GetComponent<Player>().atk)).ToString();
            }
                 
        }


    }
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        chaseThePlayer = false;
             
    //    }
    //}
    public float Damages(float a)
    {
        if (a > defense)
        {
             
            hp -= (int)a - defense;
             
            return (int)a - defense;
             
        }
        else if (a < defense)
        {
             
            hp -= 0;
             
            return 0;
             
        }
        else
        {
            hp -= (int)a - defense;
             
            return (int)a - defense;
             
        }
    }
    void del()
    {
        Destroy(gameObject);
    }
}
