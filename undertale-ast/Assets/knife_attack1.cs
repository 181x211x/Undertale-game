using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class knife_attack1 : MonoBehaviour {

    public static　float speed = 0.2f;
    bool left = false;
    bool right = false;
    bool up = false;
    bool down = false;

    float time;





    // Use this for initialization
    void Start() {

        


        if (gameObject.transform.position.x < -5)
        {
            left = true;
        }
        else if (gameObject.transform.position.x > 5)
        {
            right = true;
            gameObject.transform.Rotate(0,0,-180);
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


    public void ChangeSpeed(float s) {

        speed = s;

    }







    // Update is called once per frame
    void Update () {

        time += Time.deltaTime;


        

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


        if (time > 2f)
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
