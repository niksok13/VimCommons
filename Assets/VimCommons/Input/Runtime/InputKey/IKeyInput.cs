using System;
using UnityEngine;

namespace Commons.Input.Runtime.InputKey
{
    public interface IKeyInput
    {
        void Register(KeyCode key, Action listener);
        void Release(KeyCode key, Action listener);
    }
}