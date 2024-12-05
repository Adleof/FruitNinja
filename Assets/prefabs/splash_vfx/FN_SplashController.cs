using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FN_SplashController : MonoBehaviour
{
    public SpriteRenderer sr;
    public SpriteRenderer sr_ring;
    public List<Sprite> sprites;
    public Transform slash;
    public float stt;
    public void setcolor(Color color)
    {
        sr.color = color;
        sr_ring.color = color;
    }
    public void setdir(Quaternion rot)
    {
        slash.rotation = rot*Quaternion.Euler(new Vector3(0f,0f,90f));
        sr_ring.transform.rotation = slash.rotation;
    }
    // Start is called before the first frame update
    void Start()
    {
        stt = Time.realtimeSinceStartup;
        sr.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, UnityEngine.Random.Range(0, 360)));
        sr.sprite = sprites[UnityEngine.Random.Range(0, sprites.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup - stt > 3.2f)
        {
            Destroy(gameObject);
        }
    }
}
