using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SirketAdi.ProjeAdi.Core;
using DG.Tweening;
using UnityEngine.Pool;

public class Money : MonoBehaviour
{
    private Action<Money> _killAction;

    public void Init(Action<Money> killAction)
    {
        _killAction = killAction;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("MoneyDestroy"))
        {
            if (_killAction != null)
            {
                _killAction(this);    
            }
            
        }
    }
    //[SerializeField] private GameplayController GamePlayController;
    //[SerializeField] private float moneyFinalPosY;


    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MoneyDestroy"))
        {
            GamePlayController.DestroyMoney = true;
            Debug.Log("Destroy------------------------");
        }
    }*/
}
