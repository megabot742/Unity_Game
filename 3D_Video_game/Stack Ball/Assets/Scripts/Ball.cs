using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Image = UnityEngine.UI.Image;

public class Ball : MonoBehaviour
{
    Rigidbody rb;
    float currentTime;
    bool smash, invincible;
    int currentBronkenStacks, totalStacks;
    [SerializeField] GameObject invincibleObj;
    [SerializeField] Image invincibleFill;
    [SerializeField] GameObject fireEffect, winEffect, splashEffect;
    public enum BallState
    {
        Prepare,
        Playing,
        Died,
        Finish
    }

    [HideInInspector]
    public BallState ballState = BallState.Prepare;

    public AudioClip bounceOffClip, deadClip, winClip, destoryClip, iDestroyClip;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentBronkenStacks = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        totalStacks = FindObjectsOfType<StackController>().Length;
    }

    // Update is called once per frame
    void Update()
    {
        if(ballState == BallState.Playing)
        {
            //click mouse left
            if(Input.GetMouseButtonDown(0))
            {
                smash = true;
            }
            //click mouse right
            else if(Input.GetMouseButtonDown(1))
            {
                smash = false;
            }

            if(invincible)
            {
                currentTime -= Time.deltaTime * 0.35f;
                if(!fireEffect.activeInHierarchy)
                    fireEffect.SetActive(true);
            }
            else
            {
                if(fireEffect.activeInHierarchy)
                    fireEffect.SetActive(false);
                //check smash
                if(smash)
                    currentTime += Time.deltaTime * 0.8f;
                else
                    currentTime -= Time.deltaTime * 0.5f;
            }
            if(currentTime >= 0.3f || invincibleFill.color == Color.red)
                invincibleObj.SetActive(true);
            else
                invincibleObj.SetActive(false);

            if(currentTime >= 1)
            {
                currentTime = 1;
                invincible = true;
                invincibleFill.color = Color.red;
            }
            else if(currentTime <= 0)
            {
                currentTime = 0;
                invincible = false;
                invincibleFill.color = Color.white;
            }
            if(invincibleObj.activeInHierarchy)
                invincibleFill.fillAmount = currentTime / 1;

        }
        if(ballState == BallState.Finish)
        {
            if(Input.GetMouseButtonDown(0))
                FindObjectOfType<LevelSpawner>().NextLevel();
        }
    }
    void FixedUpdate() //FixedUpdate is often used to handle physics-related tasks
    {
        if(ballState == BallState.Playing)
        {
            if(Input.GetMouseButton(0))
            {
                smash = true;
                rb.velocity = new Vector3(0, -100 * Time.fixedDeltaTime * 7, 0);
            }  
        }
        if(rb.velocity.y > 5) // maximum velocity
        {
            rb.velocity = new Vector3(rb.velocity.x, 5, rb.velocity.z);
        }
    }
    public void IncreaseBronkenStacks()
    {
        currentBronkenStacks++;
        if(!invincible)
        {
            ScoreManager.instance.AddScore(1);
            SoundManager.instance.PlaySoundFX(destoryClip, 0.5f);
        }
        else
        {
            ScoreManager.instance.AddScore(2);
            SoundManager.instance.PlaySoundFX(iDestroyClip, 0.5f);
        }
    }
    void OnCollisionEnter(Collision other) 
    {
        if(!smash)
        {
            rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5,0);
            if(other.gameObject.tag != "Finish")
            {
                GameObject splash = Instantiate(splashEffect);
                splash.transform.SetParent(other.transform);
                splash.transform.localEulerAngles = new Vector3(90, Random.Range(0, 359), 0);
                float randomScale = Random.Range(0.18f, 0.25f);
                splash.transform.localScale = new Vector3(randomScale, randomScale, 1);
                splash.transform.position = new Vector3(transform.position.x, transform.position.y - 0.22f, transform.position.z);
                splash.GetComponent<SpriteRenderer>().color = transform.GetChild(0).GetComponent<MeshRenderer>().material.color;
            }
            SoundManager.instance.PlaySoundFX(bounceOffClip, 0.5f);
        }
        else
        {
            if(invincible)
            {
                if(other.gameObject.tag == "enemy" || other.gameObject.tag == "plane")
                {
                    other.transform.parent.GetComponent<StackController>().ShatterAllPart();
                }
            }
            else
            {
                if(other.gameObject.tag == "enemy")
                {
                    other.transform.parent.GetComponent<StackController>().ShatterAllPart();
                }
                else if(other.gameObject.tag == "plane")
                {
                    rb.isKinematic = true;
                    transform.GetChild(0).gameObject.SetActive(false);
                    ballState = BallState.Died;
                    PlayerPrefs.DeleteKey("Level"); //Reset Level after die
                    SoundManager.instance.PlaySoundFX(deadClip, 0.5f);
                }
            }
        }
        FindObjectOfType<GameUI>().LevelSliderFill(currentBronkenStacks / (float)totalStacks);

        if(other.gameObject.tag == "Finish" && ballState == BallState.Playing)
        {
            ballState = BallState.Finish;
            SoundManager.instance.PlaySoundFX(winClip, 0.7f);
            GameObject win = Instantiate(winEffect);
            win.transform.SetParent(Camera.main.transform);
            win.transform.localPosition = Vector3.up * 1.5f;
            win.transform.eulerAngles = Vector3.zero;
        }
        
    }
    void OnCollisionStay(Collision other)
    {
        if(!smash || other.gameObject.tag == "Finish") // check for finish 
        {
            rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0); //reset velocity to normal
        }
    }
}
