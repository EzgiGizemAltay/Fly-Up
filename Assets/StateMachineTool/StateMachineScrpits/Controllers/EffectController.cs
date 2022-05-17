using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using System.Reflection;
using UnityEngine.UI;

namespace SirketAdi.ProjeAdi.Utils
{
    public class EffectController : LevelSingleton<EffectController>
    {

        //        [SerializeField, TabGroup("Effects References"), Required] private ParticleSystem[] _badEmojis;
        //        [SerializeField, TabGroup("Effects References"), Required] private ParticleSystem[] _goodEmojis;

        //        [SerializeField, TabGroup("Restert Panel Objects"), Required] private GameObject _RestartPanel;
        //        [SerializeField, TabGroup("Restert Panel Objects"), Required] private GameObject _RestartAnim;

        //        [SerializeField, TabGroup("Text Effect Objects"), Required] private GameObject _textEffect;
        //        [SerializeField, TabGroup("Text Effect Objects"), Required] private GameObject _taptap;
        //        [SerializeField, TabGroup("Text Effect Objects"), Required] private GameObject _swipe;

        //        private List<ParticleSystem> _emojis;

        protected override void Awake()
        {
            //base.Awake();
            //InitializeEffects();
        }

        private void InitializeEffects()
        {
            //_emojis = new List<ParticleSystem>();
            //System.Type myType = this.GetType();
            //BindingFlags myFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;
            //FieldInfo[] fields = myType.GetFields(myFlags);

            //foreach (FieldInfo item in fields)
            //{
            //    ParticleSystem single = item.GetValue(this) as ParticleSystem;
            //    if (single)
            //    {
            //        _emojis.Add(single);
            //    }
            //    else
            //    {
            //        ParticleSystem[] array = item.GetValue(this) as ParticleSystem[];
            //        if (array != null)
            //        {
            //            foreach (ParticleSystem particle in array)
            //            {
            //                _emojis.Add(particle);
            //            }
            //        }
            //    }
            //}

            //StopAllActiveEffects();
        }

        public void StopAllActiveEffects()
        {
            //foreach (ParticleSystem effect in _emojis)
            //{
            //    if (effect)
            //        effect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            //}
        }

        private int SelectRandomEffect(ParticleSystem[] effects)
        {
            int selectedRandomIndex = UnityEngine.Random.Range(0, effects.Length);
            return selectedRandomIndex;
        }

        private void PlayEffect(ParticleSystem effect)
        {
            //effect.Play();
        }

        public void PlayBadEffect()
        {
            //PlayEffect(_dislikeEmoji);
        }

        public void PlayRandomGoodEffect()
        {
            //_goodBackgroundEmojis.Play();
            //PlayEffect(_goodEmojis[SelectRandomEffect(_goodEmojis)]);
        }

        public void PlayRandomBadEffect()
        {
            //PlayEffect(_badEmojis[SelectRandomEffect(_badEmojis)]);
        }

        public void PlayGoodEffect()
        {
            //_heartEmoji.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        public void KillGoodEffect()
        {
            //_heartEmoji.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        public void PlayFailedEffect()
        {
            //_RestartPanel.SetActive(true);
            //_RestartAnim.transform.DOLocalMove(new Vector3(0, 770f, 0), 2).SetEase(Ease.OutBounce);
        }

        public void PlayTextEffect()
        {
            //_textEffect.SetActive(true);
        }
        public void StopTextEffect()
        {
           // _textEffect.SetActive(false);
        }

        public void PlayTaptapEffect()
        {
            //_taptap.SetActive(true);
        }
        public void StopTaptapEffect()
        {
            //_taptap.SetActive(false);
        }

        public void PlaySwipeEffect()
        {
            //_swipe.SetActive(true);
        }
        public void StopSwipeEffect()
        {
            //_swipe.SetActive(false);
        }

        public void PlayCameraShakeEffect()
        {
            //_camera.transform.DOMove(_camera.transform.localPosition + _shakePower, _durationShake).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
            //_camera.transform.DOPunchRotation(_shakePower, _durationShake, 2).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }

    }

}