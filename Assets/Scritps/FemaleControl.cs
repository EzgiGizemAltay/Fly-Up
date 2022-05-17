using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SirketAdi.ProjeAdi.Core;

public class FemaleControl : MonoBehaviour
{
    [SerializeField] private GameplayController GamePlayController;
    [SerializeField] private FinishController FinishStateController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            GamePlayController.femaleFinish = true;
        }
        
        if (other.gameObject.CompareTag("Ground") && GamePlayController._isTapped)
        {
            FinishStateController.OnGround = true;
        }
    }
}
