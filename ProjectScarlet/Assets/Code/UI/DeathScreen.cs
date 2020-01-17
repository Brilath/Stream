using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private float countDown;
    [SerializeField] private Text countDownText;

    private void OnEnable() 
    {

    }

    void Update()
    {
        countDown -= Time.deltaTime;

        countDownText.text = "Repawn: " + countDown.ToString("F0");

        if(countDown <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void CountDown(float seconds)
    {
        countDown = seconds;
    }
}
