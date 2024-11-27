using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class wholeFruitBehavior : MonoBehaviour
{
    public Rigidbody upprefab;
    public Rigidbody downprefab;
    public Rigidbody rbself;
    public FN_curser fc;
    public bool horizon_cut;
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("ent");
        Vector2 relativeSpeed = (new Vector2(rbself.velocity.x, rbself.velocity.y) - fc.curser_speed).normalized;
        //Vector2 relativeSpeed = fc.delta_mpos.normalized;
        if (horizon_cut)
        {
            Vector3 ori_cut_normal = new Vector3(transform.up.x,transform.up.y,0f);
            Vector3 cut_norm_dir = Vector3.Cross(new Vector3(relativeSpeed.x, relativeSpeed.y, 0f), new Vector3(0f, 0f, 1f));
            Vector3 new_norm_dir = Vector3.Dot(cut_norm_dir, ori_cut_normal) > 0 ? cut_norm_dir : -cut_norm_dir;
            //transform.rotation = transform.rotation * Quaternion.FromToRotation(ori_cut_normal, new_norm_dir);
            transform.rotation = Quaternion.FromToRotation(new Vector3(0f, 1f, 0f), new_norm_dir);
            becut();
        }
        else
        {
            becut();
        }
    }
    public void becut()
    {
        Rigidbody uprb = Instantiate(upprefab, transform.position, transform.rotation);
        uprb.velocity = rbself.velocity + rbself.transform.up.normalized*2f;
        uprb.angularVelocity = rbself.angularVelocity;

        Rigidbody dwrb = Instantiate(downprefab, transform.position, transform.rotation);
        dwrb.velocity = rbself.velocity - rbself.transform.up.normalized * 2f;
        dwrb.angularVelocity = rbself.angularVelocity;
        Destroy(gameObject);
    }

    private void Start()
    {
        fc = GameObject.FindGameObjectWithTag("FNcurser").GetComponent<FN_curser>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            becut();
        }
        if(transform.position.y < -7f)
        {
            Destroy(gameObject);
        }
    }
}
