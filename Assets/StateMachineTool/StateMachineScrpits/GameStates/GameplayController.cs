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
    public class GameplayController : GameState
    {
        [SerializeField, TabGroup("GameObjects")]
        private GameObject player, balloonFemale, maleStickman, balloonMale;
        
        private float _velocityUp = 1f, velocityUpMale = 1f;
        
        [NonSerialized] public bool _isTapped= false;
        
        private Vector3 _balloonScaleFemale, _balloonScaleMale;
        
        private bool _canBeDestructible, _balloonDne = false;
        
        [SerializeField, TabGroup("Arrays")] private Texture[] femaleTextures, maleTextures;

        [SerializeField, TabGroup("GameObjects")]
        private GameObject femaleSkin, maleSkin;
        
        [SerializeField, TabGroup("Arrays")] private Material[] balloonMaterial;
        private Material _currentMaterialFemale;

        [SerializeField, TabGroup("GameObjects")]
        private GameObject balloonFemaleForMaterial;

        private int _maleSpeedChange;
        
        [NonSerialized] public bool maleFinish = false, femaleFinish = false;

        [SerializeField, TabGroup("Animators")] private Animator femaleAnimator, maleAnimator;

        [SerializeField, TabGroup("GameObjects")] private GameObject money, moneyNew, moneyTextGameobj;

        [NonSerialized] public bool MoneyCanMove = false;
        
        [SerializeField, TabGroup("Floats")] private float moneyFinalPosY;
        
        private TextMeshPro moneyText;

        private GameObject newMoney;

        [SerializeField] private GameObject Cam;
        private bool camActive = false, camCanMove = true;

        [SerializeField] private GameObject CubeFirst, CubeSec;

        [NonSerialized] public bool fail = false, win = false, onGround = false;

        [SerializeField] private ParticleSystem BalloonPop;

        private void OnEnable()
        {
            CamMovement();
            Debug.Log("Gameplay State Basladi");
            _currentMaterialFemale = balloonFemaleForMaterial.GetComponent<Renderer>().material;
            balloonFemale.SetActive(false);
            balloonMale.SetActive(false);
        }

        private void OnDisable()
        {
            Debug.Log("Gameplay State Bitti");
        }

        public void StateGec()
        {
            _controller.CurrentStateFinished();
        }

        public void MoneyInstantiate()
        {
            newMoney = Instantiate(moneyTextGameobj, new Vector3(money.transform.position.x, money.transform.position.y-moneyFinalPosY, money.transform.position.z), quaternion.identity);
            moneyText = newMoney.GetComponent<TextMeshPro>();
            newMoney.transform.parent = moneyNew.transform;
            newMoney.transform.DOLocalMoveY(moneyFinalPosY, 1f);
            moneyText.DOColor(new Color32(20, 255, 23, 0), 1f);
        }
        
        private void Start()
        {
            BalloonPop.Stop();
        }

        private void Update()
        {
            Debug.Log("VelocityUp: " +_velocityUp);
            Debug.Log("scalex: "+ _balloonScaleFemale.x);
            if (Input.GetMouseButtonUp(0))
            {
                _isTapped = true;
                femaleAnimator.SetTrigger("IsReady");
                maleAnimator.SetTrigger("IsReady");
                balloonFemale.SetActive(true);
                balloonMale.SetActive(true);
                MoneyCanMove = true;
                camActive = true;
            }
            if (_isTapped)
            {
                EventBus<GameCanvasOpenEvent>.Emit(new GameCanvasOpenEvent());
            }
            if (_isTapped)
            {
                MoveUp();
                ChangeVelocityUpandScaleOfBalloonWhileTap();
                MoneyMove();
            }
            //TODO
            if (maleFinish)
            {
                fail = true;
                BalloonPop.Play();
                StateGec();
            }
            //TODO
            if (femaleFinish)
            {
                win = true;
                StateGec();
                femaleFinish = false;
            }
            
            //Balloon becomes destructible
            if (_balloonScaleFemale.x >= 0.5f)
            {
                _canBeDestructible = true;
            }
        }

        public void MoveUp()
        {
            player.transform.Translate(0, _velocityUp * Time.deltaTime, 0);
            maleStickman.transform.Translate(0, velocityUpMale*Time.deltaTime, 0);
        }

        public void ChangeVelocityUpandScaleOfBalloonWhileTap()
        {
            if (_balloonScaleFemale.x <= 2.5f)
            {
                _currentMaterialFemale.DOColor(balloonMaterial[0].color, 0.2f);
            }
            //Balloon size and velocityUp increases while tapping
            if (Input.GetMouseButtonDown(0) && !_balloonDne)
            {
                _velocityUp += 0.2f;
                femaleSkin.GetComponent<SkinnedMeshRenderer>().material.mainTexture = femaleTextures[1];
                maleSkin.GetComponent<SkinnedMeshRenderer>().material.mainTexture = maleTextures[1];
                balloonFemale.transform.DOScale(new Vector3(balloonFemale.transform.localScale.z + 0.2f,
                    balloonFemale.transform.localScale.y + 0.2f, balloonFemale.transform.localScale.z + 0.2f), 0.2f);
                AIControl();
                MoneyInstantiate();
                
                //Balloon can be exploded soon (Warning)
                if (_balloonScaleFemale.x >= 2.5f && _balloonScaleFemale.x<= 3)
                {
                    // balloon color will be orange 
                    _currentMaterialFemale.DOColor(balloonMaterial[1].color, 0.2f);
                }
                //Balloon has been exploded
                else if (_balloonScaleFemale.x >= 3)
                {
                    // balloon color will be red
                    balloonFemaleForMaterial.GetComponent<SkinnedMeshRenderer>().material = balloonMaterial[2];
                    BalloonPop.Play();
                    fail = true;
                    StateGec();
                }
            }
            //Balloon size and velocityUp decreases (no tapping)
            else if (!_balloonDne)
            {
                _velocityUp -= 0.02f;
                femaleSkin.GetComponent<SkinnedMeshRenderer>().material.mainTexture = femaleTextures[0];
                maleSkin.GetComponent<SkinnedMeshRenderer>().material.mainTexture = maleTextures[0];
                //balloonFemale.transform.localScale = new Vector3(balloonFemale.transform.localScale.z - 0.01f,
                    //balloonFemale.transform.localScale.y - 0.01f, balloonFemale.transform.localScale.z - 0.01f);
                balloonFemale.transform.DOScale(new Vector3(balloonFemale.transform.localScale.z - 0.02f,
                    balloonFemale.transform.localScale.y - 0.02f, balloonFemale.transform.localScale.z - 0.02f), 0.2f);
                //Balloon size is very low, destroy the balloon and loose
                if (_balloonScaleFemale.x <= 0.5f && _canBeDestructible)
                {
                    _balloonDne = true;
                    fail = true;
                    BalloonPop.Play();
                    StateGec();
                }
            }
            _balloonScaleFemale = balloonFemale.transform.localScale;
        }

        public void LooseFunc()
        {
            _velocityUp = -10;
            balloonFemale.SetActive(false);
            DieScreen.Open();
        }

        //Speed of MaleStickman will be change randomly
        public void AIControl()
        {
            _maleSpeedChange = Random.Range(0, 5);
            switch (_maleSpeedChange)
            {
                case 1 :
                    velocityUpMale = 1.5f;
                    balloonMale.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 2f);
                    break;
                case 2 :
                    velocityUpMale = 2;
                    balloonMale.transform.DOScale(new Vector3(2, 2, 2), 2f);
                    break;
                case 3 :
                    velocityUpMale = 3;
                    balloonMale.transform.DOScale(new Vector3(2.5f,2.5f, 2.5f), 2f);
                    break;
                case 4 :
                    velocityUpMale = 4;
                    balloonMale.transform.DOScale(new Vector3(3,3, 3), 2f);
                    break;
            }
        }

        public void MoneyMove()
        {
            if (MoneyCanMove)
            {
                money.transform.DOLocalMoveY( moneyFinalPosY, 2f);
            }
        }
        public void CamMovement()
        {
            Cam.transform.DOLocalMove(new Vector3(-4.94f, 5.58f, -13.09f), 0.5f);
        }

       /* public void MoveNavBar()
        {
             var distance = (CubeSec.transform.position - CubeFirst.transform.position).magnitude;
            PercentOfBarLoad = (_velocityUp / distance);
            NavigationBar.value += PercentOfBarLoad * Time.deltaTime;
        }*/
    }
}