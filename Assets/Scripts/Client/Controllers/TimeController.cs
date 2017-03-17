using Boo.Lang;
using System;
using UnityEngine;

namespace Scripts.Client.Controllers
{
    public class TimeController : BaseController
    {
        public class ActionHolder
        {
            public Action Action
            {
                get;
                set;
            }

            public float TimeToInvoke
            {
                get;
                set;
            }
        }

        protected List<ActionHolder> Actions;

        public TimeController()
        {
            Actions = new List<ActionHolder>();
        }

        public void FixedUpdate()
        {
            Actions.RemoveAll(ShouldBeInvoked);
        }

        public void AddAction(Action action, float time)
        {
            Actions.Add(new ActionHolder { Action = action, TimeToInvoke = Time.time + time });
        }

        private bool ShouldBeInvoked(ActionHolder actionHolder)
        {
            if (Time.time > actionHolder.TimeToInvoke)
            {
                actionHolder.Action();
                return true;
            }
            return false;
        }
    }
}
