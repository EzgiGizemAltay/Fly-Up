using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;
using System.Threading;
using System.Threading.Tasks;

public class SwitchHandler : MonoBehaviour
{
    private int switchState = 1;
    [SerializeField] private GameObject switchBtn;
    [SerializeField] private Texture red, green;
    [SerializeField] private GameObject background;
    private float t = 0;
    [SerializeField] private float speed = 0.01f;
    public void OnSwitchButtonClicked()
    {
        switchState = Math.Sign(-switchBtn.transform.localPosition.x);


        switchBtn.transform.localPosition = new Vector3(Mathf.Lerp(-switchBtn.transform.localPosition.x, switchBtn.transform.localPosition.x, t += speed * Time.deltaTime), 0f, 0f);

        if (switchBtn.transform.localPosition.x < 0)
        {
            background.GetComponent<RawImage>().texture = red;
        }
        else if (switchBtn.transform.localPosition.x > 0)
        {
            background.GetComponent<RawImage>().texture = green;
        }
    }
}