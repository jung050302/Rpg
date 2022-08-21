using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    float speed;
    float scale;
    public Sprite axe;
    public Animator ani;

    public GameObject Damagetext;
    public Camera _camera;

    float saveExp;

    //스텟
    public int lv;
    int hp;
    int maxHp;
    float maxExp;
    float exp;
    public int atk;
    public int defense;
    int str;

    public bool playerAtk;
    public bool playerDie;
    Vector2 savePos;
    public GameObject gameOver;

    bool first;

    public Image black;
     
    public GameObject _audio;
    public GameObject boss;
    void Start()
    {
         

        first = false;
        savePos = new Vector2(0, 0);
        Load2();

        saveExp = 0;

        playerDie = false;
        playerAtk = false;
        maxExp = 100;
        exp = 0;
        lv = 1;
        hp = 100;
        maxHp = 100;
         
        defense = 5;
        str= 10;
        atk = 8+str/2;
        speed = 5f;
        scale = this.gameObject.transform.localScale.x;
        if (!first)
        {
            Save();
            first = true;
            Save2();
        }

        Load();

        this.gameObject.transform.position = new Vector2(savePos.x, savePos.y);
        GameObject.Find("EXP").GetComponent<Image>().fillAmount = (float)exp / (float)maxExp;
        
    }

    // Update is called once per frame
    void Update()
    {
        print("방어력" + defense);
        
        if(Input.GetKey(KeyCode.LeftControl)&& Input.GetKey(KeyCode.D))
        {
             
            StartCoroutine(Fade());
        }
        Save();
        if (hp <= 0)
        {
            hp = 0;
            playerDie = true;
            ani.SetTrigger("Die");
            gameOver.SetActive(true);
        }
        if (!playerDie)
        {
            ani.SetFloat("RunState", 0f);
            BorderLine();
            Move();
            Attack();
            GameObject.Find("LVText").GetComponent<Text>().text = "LV:" + lv.ToString();
             
            GameObject.Find("ExpText").GetComponent<Text>().text = ((int)exp).ToString() + "/" + maxExp.ToString();
            if (exp >= maxExp)
            {
                LvUp();
                exp += saveExp;
                saveExp = 0;
                GameObject.Find("EXP").GetComponent<Image>().fillAmount = (float)exp / (float)maxExp;

            }
        }
        GameObject.Find("HPText").GetComponent<Text>().text = hp.ToString() + "/" + maxHp.ToString();
    }

    void Move()
    {
        
        if (Input.GetKey(KeyCode.LeftArrow))//왼쪽
        {
            gameObject.transform.Translate(-speed * Time.deltaTime, 0, 0);
            this.gameObject.transform.localScale = new Vector2(scale, this.gameObject.transform.localScale.y);
            ani.SetFloat("RunState", 0.5f);
        }
        if (Input.GetKey(KeyCode.RightArrow))//오른쪽
        {
            gameObject.transform.Translate(speed * Time.deltaTime, 0, 0);
            this.gameObject.transform.localScale = new Vector2(-scale, this.gameObject.transform.localScale.y);
            ani.SetFloat("RunState", 0.5f);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            gameObject.transform.Translate(0, -speed * Time.deltaTime, 0);
            ani.SetFloat("RunState", 0.5f);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            gameObject.transform.Translate(0, speed * Time.deltaTime, 0);
            ani.SetFloat("RunState", 0.5f);
        }
    }
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ani.SetTrigger("Attack");
            //weapon.GetComponent<PlayerAtk>().playerAtk = true;
            playerAtk = true;
            
        }
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("AttackState") && ani.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            playerAtk = true;
        }
        else
        {
            playerAtk = false;
        }
    }
    public float Damage(float a)
    {
        if (a > defense)
        {
            hp -= (int)a - defense;
            GameObject.Find("HPbar").GetComponent<Image>().fillAmount = (float)hp / (float)maxHp;
             
            return (int)a - defense;
        }
        else if (a < defense)
        {
            hp -= 0; 
            GameObject.Find("HPbar").GetComponent<Image>().fillAmount = (float)hp / (float)maxHp;
            return 0;
        }
        else
        {
            hp -= (int)a - defense;
            GameObject.Find("HPbar").GetComponent<Image>().fillAmount = (float)hp / (float)maxHp;
            return (int)a - defense;
        }
         
         
    }
    public void LvUp()
    {
        lv++;
        maxExp += (maxExp * 5) / 100;
        exp = 0;
         
        defense += (defense * 3 / 100)+5;
        str+= 10;
        
        maxHp += maxHp * 5 / 100;
        hp = maxHp;
        atk += ((atk * 4) / 100) + str/2;
        GameObject.Find("EXP").GetComponent<Image>().fillAmount = (float)exp / (float)maxExp;
        GameObject.Find("HPbar").GetComponent<Image>().fillAmount = (float)hp / (float)maxHp;
         
    }
    public void AddExp(float a)
    {
        if (exp + a > maxExp)
        {
            saveExp = (exp + a) - maxExp;
        }
        exp += a;
        GameObject.Find("EXP").GetComponent<Image>().fillAmount = (float)exp / (float)maxExp;
       
    }
    void BorderLine()
    {
        Vector2 pos = this.gameObject.transform.position;
        //맵 밖으로 못나가게 하기
        if(pos.x <-56.22&&pos.x>-57.22)
        {
            gameObject.transform.position = new Vector2(-56.21f, pos.y);
        }
        else if(pos.x > 56.22f&&pos.x<57)
        {
            gameObject.transform.position = new Vector2(56.22f, pos.y);
        }
        if (pos.y > 33.3 && pos.y < 34)
        {
            gameObject.transform.position = new Vector2(pos.x, 33.25f);
        }
        else if(pos.y < -33.3 && pos.y > -34)
        {
            gameObject.transform.position = new Vector2(pos.x, -33.25f);
        }

        if (pos.x < -14.5 && pos.x > -15.5&&pos.y< 100&&pos.y>53.7)
        {
            gameObject.transform.position = new Vector2(-14.45f, pos.y);
        }
        if(pos.x > 14.2 && pos.x < 14.5 && pos.y < 100 && pos.y > 53.7)
        {
            gameObject.transform.position = new Vector2(14f, pos.y);
        }
        if(pos.y > 92.6 && pos.y < 93)
        {
            gameObject.transform.position = new Vector2(pos.x, 92.55f);
        }
        if(pos.y < 73.5 && pos.y > 72.5)
        {
            gameObject.transform.position = new Vector2(pos.x, 73.55f);
        }

        if(pos.x < 108.3f&&pos.x>107.3f)
        {
            gameObject.transform.position = new Vector2(108.3f, pos.y);
        }
        else if(pos.x> 166.6f && pos.x < 170.6f)
        {
            gameObject.transform.position = new Vector2(166.5f, pos.y);
        }
        if(pos.y> 15.2f && pos.y < 16f&&pos.x> 108.35f)
        {
            gameObject.transform.position = new Vector2(pos.x, 15f);
        } 
        else if (pos.y < -22.5f && pos.y > -23f)
        {
            gameObject.transform.position = new Vector2(pos.x, -22.3f);
        }
    }

    private void Load()
    {
        
        savePos.x = PlayerPrefs.GetFloat("savePos_X");
        savePos.y = PlayerPrefs.GetFloat("savePos_Y");

        lv= PlayerPrefs.GetInt("LV");
        hp = PlayerPrefs.GetInt("HP");
        maxHp = PlayerPrefs.GetInt("maxHP");
        maxExp = PlayerPrefs.GetFloat("maxExp");
        exp = PlayerPrefs.GetFloat("Exp");
        atk = PlayerPrefs.GetInt("atk");
        defense = PlayerPrefs.GetInt("defense");
        str = PlayerPrefs.GetInt("str");
    }
    private void Save()
    {
        PlayerPrefs.SetFloat("savePos_X",this.gameObject.transform.position.x);
        PlayerPrefs.SetFloat("savePos_Y", this.gameObject.transform.position.y);
        PlayerPrefs.SetInt("LV", lv);
        PlayerPrefs.SetInt("HP", hp);
        PlayerPrefs.SetInt("maxHP", maxHp);
        PlayerPrefs.SetFloat("maxExp", maxExp);
        PlayerPrefs.SetFloat("Exp", exp);
        PlayerPrefs.SetInt("atk", atk);
        PlayerPrefs.SetInt("defense", defense);
        PlayerPrefs.SetInt("str", str);
 
    }
    void Delete()
    {
        PlayerPrefs.DeleteAll();
    }
    public void _Restart()
    {
        hp = maxHp;
        playerDie = false;
        GameObject.Find("HPbar").GetComponent<Image>().fillAmount = (float)hp / (float)maxHp;
    
    }
    void Save2()
    {
        PlayerPrefs.SetInt("first", System.Convert.ToInt16(first));
    }
    void Load2()
    {
        first = System.Convert.ToBoolean(PlayerPrefs.GetInt("first"));
    }

    IEnumerator Fade()
    {


        while (black.color.a < 1.0f)
        {
            black.color = new Color(black.color.r, black.color.g, black.color.b, black.color.a + (Time.deltaTime / 3.0f));
            yield return null;
        }
        Delete();
        SceneManager.LoadScene("MainLobby");

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
         
        if(collision.gameObject.tag== "Huntingground")
        {
            _audio.GetComponent<AudioSource>().clip = _audio.GetComponent<BGM>().bgm[0];
            _audio.GetComponent<AudioSource>().Play();
        }
        if (collision.gameObject.tag == "Village")
        {
            _audio.GetComponent<AudioSource>().clip = _audio.GetComponent<BGM>().bgm[1];
            _audio.GetComponent<AudioSource>().Play();
        }
        if(collision.gameObject.tag == "BossStage")
        {
            _audio.GetComponent<AudioSource>().clip = _audio.GetComponent<BGM>().bgm[2];
            _audio.GetComponent<AudioSource>().Play();
        }
        if(collision.gameObject.name== "L_Weapon_Boss"&& boss.GetComponent<Boss>().bossAtk==true)
        {
            Damage(boss.GetComponent<Boss>().atk);
            GameObject _text = Instantiate(Damagetext);
            _text.transform.position = _camera.WorldToScreenPoint(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 3));
            _text.GetComponent<Text>().text = Damage(boss.GetComponent<Boss>().atk).ToString();
        }
    }

}
