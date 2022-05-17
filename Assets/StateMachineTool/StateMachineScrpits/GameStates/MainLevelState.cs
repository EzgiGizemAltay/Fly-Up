using Animancer.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace SirketAdi.ProjeAdi.Core
{
    public class MainLevelState : GameState
    {
        //[SerializeField] private GameObject button;
        private void OnEnable()
        {
            Debug.Log("MainLevel State Basladi");
            //button.SetActive(true);
        }
        
        private void OnDisable()
        {
            Debug.Log("MainLevel State Bitti");
            //button.SetActive(false);
        }
        
        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                _controller.CurrentStateFinished();
            }
        }
        
        public void StateGec()
        {
            _controller.CurrentStateFinished();
        }
    }

}

