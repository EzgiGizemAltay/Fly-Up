using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer.FSM;

namespace SirketAdi.ProjeAdi.Core {

    public abstract class GameState : StateBehaviour {

        [SerializeField] protected GameController _controller;
    }

}
