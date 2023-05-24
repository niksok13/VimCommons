using System;
using UnityEngine;

namespace Commons.Input.Runtime.InputTouch
{
    [Serializable]
    public struct JoystickState
    { 
        public Vector2 value;
        public JoystickEvent action;
    }
}