using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboDisp : MonoBehaviour
{
    public List<Sprite> sps;
    public SpriteRenderer myspr;
    private float stt;

    public void setnum(int num)
    {
        stt = Time.realtimeSinceStartup;
        myspr.sprite = sps[num-3];
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup - stt > 1.5f)
        {
            Destroy(gameObject);
        }
    }
}
