using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_help : MonoBehaviour {

    private float speed = 0.05f;
    bool left = false;
    bool right = false;
    //bool up = false;
    //bool down = false;

    float time;


    // Use this for initialization
    void Start () {


        if (gameObject.transform.position.x < -5)
        {
            left = true;
        }
        else if (gameObject.transform.position.x > 5)
        {
            right = true;
            gameObject.transform.Rotate(0, 0, -180);
        }

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


        if (time > 9f)
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
