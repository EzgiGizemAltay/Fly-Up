using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SirketAdi.ProjeAdi.Core;
public class MaleControl : MonoBehaviour
{
    [SerializeField] private GameplayController GamePlayController;
    [SerializeField] private FinishController FinishStateController;
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Finish"))
        {
            GamePlayController.maleFinish = true;
            Debug.Log("MaleFinish------------");
        }
        if (col.gameObject.CompareTag("Ground") && GamePlayController._isTapped)
        {
            FinishStateController.OnGround = true;
        }
    }
}
