using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class GameUI : MonoBehaviour
{
    [SerializeField] GameObject homeUI, inGameUI;
    [SerializeField] GameObject allButtons;
    bool buttons;

    [Header("PreGame")]
    [SerializeField] Button soundButton;
    [SerializeField] Sprite soundOnS, soundOffS;

    [Header("InGame")]
    [SerializeField] Image levelSlider;
    [SerializeField] Image currentLevelImg;
    [SerializeField] Image nextLevelImg;

    Material ballMat;
    Ball ball;

    // Start is called before the first frame update
    void Awake()
    {
        ballMat = FindObjectOfType<Ball>().transform.GetChild(0).GetComponent<MeshRenderer>().material;
        ball = FindObjectOfType<Ball>();

        levelSlider.transform.parent.GetComponent<Image>().color = ballMat.color + Color.gray;
        levelSlider.color = ballMat.color;
        currentLevelImg.color = ballMat.color;
        nextLevelImg.color = ballMat.color;

        soundButton.onClick.AddListener(() => SoundManager.instance.SoundOnOff());
    }


    // Update is called once per frame
    void Update()
    {
        if(ball.ballState == Ball.BallState.Prepare)
        {
            if(SoundManager.instance.sound && soundButton.GetComponent<Image>().sprite != soundOnS)
                soundButton.GetComponent<Image>().sprite = soundOnS;
            else if(!SoundManager.instance.sound && soundButton.GetComponent<Image>().sprite != soundOffS)
                soundButton.GetComponent<Image>().sprite = soundOffS;
        }


        if(Input.GetMouseButtonDown(0) && !IgnoreUI() && ball.ballState == Ball.BallState.Prepare)
        {
            ball.ballState = Ball.BallState.Playing;
            homeUI.SetActive(false);
            inGameUI.SetActive(true);
        }
    }
    bool IgnoreUI()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResultsList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResultsList);
        for(int i = 0; i < raycastResultsList.Count; i++)
        {
            if(raycastResultsList[i].gameObject.GetComponent<Ignore>() != null)
            {
                raycastResultsList.RemoveAt(i);
                i--;
            }
        }
        return raycastResultsList.Count > 0;
    }
    public void LevelSliderFill(float fillAmount)
    {
        levelSlider.fillAmount = fillAmount;
    }
    public void Setting()
    {
        buttons = !buttons;
        allButtons.SetActive(buttons);
    }
}
