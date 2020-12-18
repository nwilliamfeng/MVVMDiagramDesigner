using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DiagramDesigner.Helpers
{
    //[DebuggerNonUserCode]
     sealed class WeakPropertyEventHandler 
    {
        private readonly WeakReference _targetReference;
        private readonly MethodInfo _method;

        public WeakPropertyEventHandler(PropertyChangedEventHandler callback)
        {
            _method = callback.Method;
            _targetReference = new WeakReference(callback.Target, true);
        }

        //[DebuggerNonUserCode]
        public void Handler(object sender, PropertyChangedEventArgs e)
        {
            var target = _targetReference.Target;
            if (target != null)
            {
                var callback = (Action<object, PropertyChangedEventArgs>)Delegate.CreateDelegate(typeof(Action<object, PropertyChangedEventArgs>), target, _method, true);
                if (callback != null)
                {
                    callback(sender, e);
                }
            }
        }
    }
}
