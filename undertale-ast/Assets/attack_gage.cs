using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class attack_gage : MonoBehaviour {

    public  Slider slider;
    private bool updown_flag= true;
    private bool gage_flag = true;
    public static int attack_damage = 0;


    

    // Use this for initialization
    void Start () {
        slider.value = 0;
	}
	
	// Update is called once per frame
	void Update () {


        



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
                Debug.Log(attack_damage);

            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("codeQ");
                gage_flag = true;

            }
        

        



    }

    public void resetGage()
    {
        slider.value = 0;
    }
}
