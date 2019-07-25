using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_move : MonoBehaviour {


    
    public Vector2 SPEED = new Vector2(0.05f, 0.05f);

    public static double maxfield_left = -1.75;
    public static double maxfield_right = 1.8;
    public static double maxfield_up = -0.2;
    public static double maxfield_down = -2.75;

    public float speed = 4;

    public static float field_left;
    public static float field_right;
    public static float field_up;
    public static float field_down;

    private float x_prev = 0;
    private float y_prev = 0;

    public static string player_state = "red";

    public static bool player_moving = false;


    public float flap = 1000f;

    public Rigidbody2D rb2d;

    private int count;



    private float jumptime;
    private bool jump_flag;
    private bool falling;

    private int jump_count = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (x_prev.Equals(transform.position.x) && y_prev.Equals(transform.position.y))
        {
            player_moving = false;
            //Debug.Log("false");

        }
        else if (!x_prev.Equals(transform.position.x ) || !y_prev.Equals(transform.position.y))
        {
            player_moving = true;
            //Debug.Log("true");
        }






        if (player_state.Equals("red"))
        {
            Vector2 Position = transform.position;

            x_prev = Position.x;
            y_prev = Position.y;



            // 右・左
            float x = Input.GetAxisRaw("Horizontal");

            // 上・下
            float y = Input.GetAxisRaw("Vertical");

            // 移動する向きを求める
            Vector2 direction = new Vector2(x, y).normalized;



            GetComponent<Rigidbody2D>().velocity = direction * speed;



            transform.position = Position;
        }
        else if (player_state.Equals("blue"))
        {




            // 右・左
            float x = Input.GetAxisRaw("Horizontal");

            // 上・下
            float y = 0f;

            // 移動する向きを求める
            Vector2 direction = new Vector2(x, y).normalized;



            GetComponent<Rigidbody2D>().velocity = direction * speed;





            if (Input.GetKeyDown("space") && !falling)
            {
                falling = true;
                Debug.Log("Jump!!");
                jump_count = 0;
                StartCoroutine("Jump");
            }




        }





        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("x: " + transform.position.x);
            Debug.Log("y: " + transform.position.y);
        }

        
    }


    private IEnumerator Jump()
    {

        yield return new WaitForSeconds(0.001f);
        Jumpimg();
    }

    private void Jumpimg()
    {
        jump_count++;
        if(jump_count< 12)
        {
            Vector2 Position = transform.position;
            Position.y += 0.15f;
            transform.position = Position;
            StartCoroutine("Jump");
        }
        else
        {

        }

    }

    public void SetField(float up, float down, float left, float right)
    {
        field_down = down;
        field_left = left;
        field_up = up;
        field_right = right;
    }

    void OnTriggerEnter2D(Collider2D other)
    {


        if (other.gameObject.name.Equals("ground"))
        {
            falling = false;
        }


    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.name.Equals("stage_help2(Clone)"))
        { 
            falling = false;
        }


    }





}

