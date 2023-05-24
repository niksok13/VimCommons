using System;
using System.Collections.Generic;
using UnityEngine;

namespace VimCore.Runtime.MVVM.ViewModels.Animation
{
    public class VMAnimator: MonoBehaviour
    {
        private Animator _anim;
        private ModelBehaviour _model;

        private readonly Dictionary<string, ObservableData<bool>> _observableBools = new();
        private readonly Dictionary<string, ObservableData<float>> _observableFloats = new();
        private readonly Dictionary<string, ObservableData<int>> _observableInts = new();
        private readonly Dictionary<string, ObservableData<bool>> _observableTriggers = new();
        
        private readonly Dictionary<string, Action<bool>> _actionBools = new();
        private readonly Dictionary<string, Action<float>> _actionFloats = new();
        private readonly Dictionary<string, Action<int>> _actionInts = new();
        private readonly Dictionary<string, Action<bool>> _actionTriggers = new();

        private void Start()
        {
            _anim = GetComponent<Animator>();
            _anim.cullingMode = AnimatorCullingMode.CullCompletely;
            _model = GetComponentInParent<ModelBehaviour>();
            foreach (var parameter in _anim.parameters)
            {
                var key = parameter.name;
                switch (parameter.type)
                {
                    case AnimatorControllerParameterType.Bool:
                        var b = _model.GetObservableData<bool>(key);
                        if (b == null) break;
                        Action<bool> ab = val => _anim.SetBool(key, val);
                        _observableBools[key] = b;
                        _actionBools[key] = ab;
                        b.OnValue += ab;
                        break;
                    
                    case AnimatorControllerParameterType.Float:
                        var f = _model.GetObservableData<float>(key);
                        if (f == null) break;
                        Action<float> af = val => _anim.SetFloat(key, val);
                        _observableFloats[key] = f;
                        _actionFloats[key] = af;
                        f.OnValue += af;
                        break;

                    case AnimatorControllerParameterType.Int:
                        var i = _model.GetObservableData<int>(key);
                        if (i == null) break;
                        Action<int> ai = val => _anim.SetInteger(key, val);
                        _observableInts[key] = i;
                        _actionInts[key] = ai;
                        i.OnValue += ai;
                        break;
                    
                    case  AnimatorControllerParameterType.Trigger:
                        var t = _model.GetObservableData<bool>(key);
                        if (t == null) break;
                        Action<bool> at = val =>
                        {
                            if (val)
                                _anim.SetTrigger(key);
                            else
                                _anim.ResetTrigger(key);
                        };
                        _observableTriggers[key] = t;
                        _actionTriggers[key] = at;
                        t.OnValue += at;
                        break;
                    
                }
            }
        }


        private void OnDestroy()
        {
            foreach (var (key,observable) in _observableBools)
                observable.OnValue-=_actionBools[key];
            foreach (var (key,observable) in _observableFloats)
                observable.OnValue-=_actionFloats[key];
            foreach (var (key,observable) in _observableInts)
                observable.OnValue-=_actionInts[key];
            foreach (var (key,observable) in _observableTriggers)
                observable.OnValue-=_actionTriggers[key];
        }
    }
}