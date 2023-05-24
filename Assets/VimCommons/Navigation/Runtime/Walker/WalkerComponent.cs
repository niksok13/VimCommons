using UnityEngine;
using UnityEngine.AI;
using VimCommons.Input.Runtime.InputTouch;
using VimCore.Runtime.MVVM;
using VimCore.Runtime.Utils;

namespace VimCommons.Navigation.Runtime.Walker
{
    public class WalkerComponent: MonoBehaviour
    {
        public float walkSpeed =  5 ;

        private float Speed => walkSpeed;

        public ObservableData<float> MoveSpeed { get; } = new();

        private Transform _transform;
        public Transform Transform => _transform ??= transform.GetChild(0);

        private NavMeshAgent _agent;
        private NavMeshAgent Agent => _agent ??= GetComponent<NavMeshAgent>();
        
        private Vector3 _moveDirection;

        private void Awake() => LoopUtil.PostLateUpdate += Tick;

        private void OnDestroy() => LoopUtil.PostLateUpdate -= Tick;


        public void Tick()
        {
            if (Time.timeScale < float.Epsilon) return;
            var dir = _moveDirection;
            var speed = Speed * LoopUtil.Delta;
            if(Agent.isOnNavMesh)
                Agent.Move(dir * speed);
        }
        
        public void OnJoystick(JoystickState state)
        {
            switch (state.action)
            {
                case JoystickEvent.Move:
                    MoveSpeed.Value = 1;

                    var dir = new Vector3(state.value.x, 0, state.value.y);
                    dir = transform.rotation * dir;
                    Transform.rotation = Quaternion.LookRotation(dir);
                    _moveDirection = dir.normalized;
                    break;
                
                case JoystickEvent.Release:
                    MoveSpeed.Value = 0;
                    
                    _moveDirection = Vector3.zero;
                    break;
            }
        }
    }
}