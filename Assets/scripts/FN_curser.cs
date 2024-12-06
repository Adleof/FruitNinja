using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FN_curser : MonoBehaviour
{
    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("cut!"); 
    //}
    private Vector3 p_mwpos;
    public Vector2 curser_speed;
    public ComboDisp comboprefab;
    public Rigidbody selfrb;
    private float lastcuttime;
    private int combo;
    private int score;

    public void onCutEvent(Vector2 pos,int idx)
    {
        //Debug.Log(pos.ToString() + "id:" + idx.ToString());
        lastcuttime = Time.realtimeSinceStartup;
        combo++;
        score++;
    }
    public String getinfo()
    {
        return "score:" + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        Vector3 mwpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        curser_speed = mwpos - p_mwpos;
        curser_speed /= Time.deltaTime;
        p_mwpos = mwpos;
        //transform.position = new Vector3(mwpos.x, mwpos.y, transform.position.z);
        selfrb.MovePosition(new Vector3(mwpos.x, mwpos.y, transform.position.z));
        if (Time.realtimeSinceStartup - lastcuttime > 0.2f)
        {
            if (combo >= 3)
            {
                //Debug.Log("combo:" + combo.ToString());
                ComboDisp nc = Instantiate(comboprefab,new Vector3(Mathf.Clamp(transform.position.x,-6f,6f), Mathf.Clamp(transform.position.y,-2.5f,2.5f), -10),Quaternion.identity);
                nc.setnum(Math.Min(combo,6));
                score += combo * 2;
            }
            combo = 0;
        }
    }
}
