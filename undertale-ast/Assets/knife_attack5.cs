using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class knife_attack5 : MonoBehaviour {

    public static float speed = 0.5f;
    bool left = false;
    bool right = false;
    bool up = false;
    bool down = false;

    float time;

    public Renderer knife;




    // Use this for initialization
    void Start()
    {




        if (gameObject.transform.position.x < -5)
        {
            left = true;
        }
        else if (gameObject.transform.position.x > 5)
        {
            right = true;
            gameObject.transform.Rotate(0, 0, -180);
        }
        else if (gameObject.transform.position.y > 1)
        {
            up = true;
            gameObject.transform.Rotate(0, 0, -90);
        }
        else if (gameObject.transform.position.y < -2)
        {
            down = true;
            gameObject.transform.Rotate(0, 0, 90);
        }

    }


    public void ChangeSpeed(float s)
    {

        speed = s;

    }







    // Update is called once per frame
    void Update()
    {

        time += Time.deltaTime;

        if(time < 2.0f)
        {
            transform.Rotate(new Vector3(0, 0, 30f));
        }

        if(time >= 2.0f)
        {
            if (left)
            {
                Transform rotate = this.transform;
                Vector3 localangle = rotate.localEulerAngles;
                localangle.z = -90f;
                rotate.localEulerAngles = localangle;
            }

            if (right)
            {
                Transform rotate = this.transform;
                Vector3 localangle = rotate.localEulerAngles;
                localangle.z = 90f;
                rotate.localEulerAngles = localangle;
            }
        }



        knife.material.color = new Color(1f, 1f, 1f, time);



        if (time > 2.0f)
        {
            if (left)
            {
                Vector2 Position = transform.position;
                Position.x += speed;
                transform.position = Position;
            }

            if (right)
            {
                Vector2 Position = transform.position;
                Position.x -= speed;
                transform.position = Position;
            }

            if (up)
            {
                Vector2 Position = transform.position;
                Position.y -= speed;
                transform.position = Position;
            }

            if (down)
            {
                Vector2 Position = transform.position;
                Position.y += speed;
                transform.position = Position;
            }
        }




        if (time > 3f)
        {
            StartCoroutine("Timelag");
        }


    }



    private IEnumerator Timelag()
    {

        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);


    }


}
