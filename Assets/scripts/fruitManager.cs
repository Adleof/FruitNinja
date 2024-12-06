using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fruit_behavior : MonoBehaviour
{
    public List<Rigidbody> fruitPrefab;
    private Rigidbody rb;
    int layer = 0;
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

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.m 
        //if (Input.GetKeyDown(KeyCode.S))
        if (Input.GetMouseButtonDown(0))
        {
            spawnFruit(Random.Range(0,fruitPrefab.Count));
        }
    }
}
