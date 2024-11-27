using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class scripts : MonoBehaviour
{
    public Transform target;
    public Rigidbody self;
    public Rigidbody other;
    public bool bk;
    public bool bk1;
    // Start is called before the first frame update
    void Start()
    {
        bk = false;
        bk1 = false;
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void FixedUpdate()
    {
        //self.AddForce(target.position - transform.position);
        if (bk1)
        {
            bk1 = false;
            other.AddRelativeForce(new Vector3(10, 0, 0), ForceMode.Impulse);
            self.AddRelativeForce(new Vector3(-10, 0, 0), ForceMode.Impulse);
            Debug.Log("moved");

        }
        if (!bk)
        {
            if (Input.GetMouseButton(0))
            {
                bk = true;
                bk1 = true;
                //other.gameObject.transform.SetParent(target);
                
                other.AddRelativeForce(new Vector3(10, 0, 0), ForceMode.Impulse);
                self.AddRelativeForce(new Vector3(-10, 0, 0), ForceMode.Impulse);
                //self.AddExplosionForce(200, transform.position + new Vector3(1f, 0, 0), 2);
                //other.AddExplosionForce(200, transform.position + new Vector3(1f, 0, 0), 2);
                Debug.Log("exploaded");
            }
            //bk = true;
        }
    }
}
