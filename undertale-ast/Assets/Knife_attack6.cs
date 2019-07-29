using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Knife_attack6 : MonoBehaviour {

    public static float speed = 0.03f;
    bool left = false;
    bool right = false;

    float time;

    public Renderer knife;

    public GameObject knife_obj;

    private int count = 0;


    // Use this for initialization
    void Start () {

        if (gameObject.transform.position.x < -5)
        {
            left = true;
        }
        else if (gameObject.transform.position.x > 5)
        {
            right = true;
            gameObject.transform.Rotate(0, 0, 180);
        }


    }

    // Update is called once per frame
    void Update () {


        time += Time.deltaTime;
        Vector2 Position = transform.position;

        if (left)
        {

            Position.x += speed;
            transform.position = Position;
        }

        if (right)
        {

            Position.x -= speed;
            transform.position = Position;
        }

       

        if(time > 3f)
        {
            if(count == 0)
            {
                iTween.ScaleBy(knife_obj, iTween.Hash("y", 4.0f, "time", 1f));
                time = 0;
                count = 1; 

            }
            else if(count == 1)
            {
                iTween.ScaleBy(knife_obj, iTween.Hash("y", 0.25f, "time", 1f));
                time = 0;
                count = 0;
            }

        }


        if (Position.x < -10)
        {
            Destroy(gameObject);
        }


    }


   
}
