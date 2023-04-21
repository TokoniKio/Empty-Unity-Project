using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ButtonClick : MonoBehaviour
{
    public bool buttonEnabled = true;
    private PlayerController playerController;
    public Button playButton;
    public TextMeshProUGUI textTitle;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = playButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonEnabled == true)
        {
            playButton.gameObject.SetActive(true);
            textTitle.gameObject.SetActive(true);
        }
        else 
        {
            playButton.gameObject.SetActive(false);
            textTitle.gameObject.SetActive(false);

        }
    }

    void TaskOnClick()
    {
        playerController.gameOver = false;
        buttonEnabled = false;
    }
}
