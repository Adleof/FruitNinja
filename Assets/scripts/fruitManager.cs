using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class fruit_behavior : MonoBehaviour
{
    class FruTimline
    {
        float entertime;
        List<float> fruittime;
        int idx;
        float duration;

        public FruTimline(List<float> f, float dur)
        {
            fruittime = f;
            duration = dur;
        }
        public void start_timeline()
        {
            entertime = Time.realtimeSinceStartup;
            idx = 0;
        }
        public void throwfruit_or_not(ref bool ended, ref bool thr)//return end or not
        {
            float ctp = Time.realtimeSinceStartup - entertime;
            thr = false;
            if (idx < fruittime.Count && ctp > fruittime[idx])
            {
                idx += 1;
                thr = true;
            }
            ended = ctp > duration;
        }
    }

    FruTimline wait_two;
    private List<FruTimline> patterns;
    FruTimline cur_pattern;

    public List<Rigidbody> fruitPrefab;
    private Rigidbody rb;
    int layer = 0;
    enum FNmgrState { stop, wait, throwfruit, fever };
    private float nextthrow;
    private float feverendtime;
    private FNmgrState curstate;

    public void enterfever(float fduration)
    {
        feverendtime = Time.realtimeSinceStartup + fduration;
        curstate = FNmgrState.fever;
        nextthrow = 0;
    }
    // Start is called before the first frame update
    void spawnFruit(int fid)
    {
        float spawnx = Random.Range(-7f, 7f);
        rb = Instantiate(fruitPrefab[fid],new Vector3(spawnx, -4f,-3f + layer*2),Quaternion.identity);
        layer++;
        if (layer > 5)
        {
            layer = 0;
        }
        rb.velocity = new Vector3(-spawnx*0.5f, 9.0f, 0.0f);
        rb.angularVelocity = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-3f, 3f), Random.Range(-1f, 1f));
        rb.gameObject.GetComponent<wholeFruitBehavior>().setid(fid);
    }

    public void startFruit()
    {
        curstate = FNmgrState.wait;
        wait_two.start_timeline();
    }
    public void stopFruit()
    {
        curstate = FNmgrState.stop;
    }

    private void Start()
    {
        curstate = FNmgrState.stop;
        wait_two = new FruTimline(new List<float> { }, 2f);
        patterns = new List<FruTimline>();
        patterns.Add(new FruTimline(new List<float> { 1, 2, 3 }, 4f));
        patterns.Add(new FruTimline(new List<float> { 1, 2, 3 }, 4f));
        patterns.Add(new FruTimline(new List<float> { 1, 2, 3 }, 4f));
        patterns.Add(new FruTimline(new List<float> { 1, 2, 3, 4, 5 }, 6f));
        patterns.Add(new FruTimline(new List<float> { 1, 2, 3, 4, 5 }, 6f));
        patterns.Add(new FruTimline(new List<float> { 1, 2, 3, 4, 5 }, 6f));
        patterns.Add(new FruTimline(new List<float> { 1, 1, 1 }, 2f));
        patterns.Add(new FruTimline(new List<float> { 1, 1.5f, 2, 2.5f, 3, 3.5f , 4}, 5f));
        patterns.Add(new FruTimline(new List<float> { 1, 1.2f, 1.4f, 1.5f }, 2.5f));
        patterns.Add(new FruTimline(new List<float> { 1, 1, 1.1f, 1.1f, 1.2f  }, 3f));
        patterns.Add(new FruTimline(new List<float> { 1, 1.1f, 1.2f, 1.3f, 1.4f, 1.5f, 1.6f, 1.7f, 1.8f }, 2.8f));
        patterns.Add(new FruTimline(new List<float> { 1, 1.25f, 1.5f, 1.75f, 2f, 2.25f, 2.5f, 2.75f, 3f }, 3.8f));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            spawnFruit(Random.Range(0, fruitPrefab.Count));
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            enterfever(15);
        }
        bool th = false;
        bool ended = false;
        switch (curstate)
        {
            case FNmgrState.wait:
                wait_two.throwfruit_or_not(ref ended, ref th);
                if (ended)
                {
                    curstate = FNmgrState.throwfruit;
                    cur_pattern = patterns[Random.Range(0,patterns.Count)];
                    cur_pattern.start_timeline();
                }
                break;
            case FNmgrState.throwfruit:
                cur_pattern.throwfruit_or_not(ref ended, ref th);
                if (th)
                {
                    spawnFruit(Random.Range(0, fruitPrefab.Count));
                }
                if (ended)
                {
                    curstate = FNmgrState.wait;
                    wait_two.start_timeline();
                }
                break;
            case FNmgrState.fever:
                float ct = Time.realtimeSinceStartup;
                if ( ct > nextthrow)
                {
                    nextthrow = ct + 0.1f;
                    spawnFruit(Random.Range(0, fruitPrefab.Count));
                }
                if (ct > feverendtime)
                {
                    curstate = FNmgrState.wait;
                    wait_two.start_timeline();
                }
                break;
        }
    }
}
