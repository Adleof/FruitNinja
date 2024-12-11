using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FN_vfx_ctrl : MonoBehaviour
{
    // Start is called before the first frame update 
    public VisualEffect effe; 
    private float efxstt;
    public float timepass;
    public int liveparticle;
    public void setspd_col(Vector2 vel, Color color)
    {
        //Debug.Log(color.ToString());
        effe.SetVector2("vel_bias",vel);
        effe.SetVector4("color", color);
    }
    private void Start()
    {
        efxstt = Time.realtimeSinceStartup;
    }
    private void Update()
    {
        //Debug.Log(efxalive.ToString() + "," + effectbig.aliveParticleCount.ToString() +","+ effectsml.aliveParticleCount.ToString());
        //effectbig.
        timepass = Time.realtimeSinceStartup - efxstt;
        liveparticle = effe.aliveParticleCount;
        if (timepass > 5 || (timepass > 0.5f && effe.aliveParticleCount == 0))
        {
            Destroy(gameObject);
        }
    }
}
