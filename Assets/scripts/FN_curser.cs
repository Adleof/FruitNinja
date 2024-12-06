using System;
using UnityEngine;

public enum FNGameState { Home, Game, End };
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
    public FNGameState current_game_state;
    public Rigidbody start_fruit_prefab;
    public GameObject homeUI;
    public GameObject gameUI;
    public GameObject endUI;

    public void onCutEvent(Vector2 pos,int idx)
    {
        //Debug.Log(idx);
        switch (current_game_state)
        {
            case FNGameState.Home:
                if (idx == 999)//start game fruit id
                {
                    //Debug.Log("gamestart");
                    current_game_state = FNGameState.Game;
                    //transaction to game
                    homeUI.SetActive(false);
                    gameUI.SetActive(true);
                }
                break;
            case FNGameState.Game:
                lastcuttime = Time.realtimeSinceStartup;
                combo++;
                score++;
                break;
            case FNGameState.End:
                break;
        }
    }
    public String getinfo()
    {
        return "score:" + score.ToString() + "\n" + Time.fixedDeltaTime.ToString();
    }

    private void Start()
    {
        Time.fixedDeltaTime = 0.002f;
        Rigidbody rb = Instantiate(start_fruit_prefab, new Vector3(0f, -1.4f, -10f), Quaternion.identity);
        //rb.gameObject.transform.localScale = new Vector3(0.7f,0.7f,0.7f);
        rb.useGravity = false;
        rb.angularVelocity = new Vector3(UnityEngine.Random.Range(-0.2f, 0.2f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
        rb.gameObject.GetComponent<wholeFruitBehavior>().setid(999);//999 for start game fruit
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
