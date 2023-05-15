using System;
using UnityEngine;

namespace VimInput.Runtime.InputTouch
{
    [Serializable]
    public struct JoystickState
    { 
        public Vector2 value;
        public JoystickEvent action;
    }
}