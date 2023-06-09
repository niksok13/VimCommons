using System;

namespace VimCommons.Input.Runtime.InputTouch
{
    public interface ITouchInput
    {
        void Register (Action<JoystickState> listener);
        void Release (Action<JoystickState> listener);
        void Release();
    }
}