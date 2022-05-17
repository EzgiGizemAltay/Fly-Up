using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SirketAdi.ProjeAdi.Utils {
    public abstract class LevelSingleton<T> : MonoBehaviour where T : MonoBehaviour {

        private static T _instance = null;

        public static bool IsAwake { get { return (_instance != null); } }
        public static T Instance { get => _instance; }

        protected virtual void Awake() {
            if (_instance)
                Destroy(_instance);
            _instance = this as T;
        }

        protected virtual void OnApplicationQuit() {
            _instance = null;
        }

    }
}
