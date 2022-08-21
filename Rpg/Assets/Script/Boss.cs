using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    int hp;
    int maxHp;
    public int atk;
    public int defense;
    int exp;
    public float timer;
    float atkTime = 0;
    public bool chaseThePlayer;
    public GameObject player;
    public Animator ani;
    public GameObject bossHp;
    public Text text_;
    Vector2 pos;
    Vector3 bossPos;
    public GameObject Damagetext;
    bool die;
    public bool bossAtk;
    public GameObject monster;
    int i;
    void Start()
    {
        i = 0;
        bossAtk = false;
        die = false;
        chaseThePlayer = false;
        hp = 1000000;
        maxHp = 1000000;
        atk = 30000;
        defense = 100000;
        exp = 100000;
        bossPos = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        bossHp.GetComponent<Image>().fillAmount = (float)hp / (float)maxHp;
        text_.text = hp.ToString() + "/" + maxHp.ToString();
        pos = new Vector2(player.transform.position.x + 2f, player.transform.position.y);
        if (!die)
        {
            if (chaseThePlayer)
            {
                bossHp.SetActive(true);
                this.gameObject.transform.position = Vector2.MoveTowards
                                (
                                    gameObject.transform.position, pos, 3 * Time.deltaTime
                                );


                if (new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y) == pos)
                {
                    timer += Time.deltaTime;
                    //보스 패턴
                     
                    if (timer>5&&timer < 6)
                    {
                        
                        if (i != 5)
                        {
                            Summon();
                             
                        }
                         
                    }
                    else if(timer > 7 && timer < 8)
                    {
                        i = 0;
                    }
                    else if (timer>9&&timer < 10)
                    {
                        if (i != 1)
                        {
                            atk *= 2;
                            i++;
                        }
                         
                    }
                     
                    else if (timer > 15)
                    {
                        atk /=2;
                        timer = 0;
                        return;
                    }
                    else
                    {
                        bossAtk = true;
                        ani.SetTrigger("Attack");
                        bossAtk = true;
                    }
                    if (timer > 11)
                    {
                        i = 0;
                    }
                    ani.SetFloat("RunState", 0f);
                }
                else
                {
                    ani.SetFloat("RunState", 0.5f);
                }
            }
             
        }
        if (!chaseThePlayer)
        {
            bossHp.SetActive(false);
            this.gameObject.transform.position = bossPos;
            timer = 0;
             
        }
        if (hp <= 0)
        {
            bossHp.SetActive(false);
            die = true;
            Invoke("del", 2f);
            ani.SetTrigger("Die");
            player.GetComponent<Player>().AddExp(exp);
        }
        
    }
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "L_Weapon_Player")
        {
            if (player.GetComponent<Player>().playerAtk)
            {
                Damages(player.GetComponent<Player>().atk);
                GameObject _text = Instantiate(Damagetext);
                _text.transform.position = GameObject.Find("Main Camera").GetComponent<Camera>().WorldToScreenPoint(
                    new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 3)
                    );
                _text.GetComponent<Text>().text = (Damages(player.GetComponent<Player>().atk)).ToString();
            }
        }
    }
    void del()
    {
        Destroy(gameObject);
    }
    void Summon()
    {
        GameObject summon = Instantiate(monster);
        summon.transform.position = pos;
        i++;
        if (!chaseThePlayer)
        {
            Destroy(summon);
        }
    }
}
