using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossDisp : MonoBehaviour
{
    public List<Animator> cross;
    private bool[] status;
    public void sethealth(int health)
    {
        health = Mathf.Clamp(health, 0, 3);
        int numredcross = 3 - health;
        for (int i = 0; i < 3; i++)
        {
            bool cur_cros_should_red = numredcross >= (i + 1);
            if (cur_cros_should_red && !status[i])
            {
                cross[i].SetTrigger("turnred");
                status[i] = true;
            }
            else if(!cur_cros_should_red && status[i])
            {
                cross[i].SetTrigger("turnemp");
                status[i] = false;
            }
        }
    }

    private void Start()
    {
        status = new bool[3] { false, false, false };
    }

}
