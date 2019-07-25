using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Controller : MonoBehaviour {

    public Slider Player_HP;
    public GameObject player;
    private bool isDamaged = false;
    private float time;
    private float damage_time;

    public static bool player_damaged = false;


    public Rigidbody2D rg;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        time += Time.deltaTime;

        if (isDamaged && (time - damage_time) < 1.7f)
        {
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            player.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, level);
            player_damaged = true;
        }

        if((time - damage_time) > 1.7f)
        {
            isDamaged = false;
            player.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f,1);
            player_damaged = false;
        }

    }


    void OnTriggerEnter2D(Collider2D collision)
    {

        //Debug.Log("攻撃を受けた物体:"+collision.name);

        if(collision.name != "ground" && !collision.name.Equals("stage_help2(Clone)"))
        {
            if (!player_damaged)
            {
                player_damaged = true;
                isDamaged = true;
                if (collision.gameObject.name.Contains("orange"))
                {
                    Debug.Log(player_move.player_moving);
                    if (!player_move.player_moving)
                    {
                        Debug.Log("Hit");
                        Player_HP.value = Player_HP.value - 7;
                        float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
                        player.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, level);
                        damage_time = time;
                    }

                }
                else if (collision.gameObject.name.Contains("blue"))
                {
                    Debug.Log(player_move.player_moving);
                    if (player_move.player_moving)
                    {
                        Debug.Log("Hit");
                        Player_HP.value = Player_HP.value - 7;
                        float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
                        player.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, level);
                        damage_time = time;
                    }
                }
                else
                {
                    Debug.Log("Hit");
                    Player_HP.value = Player_HP.value - 7;
                    float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
                    player.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, level);
                    damage_time = time;
                }
            }


        
        }

    }




}
