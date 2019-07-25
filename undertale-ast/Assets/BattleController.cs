using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleController : MonoBehaviour {

    public static string battle_state;

    public GameObject Slider;
    public GameObject Gage;
    public GameObject[] Player;
    public GameObject field;
    public Text contents_text;


    public Slider slider;
    public Slider Player_HP;
    public Slider Boss_HP;
    private bool updown_flag = true;
    private bool gage_flag = true;
    private bool attack_end_flag = false;
    private bool fieldset_flag = true;
    public static int attack_damage = 0;


    public Text HP_now;

    public GameObject Boss;
    public GameObject Damage_obj;
    public Text Damage_text;
    public float fadeTime = 0.5f;
    private float time;
    private float defense_time = 0f;
    public SpriteRenderer render_gage;


    private bool defence_flag = true;
    private bool defense_end_flag = false;

    public GameObject knife;
    public GameObject knife_orange;
    public GameObject knife_blue;
    public GameObject knife3;
    public GameObject knife4;
    public GameObject knife5;

    public GameObject stage_help;

    private GameObject[] knfe_copy = new GameObject[10];
    private GameObject[] stagehelp_copy = new GameObject[10];


    private GameObject[] knife_blue1_copy = new GameObject[10];
    private GameObject[] knife_blue2_copy = new GameObject[10];

    //private int count = 0;

    private int defence_count = 0;
    private int talk_count = 0;



    //private int current =0;

    public GameObject comment;
    public Text comment_text;


    private bool talk_flag = true;
    private bool select_flag = false;

    private float radius = 3.0f;
    private float speed = 1.0f;

    private float knife3_x;
    private float knife3_y;

    private bool isKnife3_flag = false;
    private bool isKnife5_flag = false;


    private float knife3_time;


    public static Transform target;

    private bool first_defense = true;

    public GameObject Boss_comment_obj;


    public AudioClip hit_sound;
    public AudioSource audioSource;

    public static int player_state = 0;

    // Use this for initialization
    void Start () {

        battle_state = "talk_state";

        target = field.transform;

        

    }
	
	// Update is called once per frame
	void Update () {

        //HP
        HP_now.text = ((int)Player_HP.value).ToString();

        knife3_time += Time.deltaTime;
        knife3_x = radius * Mathf.Sin(knife3_time * speed);
        knife3_y = radius * Mathf.Cos(knife3_time * speed);
        



        if (battle_state.Equals("default"))
        {
            DefaultState();
        }
        else if (battle_state.Equals("attack_state"))
        {
            AttackState();
        }
        else if (battle_state.Equals("defense_state"))
        {
            DefenseState();
        }
        else if (battle_state.Equals("talk_state"))
        {
            TalkState1();
        }






    }

    public string ReturnState()
    {
        return battle_state;
    }


    public void TalkState1()
    {
        if (talk_flag && talk_count < 2)
        {
            TextController text_boss = comment.GetComponent<TextController>();
            text_boss.SetNextSentence();
            Boss_comment_obj.SetActive(true);
            talk_flag = false;
            StartCoroutine("CommentTimelag");
        }
        else if(talk_count == 3)
        {
            battle_state = "defense_state";
        }





    }

    private IEnumerator CommentTimelag()
    {
        yield return new WaitForSeconds(3.0f);
        talk_flag = true;
        TalkState1();
        talk_count++;
    }




    public void DefaultState()
    {
        if (fieldset_flag)
        {

            iTween.ScaleBy(field, iTween.Hash("x", 2.5f, "delay", 1f));

            fieldset_flag = false;

            comment_text.text = "";

            Player[player_state].SetActive(false);

            StartCoroutine("ContentCommentTimelag");


           


        }

    }

    private IEnumerator ContentCommentTimelag()
    {
        yield return new WaitForSeconds(2.0f);
        contents_text.text = "＊CHARAは笑みを浮かべている...";
        select_flag = true;
    }



    public void AttackState()
    {
        



        if (updown_flag == true && gage_flag == true)
        {
            slider.value = slider.value + (float)1;
        }
        else if (updown_flag == false && gage_flag == true)
        {

            slider.value = slider.value - (float)1;
        }

        if (slider.value >= 100)
        {

            updown_flag = false;
        }

        if (slider.value <= 0)
        {
            updown_flag = true;
        }


        if (Input.GetKeyDown("space"))
        {
            gage_flag = false;
            attack_damage = 50 - System.Math.Abs((int)(slider.value - 50));
            //Debug.Log(attack_damage);

            Damage_obj.SetActive(true);
            Damage_text.text = attack_damage.ToString();
            iTween.PunchPosition(Damage_obj, iTween.Hash("y", 40f));

            iTween.PunchPosition(Boss, iTween.Hash("x", 1f));

            audioSource.PlayOneShot(hit_sound);

            attack_end_flag = true;

            
            for(int i = 0; i < attack_damage; i++)
            {
                Boss_HP.value--;
            }

            
        }

        if (attack_end_flag == true)
        {
            time += Time.deltaTime;
            
            //if (time < fadeTime)
           //{
                
                //float alpha = 1.0f - time / fadeTime;
                //Color color = render_gage.color;
                //color.a = alpha;
                //render_gage.color = color;
            //}
            //else
            //{
                Gage.SetActive(false);
                Slider.SetActive(false);

            //}

            if(time > 1f)
            {
                Damage_text.text = "";
                battle_state = "defense_state";
                time = 0;
                fieldset_flag = true;
                gage_flag = true;
                attack_end_flag = false;
                //Color color = render_gage.color;
                //color.a = 255f;
                slider.value = 1;


            }

        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("codeQ");
            gage_flag = true;

        }
    }


    public void DefenseState()
    {
        //Debug.Log("DefenseState");

        defense_time += Time.deltaTime;

        if (fieldset_flag)
        {
            iTween.ScaleBy(field, iTween.Hash("x", 0.4f, "time", 1f));
            //iTween.ScaleBy(field, iTween.Hash("y", 1.2f,"delay",1f));
            fieldset_flag = false;
            

            // 幅
            float width = field.GetComponent<SpriteRenderer>().bounds.size.x;
            width = width * 0.4f - 0.4f;
            //print("width: " + width);
            // 高さ
            float height = field.GetComponent<SpriteRenderer>().bounds.size.y;
            height = height * 1.0f - 0.4f;
            //print("height: " + height);
            // 中心x
            float center_x = field.GetComponent<SpriteRenderer>().transform.position.x;
            //print("center_x: " + center_x);
            // 中心y
            float center_y = field.GetComponent<SpriteRenderer>().transform.position.y;
            //print("center_y: " + center_y);



            player_state = 1;
            Player[player_state].SetActive(true);

            player_move player_Move = Player[player_state].GetComponent<player_move>();
            player_Move.SetField((center_y + height/2),(center_y - height/2),(center_x - width/2),(center_x + width/2));


            Vector3 position = new Vector3(0.0f, -1.6f, 0.0f);
            Player[player_state].transform.position = position;

            //Player[player_state].SetActive(false);
            //Boss_comment_obj.SetActive(false);

            player_move.player_state = "blue";

        }


        if (defence_flag && defense_time > 3f)
        {
            Debug.Log("knife_attack1");

            if (player_move.player_state == "red")
            {
                int value = Random.Range(0, 1 + 1);

                if (Boss_HP.value < 850)
                {
                    Boss_comment_obj.SetActive(true);
                    TextController text_boss = comment.GetComponent<TextController>();
                    text_boss.SetNextSentence();
                    Knife_attack_3();


                }
                else if (value == 0 || first_defense)
                {
                    comment_text.text = "";
                    Boss_comment_obj.SetActive(false);
                    Knife_attack_1();
                    first_defense = false;

                }
                else if (value == 1)
                {
                    Knife_attack_2();
                }
            }
            else if(player_move.player_state == "blue")
            {
                //Knife_attack_blue1();
                isKnife5_flag = true;   
                Knife_attack_blue1();
            }








            defence_flag = false;
        }

        if (defense_end_flag)
        {
            battle_state = "default";
            fieldset_flag = true;
            defense_time = 0;
            defense_end_flag = false;
            defence_flag = true;
            defence_count = 0;


        }


    }


    public void DeleteUI()
    {
        contents_text.text = "";
        Slider.SetActive(false);
        Gage.SetActive(false);
        field.SetActive(false);
        
    }


    public void SelectDecision(string choice)
    {
        if (select_flag)
        {
            if (choice.Equals("0"))
            {
                battle_state = "attack_state";
                DeleteUI();
                field.SetActive(true);
                Slider.SetActive(true);
                Gage.SetActive(true);
                select_flag = false;

            }
            else if (choice.Equals("1"))
            {

            }
            else if (choice.Equals("2"))
            {

            }
            else if (choice.Equals("3"))
            {

            }
        }

    }


    private void Knife_attack_1()
    {

        if (defence_count < 3)
        {

            Quaternion rote = Quaternion.Euler(0.0f, 0.0f, -90.0f);
            knfe_copy[0] = Instantiate(knife, new Vector3(-5.6f, Player[player_state].transform.position.y, 0.0f), rote);
            knfe_copy[1] = Instantiate(knife, new Vector3(5.6f, Player[player_state].transform.position.y, 0.0f), rote);
            knfe_copy[2] = Instantiate(knife, new Vector3(Player[player_state].transform.position.x, 2.1f, 0.0f), rote);
            knfe_copy[3] = Instantiate(knife, new Vector3(Player[player_state].transform.position.x, -3.1f, 0.0f), rote);


        }

        if(defence_count == 3 || defence_count == 5)
        {
            int value = Random.Range(0, 7 + 1);

            Quaternion rote = Quaternion.Euler(0.0f, 0.0f, -90.0f);
            knfe_copy[0] = Instantiate(knife, new Vector3(-10.6f, -0.4f, 0.0f), rote);
            knfe_copy[1] = Instantiate(knife, new Vector3(-10.6f, -0.7f, 0.0f), rote);
            knfe_copy[2] = Instantiate(knife, new Vector3(-10.6f, -1.0f, 0.0f), rote);
            knfe_copy[3] = Instantiate(knife, new Vector3(-10.6f, -1.3f, 0.0f), rote);
            knfe_copy[4] = Instantiate(knife, new Vector3(-10.6f, -1.6f, 0.0f), rote);
            knfe_copy[5] = Instantiate(knife, new Vector3(-10.6f, -1.9f, 0.0f), rote);
            knfe_copy[6] = Instantiate(knife, new Vector3(-10.6f, -2.2f, 0.0f), rote);
            knfe_copy[7] = Instantiate(knife, new Vector3(-10.6f, -2.5f, 0.0f), rote);
            knfe_copy[8] = Instantiate(knife, new Vector3(-10.6f, -2.8f, 0.0f), rote);

            

            Destroy(knfe_copy[value]);
            Destroy(knfe_copy[value + 1]);




        }

        if (defence_count == 4)
        {
            int value = Random.Range(0, 7 + 1);

            Quaternion rote = Quaternion.Euler(0.0f, 0.0f, -90.0f);
            knfe_copy[0] = Instantiate(knife, new Vector3(10.6f, -0.4f, 0.0f), rote);
            knfe_copy[1] = Instantiate(knife, new Vector3(10.6f, -0.7f, 0.0f), rote);
            knfe_copy[2] = Instantiate(knife, new Vector3(10.6f, -1.0f, 0.0f), rote);
            knfe_copy[3] = Instantiate(knife, new Vector3(10.6f, -1.3f, 0.0f), rote);
            knfe_copy[4] = Instantiate(knife, new Vector3(10.6f, -1.6f, 0.0f), rote);
            knfe_copy[5] = Instantiate(knife, new Vector3(10.6f, -1.9f, 0.0f), rote);
            knfe_copy[6] = Instantiate(knife, new Vector3(10.6f, -2.2f, 0.0f), rote);
            knfe_copy[7] = Instantiate(knife, new Vector3(10.6f, -2.5f, 0.0f), rote);
            knfe_copy[8] = Instantiate(knife, new Vector3(10.6f, -2.8f, 0.0f), rote);

            Destroy(knfe_copy[value]);
            Destroy(knfe_copy[value + 1]);

        }

        if(defence_count == 6)
        {
            int value1 = Random.Range(0, 4 + 1);
            int value2 = Random.Range(5, 8 + 1);

            Quaternion rote = Quaternion.Euler(0.0f, 0.0f, -90.0f);
            knfe_copy[0] = Instantiate(knife, new Vector3(-10.6f, -0.4f, 0.0f), rote);
            knfe_copy[1] = Instantiate(knife, new Vector3(-10.6f, -1.0f, 0.0f), rote);
            knfe_copy[2] = Instantiate(knife, new Vector3(-10.6f, -1.6f, 0.0f), rote);
            knfe_copy[3] = Instantiate(knife, new Vector3(-10.6f, -2.2f, 0.0f), rote);
            knfe_copy[4] = Instantiate(knife, new Vector3(-10.6f, -2.8f, 0.0f), rote);
            knfe_copy[5] = Instantiate(knife, new Vector3(10.6f, -0.7f, 0.0f), rote);
            knfe_copy[6] = Instantiate(knife, new Vector3(10.6f, -1.3f, 0.0f), rote);
            knfe_copy[7] = Instantiate(knife, new Vector3(10.6f, -1.9f, 0.0f), rote);
            knfe_copy[8] = Instantiate(knife, new Vector3(10.6f, -2.5f, 0.0f), rote);

            Destroy(knfe_copy[value1]);
            Destroy(knfe_copy[value2]);


        }

        defence_count++;



        StartCoroutine("Timelag");


    }

    private IEnumerator Timelag()
    {
        if(defence_count >= 7)
        {

            yield return new WaitForSeconds(3.0f);


            defense_end_flag = true;
            Destroy(knfe_copy[0]);
            Destroy(knfe_copy[1]);
            Destroy(knfe_copy[2]);
            Destroy(knfe_copy[3]);
            Destroy(knfe_copy[4]);
            Destroy(knfe_copy[5]);
            Destroy(knfe_copy[6]);
            Destroy(knfe_copy[7]);
            Destroy(knfe_copy[8]);

            defence_count = 0;


        }
        else
        {

            yield return new WaitForSeconds(2.0f);

            Knife_attack_1();


        }


    }


    private void Knife_attack_2()
    {
        int value1 = Random.Range(0, 1 + 1);


        if (value1 == 0)
        {
            knfe_copy[0] = Instantiate(knife_orange, new Vector3(-10.6f, -2.0f, 0.0f), Quaternion.identity);
            knfe_copy[1] = Instantiate(knife_blue, new Vector3(10.6f, -2.0f, 0.0f), Quaternion.identity);

        }
        else if(value1 == 1)
        {
            knfe_copy[0] = Instantiate(knife_blue, new Vector3(-10.6f, -2.0f, 0.0f), Quaternion.identity);
            knfe_copy[1] = Instantiate(knife_orange, new Vector3(10.6f, -2.0f, 0.0f), Quaternion.identity);
        }

       



        defence_count++;

        StartCoroutine("Knife_attack_2_timelag");


    }

    private IEnumerator Knife_attack_2_timelag()
    {

        if (defence_count == 4)
        {
            yield return new WaitForSeconds(5.0f);

            defense_end_flag = true;
            defence_count = 0;


        }
        else
        {
            yield return new WaitForSeconds(0.8f);

            Knife_attack_2();
        }





    }


    private void Knife_attack_3()
    {
        isKnife3_flag = true;
        knfe_copy[0] = Instantiate(knife3, new Vector3(knife3_x + 0.0f, knife3_y  - 1.6f, 0.0f), Quaternion.identity);
        radius -= 0.01f;
        defence_count++;
        StartCoroutine("Knife_attack_3_timelag");



    }

    private IEnumerator Knife_attack_3_timelag()
    {

        if(defence_count > 200)
        {
            yield return new WaitForSeconds(10.0f);
            defense_end_flag = true;
            defence_count = 0;
            knife3_time = 0;
            isKnife3_flag = false;
            Player[player_state].SetActive(false);
            Boss_comment_obj.SetActive(false);
            player_state = 1;
            player_move.player_state = "blue";


        }
        else
        {
            yield return new WaitForSeconds(0.1f);
            Knife_attack_3();
        }


    }


    private void Knife_attack_blue1()
    {

        StartCoroutine("Knife_attack_blue_set");
        StartCoroutine("Knife_attack_blue_stage_help");
        StartCoroutine("Knife_attack_blue_1");
        StartCoroutine("Knife_attack_blue_2");


    }


    private IEnumerator Knife_attack_blue_set()
    {
        fieldset_flag = true;
        iTween.ScaleBy(field, iTween.Hash("x", 2.5f, "time", 1f));
        //iTween.ScaleBy(field, iTween.Hash("y", 1.2f,"delay",1f));
        fieldset_flag = false;
        yield return new WaitForSeconds(0.8f);

    }

    private IEnumerator Knife_attack_blue_stage_help()
    {
        if (isKnife5_flag)
        {
            yield return new WaitForSeconds(2.0f);
            Stage_help();
        }

    }

    private void Stage_help()
    {
        //Quaternion rote = Quaternion.Euler(0.0f, 0.0f, -90.0f);
        stagehelp_copy[0] = Instantiate(stage_help, new Vector3(-8.6f, -2.3f, 0.0f), Quaternion.identity);
        stagehelp_copy[1] = Instantiate(stage_help, new Vector3(8.6f, -1.1f, 0.0f), Quaternion.identity);

        StartCoroutine("Knife_attack_blue_stage_help");


    }

    private IEnumerator Knife_attack_blue_1()
    {
        if (isKnife5_flag)
        {
            yield return new WaitForSeconds(0.5f);
            Knife_blue_1();

        }


    }

    private void Knife_blue_1()
    {
        knife_blue1_copy[0] = Instantiate(knife4, new Vector3(-6.0f, -2.8f, 0.0f), Quaternion.identity);
        StartCoroutine("Knife_attack_blue_1");
    }



    private IEnumerator Knife_attack_blue_2()
    {

        yield return new WaitForSeconds(1.5f);
        Knife_blue_2();


    }

    private void Knife_blue_2()
    {
        int value1 = Random.Range(0, 3 + 1);
        defence_count++;

        if (value1 == 0)
        {
            knife_blue2_copy[0] = Instantiate(knife5, new Vector3(-8.0f, -1.8f, 0.0f), Quaternion.identity);
        }
        else if (value1 == 1)
        {
            knife_blue2_copy[0] = Instantiate(knife5, new Vector3(8.0f, -1.8f, 0.0f), Quaternion.identity);
        }
        else if (value1 == 2)
        {
            knife_blue2_copy[0] = Instantiate(knife5, new Vector3(-8.0f, -0.8f, 0.0f), Quaternion.identity);
        }
        else if (value1 == 3)
        {
            knife_blue2_copy[0] = Instantiate(knife5, new Vector3(8.0f, -0.8f, 0.0f), Quaternion.identity);
        }


        if(defence_count < 10)
        {
            StartCoroutine("Knife_attack_blue_2");
        }
        else
        {
            isKnife5_flag = false;
            StartCoroutine("Knife_attack_blue_2_timelag2");



        }






    }

    private IEnumerator Knife_attack_blue_2_timelag2()
    {

        yield return new WaitForSeconds(7.0f);
        defense_end_flag = true;
        defence_count = 0;
        knife3_time = 0;
        Boss_comment_obj.SetActive(false);
        fieldset_flag = true;
        iTween.ScaleBy(field, iTween.Hash("x", 0.4f, "time", 1f));
        fieldset_flag = false;


    }



}
