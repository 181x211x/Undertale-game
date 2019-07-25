using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class knife_attack3 : MonoBehaviour {

    public float speed = 0.3f;
    private float time;

    private bool isKnife3_flag = false;

    // Use this for initialization
    void Start (){

        

        Vector2 vec = (BattleController.target.position - gameObject.transform.position).normalized;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, vec);

        

    }
	
	// Update is called once per frame
	void Update () {

        time += Time.deltaTime;


        if (isKnife3_flag)
        {
            gameObject.transform.Translate(Vector3.up * speed);

        }

        if (time > 6.0f)
        {
            isKnife3_flag = true;

        }

        if (time > 10f)
        {
            Destroy(gameObject);
        }



    }





}
