using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Pathfinding;

public class Player : MonoBehaviour
{
    public SkeletonAnimation SpineAnimation;
    public GameObject PlayerSelf;
    private Rigidbody2D rb;
    IEnumerator randomWalk;
    private bool isWalk;
    private bool touchWall = false;
    private int flyint;
    //寻路算法
    public AIPath aiPath;
    public GameObject yugang;
    // Start is called before the first frame update
    void Start()
    {
        isWalk = true;
        rb = GetComponent<Rigidbody2D>();
        SpineAnimation.AnimationName = "walk";
        //SpineAnimation.loop = true;
        randomWalk = RandomWalk();
        //StartCoroutine(randomWalk);
    }

    // Update is called once per frame
    void Update()
    {
        if (aiPath.desiredVelocity.x>0.01f)//调整ai寻路时玩家方向
        {
            transform.localScale = new Vector3(1f,1f,1f);
        }
        else if (aiPath.desiredVelocity.x < -0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        //测试用。检测玩家和木浴缸距离。触发动作
        if (Vector3.Distance(PlayerSelf.transform.position,yugang.transform.position)<1.5)
        {
            aiPath.maxSpeed = 0;
            PlayerSelf.transform.position = new Vector3(5.93f,-0.74f,0);
            SpineAnimation.AnimationName = "wash";
        }
    }
    void Walk()
    {
        rb.velocity = new Vector2(-0.5f, 0.5f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "wall1":
                StopCoroutine(randomWalk);
                rb.velocity = new Vector2(0, 0);
                SpineAnimation.AnimationName = "stand";
                Debug.Log("hitwall1");
                StartCoroutine(Flyfromwall(1));
                break;
            case "wall2":
                StopCoroutine(randomWalk);
                rb.velocity = new Vector2(0, 0);
                SpineAnimation.AnimationName = "stand";
                Debug.Log("hitwall2");
                StartCoroutine(Flyfromwall(2));
                break;
            case "wall3":
                StopCoroutine(randomWalk);
                rb.velocity = new Vector2(0, 0);
                SpineAnimation.AnimationName = "stand";
                Debug.Log("hitwall3");
                StartCoroutine(Flyfromwall(3));
                break;
            case "wall4":
                StopCoroutine(randomWalk);
                rb.velocity = new Vector2(0, 0);
                SpineAnimation.AnimationName = "stand";
                Debug.Log("hitwall4");
                StartCoroutine(Flyfromwall(4));
                break;
        }
    }
    IEnumerator RandomWalk()
    {
        while (isWalk)
        {
            //开始一段随机走路
            float time = (float)Random.Range(200, 501) / 100;
            float x = (float)Random.Range(-100, 100) / 100;
            int yrandom = Random.Range(0, 2);
            float y = 0;
            if (yrandom == 0)
            {
                y = Mathf.Sqrt(1f - x * x);
            }
            else
            {
                y = -Mathf.Sqrt(1f - x * x);
            }
            if (x > 0)
            {
                PlayerSelf.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                PlayerSelf.transform.localScale = new Vector3(-1, 1, 1);
            }
            rb.velocity = new Vector2(x / 2, y / 2);
            yield return new WaitForSeconds(time);
            rb.velocity = new Vector2(0, 0);
            SpineAnimation.AnimationName = "stand";
            yield return new WaitForSeconds(2);
            SpineAnimation.AnimationName = "walk";

        }
    }
    IEnumerator Flyfromwall(int x)
    {
        yield return new WaitForSeconds(3);
        int direct = Random.Range(0,3);
        float time = 0;
        time = (float)Random.Range(300, 500) / 100;
        Debug.Log("开始移动");
        SpineAnimation.AnimationName = "walk";
        switch (x)
        {
            case 1://左下是1逆时针排列
                switch (direct)
                {
                    case 0:
                        rb.velocity = new Vector2(0,0.5f);
                        break;
                    case 1:
                        rb.velocity = new Vector2(0.5f, 0.5f);
                        PlayerSelf.transform.localScale = new Vector3(1, 1, 1);
                        break;
                    case 2:
                        rb.velocity = new Vector2(0.5f, 0);
                        PlayerSelf.transform.localScale = new Vector3(1, 1, 1);
                        break;
                }
                break;
            case 2://左下是1逆时针排列
                switch (direct)
                {
                    case 0:
                        rb.velocity = new Vector2(0, 0.5f);
                        break;
                    case 1:
                        rb.velocity = new Vector2(-0.5f, 0.5f);
                        PlayerSelf.transform.localScale = new Vector3(-1, 1, 1);
                        break;
                    case 2:
                        rb.velocity = new Vector2(-0.5f, 0);
                        PlayerSelf.transform.localScale = new Vector3(-1, 1, 1);
                        break;
                }
                break;
            case 3://左下是1逆时针排列
                switch (direct)
                {
                    case 0:
                        rb.velocity = new Vector2(0, -0.5f);
                        break;
                    case 1:
                        rb.velocity = new Vector2(-0.5f, -0.5f);
                        PlayerSelf.transform.localScale = new Vector3(-1, 1, 1);
                        break;
                    case 2:
                        rb.velocity = new Vector2(-0.5f, 0);
                        PlayerSelf.transform.localScale = new Vector3(-1, 1, 1);
                        break;
                }
                break;
            case 4://左下是1逆时针排列
                switch (direct)
                {
                    case 0:
                        rb.velocity = new Vector2(0, -0.5f);
                        break;
                    case 1:
                        rb.velocity = new Vector2(0.5f, -0.5f);
                        PlayerSelf.transform.localScale = new Vector3(1, 1, 1);
                        break;
                    case 2:
                        rb.velocity = new Vector2(0.5f, 0);
                        PlayerSelf.transform.localScale = new Vector3(1, 1, 1);
                        break;
                }
                break;
        }
        yield return new WaitForSeconds(time);
        rb.velocity = new Vector2(0, 0);
        SpineAnimation.AnimationName = "stand";
        yield return new WaitForSeconds(2f);
        StartCoroutine(randomWalk);
        yield break;
    }
}
