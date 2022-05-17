using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using SirketAdi.ProjeAdi.Utils;
using SirketAdi.ProjeAdi.Core;
public class GameCanvasControl : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI MoneyCountText;
    [SerializeField] private Slider NavBar;
    private float _velocityUpNav = 1f;
    private float moneyCount = 0;

    private bool GameCanvasOpen=false;
    
    
    void OnEnable()
    {
        EventBus<GameCanvasOpenEvent>.AddListener(OnGameCanvasOpen);
        EventBus<GameEndEvent>.AddListener(OnGameEnd);
    }
    
    void OnDisable()
    {
        EventBus<GameCanvasOpenEvent>.RemoveListener(OnGameCanvasOpen);
        EventBus<GameEndEvent>.RemoveListener(OnGameEnd);
    }

    private void Start()
    {
        NavBar.value = 0;
    }

    private void Update()
    {
        Debug.Log("Navbarvalue:.........."+NavBar.value);
        if (Input.GetMouseButtonDown(0))
        {
            moneyCount++;
            _velocityUpNav += 0.2f;
        }
        else
        {
            _velocityUpNav -= 0.02f;
        }
        if (GameCanvasOpen)
        {
            var totaltime = 111.989f / _velocityUpNav;
            var navbarpercentload = 1/totaltime;
            NavBar.value += navbarpercentload * Time.deltaTime;
            MoneyCountText.text = moneyCount.ToString(CultureInfo.InvariantCulture); 
        }

        /*if (NavBar.value >= 0.943)
        {
            StartCoroutine(WaitandZero(1));
        }*/
    }
    private void OnGameCanvasOpen(GameCanvasOpenEvent e)
    {
        GameCanvasOpen = true;
    }
    
    private void OnGameEnd(GameEndEvent e)
    {
        NavBar.value = 0;
        moneyCount = 0;
        _velocityUpNav = 1;
    }
    IEnumerator WaitandZero(float wait)
    {
        yield return new WaitForSeconds(wait);
        NavBar.value = 0;
        moneyCount = 0;
        _velocityUpNav = 1;
    }
}
