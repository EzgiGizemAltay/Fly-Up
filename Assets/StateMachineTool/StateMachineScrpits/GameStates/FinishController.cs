using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using System;
using SirketAdi.ProjeAdi.Utils;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;
using DG.Tweening;
using LevelManagement;
using Unity.Mathematics;
using UnityEngine.Pool;
using TMPro;
using LevelManagement;

namespace SirketAdi.ProjeAdi.Core
{
    public class FinishController : GameState
    {
        [SerializeField] private GameObject player, balloonFemale;
        [NonSerialized] public bool OnGround = false;
        [SerializeField] private GameplayController GamePlayController;
        [SerializeField] private Animator FemaleAnimator;
        private float _velocityUp;
        
        
        
        private void OnEnable()
        {
            Debug.Log("Finish State Basladi");
        }

        private void OnDisable()
        {
            Debug.Log("Finish State Bitti");
        }

        private void Update()
        {
            Debug.Log("FinishVelocity:"+ _velocityUp);
            MoveDown();
            if (GamePlayController.fail)
            {
                LooseFunc();
                GamePlayController.fail = false;
            }

            if (GamePlayController.win)
            {
                WinFunc();
                GamePlayController.win = false;
            }

            if (OnGround)
            {
                OnGroundFunc();
                GamePlayController.onGround = false;
            }
        }

        public void StateGec()
        {
            _controller.CurrentStateFinished();
        }
        
        public void LooseFunc()
        {
            _velocityUp = -10;
            balloonFemale.SetActive(false);
            FemaleAnimator.SetTrigger("IsFall");
            EventBus<GameEndEvent>.Emit(new GameEndEvent());
            StartCoroutine(WaitandLoose(0.2f));
            //ßStartCoroutine(WaitAndStop(1.1f));
        }

        public void OnGroundFunc()
        {
            _velocityUp = 0;
            //balloonFemale.SetActive(false);
            FemaleAnimator.SetTrigger("OnGround");
            //StartCoroutine(WaitandLoose(1));
            
        }

        public void MoveDown()
        {
            player.transform.Translate(0, _velocityUp * Time.deltaTime, 0);
        }

        public void WinFunc()
        {
            _velocityUp = 10;
            EventBus<GameEndEvent>.Emit(new GameEndEvent());
            StartCoroutine(WaitandWin(0.2f));
            
        }

        IEnumerator WaitandLoose(float waitSec)
        {
            yield return new WaitForSeconds(waitSec);
            //TODO
            DieScreen.Open();
        }
        IEnumerator WaitandWin(float waitSec)
        {
            yield return new WaitForSeconds(waitSec);
            //TODO
            WinScreen.Open();
        }
        IEnumerator WaitAndStop(float wait)
        {
            yield return new WaitForSeconds(wait);
            Time.timeScale = 0;
        }

    }

}