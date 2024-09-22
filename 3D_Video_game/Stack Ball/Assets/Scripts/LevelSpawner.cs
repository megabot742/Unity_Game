using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] model;
    [HideInInspector]
    [SerializeField] GameObject[] modelPrefab = new GameObject[4];
    [SerializeField] GameObject WinPrefab;

    GameObject temp1, temp2;

    public int level = 1, addOn = 7;
    float i = 0;


    [SerializeField] Material plateMat, baseMat;
    [SerializeField] MeshRenderer ballMesh;
    void Awake()
    {
        plateMat.color = Random.ColorHSV(0, 1, 0.5f, 1, 1, 1);
        baseMat.color = plateMat.color + Color.gray;
        ballMesh.material.color = plateMat.color;
        
        level = PlayerPrefs.GetInt("Level", 1);
        //Check level
        if(level > 9)
        {
            addOn = 0;
        } 
        ModelSelection(); //Select model
        CreateModel(); //Create model  
    }
    void CreateModel()
    {
        float random = Random.value;
        //at the current ball position is 0, so the loop will go negative
        for(i = 0; i > -level-addOn; i-=0.5f) // create 16 loop in level = 1, addOn = 7
        {
            //check level and selects a model from the modelPrefab (only have model 0 -> 3)
            if(level <= 20)
            {
                temp1 = Instantiate(modelPrefab[Random.Range(0,2)]);
            }
            else if(level > 20 && level <= 50)
            {
                temp1 = Instantiate(modelPrefab[Random.Range(1,3)]);
            }
            else if(level > 50 && level <= 100)
            {
                temp1 = Instantiate(modelPrefab[Random.Range(2,4)]);
            }
            else //level > 100, case of too many levels, remember to add the model to modelPrefab
            {
                temp1 = Instantiate(modelPrefab[Random.Range(3,4)]);
            }
            temp1.transform.position = new Vector3(0, i - 0.01f, 0);
            temp1.transform.eulerAngles = new Vector3(0, i * 8, 0);

            if(Mathf.Abs(i) >= level * 0.3f && Mathf.Abs(i) <= level * 0.6f)
            {
                temp1.transform.eulerAngles = new Vector3(0, i * 8, 0);
                temp1.transform.eulerAngles += Vector3.up * 180;
            }else if(Mathf.Abs(i) >= level * 0.8f)
            {
                temp1.transform.eulerAngles = new Vector3(0, i * 8, 0);

                if(random > 0.75f)
                {
                    temp1.transform.eulerAngles += Vector3.up * 180;
                }
            }

            temp1.transform.parent = FindObjectOfType<Rotator>().transform;
        }
        //create Win prefab
        temp2 = Instantiate(WinPrefab);
        temp2.transform.position = new Vector3(0, i - 0.01f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            plateMat.color = Random.ColorHSV(0, 1, 0.5f, 1, 1, 1);
            baseMat.color = plateMat.color + Color.gray;
            ballMesh.material.color = plateMat.color;
        }
    }
    void ModelSelection() //random and select model 
    {
        int randomModel = Random.Range(0,5);
        switch (randomModel)
        {
            case 0: //mode 0 -> 3
                for (int i = 0; i < 4; i++)
                    modelPrefab[i] = model[i];
                break;
            
            case 1: //mode 3 -> 7
                for (int i = 0; i < 4; i++)
                    modelPrefab[i] = model[i + 4];
                break;
            
            case 2: //model 8 -> 11
                for (int i = 0; i < 4; i++)
                    modelPrefab[i] = model[i + 8];
                break;
            
            case 3: //model 12 -> 15
                for (int i = 0; i < 4; i++)
                    modelPrefab[i] = model[i + 12];
                break;
            
            case 4: //model 16 -> 19
                for (int i = 0; i < 4; i++)
                    modelPrefab[i] = model[i + 16];
                break;
        }
    }
    public void NextLevel()
    {
        int currentLevel = PlayerPrefs.GetInt("Level", 1);
        PlayerPrefs.SetInt("Level", currentLevel + 1);
        SceneManager.LoadScene(0);
    }
}


