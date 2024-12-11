using System;
using TMPro;
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
    public int health;
    private float lastcuttime;
    private float lastmisstime;
    private int combo;
    private int score;
    private int bestscore;
    public FNGameState current_game_state;
    public Rigidbody start_fruit_prefab;
    public Rigidbody home_fruit_prefab;
    public Animator HomeUIcontrol;
    public Animator GameUIcontrol;
    public Animator EndUIcontrol;
    public Transform NewgameText;
    public Transform HomeText;
    public Transform RetryText;
    public fruit_behavior fruitmgr;
    public CrossDisp crosscontroller;
    public TextMeshProUGUI EndscreenScoreDisplay;
    public GameObject newbestDisp;
    public cam_shake_controller cameraShakeController;
    private Rigidbody homefruit;
    private Rigidbody retryfruit;

    public void enterHome()
    {
        Rigidbody rb = Instantiate(start_fruit_prefab, NewgameText); 
        rb.gameObject.transform.localPosition = new Vector3(0,2.46f,0);
        rb.gameObject.transform.localScale = Vector3.one/NewgameText.localScale.x*100f;
        //rb.gameObject.transform.localScale = new Vector3(0.7f,0.7f,0.7f);
        rb.useGravity = false;
        rb.angularVelocity = new Vector3(UnityEngine.Random.Range(-0.2f, 0.2f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
        rb.gameObject.GetComponent<wholeFruitBehavior>().setid(999);//999 for start game fruit
        HomeUIcontrol.SetTrigger("openHomeScreen");
    }
    public void exitHome()
    {
        HomeUIcontrol.SetTrigger("closeHomeScreen");
    }
    public void enterGame()
    {
        GameUIcontrol.SetTrigger("openGameScreen");
        fruitmgr.startFruit();
        health = 3;
        score = 0;
        crosscontroller.sethealth(health);
    }
    public void exitGame()
    {
        GameUIcontrol.SetTrigger("closeGameScreen");
        fruitmgr.stopFruit();
    }
    public void enterEnd()
    {
        EndscreenScoreDisplay.text = score.ToString();
        homefruit = Instantiate(home_fruit_prefab, HomeText);
        homefruit.gameObject.transform.localPosition = new Vector3(0, 2.46f, 0);
        homefruit.gameObject.transform.localScale = Vector3.one / HomeText.localScale.x * 100f;
        homefruit.useGravity = false;
        homefruit.angularVelocity = new Vector3(UnityEngine.Random.Range(-0.2f, 0.2f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
        homefruit.gameObject.GetComponent<wholeFruitBehavior>().setid(998);//998 for home menu fruit

        retryfruit = Instantiate(home_fruit_prefab, RetryText);
        retryfruit.gameObject.transform.localPosition = new Vector3(0, 2.46f, 0);
        retryfruit.gameObject.transform.localScale = Vector3.one / RetryText.localScale.x * 100f;
        retryfruit.useGravity = false;
        retryfruit.angularVelocity = new Vector3(UnityEngine.Random.Range(-0.2f, 0.2f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
        retryfruit.gameObject.GetComponent<wholeFruitBehavior>().setid(997);//997 for retry fruit
        EndUIcontrol.SetTrigger("openEndScreen");
    }
    public void exitEnd()
    {
        EndUIcontrol.SetTrigger("closeEndScreen");
    }
    public void onCutEvent(Vector2 pos,int idx)
    {
        //Debug.Log(idx);
        switch (current_game_state)
        {
            case FNGameState.Home:
                if (idx == 999)//start game fruit id
                {
                    current_game_state = FNGameState.Game;
                    //transaction to game
                    exitHome();
                    enterGame();
                }
                break;
            case FNGameState.Game:
                lastcuttime = Time.realtimeSinceStartup;
                combo++;
                score++;
                break;
            case FNGameState.End:

                if (idx == 998)//home fruit id
                {
                    current_game_state = FNGameState.Home;
                    //transaction to game
                    exitEnd();
                    enterHome();
                    Destroy(retryfruit.gameObject);
                }
                if (idx == 997)//retry fruit id
                {
                    current_game_state = FNGameState.Game;
                    //transaction to game
                    exitEnd();
                    enterGame();
                    Destroy(homefruit.gameObject);
                }
                break;
        }
    }

    public void onMissEvent(int idx)
    {
        if (idx > 900) return;//message fruit
        switch (current_game_state)
        {
            case FNGameState.Game:
                if (Time.realtimeSinceStartup > lastmisstime+2)
                {
                    cameraShakeController.shakeCam(0.2f);
                    health -= 1;
                    crosscontroller.sethealth(health);
                    lastmisstime = Time.realtimeSinceStartup;
                    if (health <= 0)
                    {
                        if(score > bestscore)
                        {
                            bestscore = score;
                            newbestDisp.SetActive(true);
                        }
                        else
                        {
                            newbestDisp.SetActive(false);
                        }
                        current_game_state = FNGameState.End;
                        exitGame();
                        enterEnd();
                    }
                }
                combo = 0;
                break;
        }
    }
    public String getinfo()
    {
        //return "score:" + score.ToString() + "\n" + "health:" + health.ToString() + "\n" + Time.fixedDeltaTime.ToString();
        return "score:" + score.ToString() + "\n" + "Best:" + bestscore.ToString();
    }

    private void Start()
    {
        Time.fixedDeltaTime = 0.002f;
        enterHome();
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
                health += 1;
                if (health > 3) health = 3;
                crosscontroller.sethealth(health);
            }
            combo = 0;
        }
    }
}
