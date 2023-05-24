using System;
using UnityEngine;

namespace VimCommons.Input.Runtime.InputTouch
{
    [Serializable]
    public struct JoystickState
    { 
        public Vector2 value;
        public JoystickEvent action;
    }
}