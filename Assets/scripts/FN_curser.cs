using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FN_curser : MonoBehaviour
{
    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("cut!"); 
    //}
    private Vector3 p_mwpos;
    public Vector2 curser_speed;

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        Vector3 mwpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        curser_speed = mwpos - p_mwpos;
        curser_speed /= Time.deltaTime;
        p_mwpos = mwpos;
        transform.position = new Vector3(mwpos.x, mwpos.y, transform.position.z);
    }
}
