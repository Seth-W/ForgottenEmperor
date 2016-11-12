using System;

namespace CFE
{
    class DebugMessageAction : IAction
    {
        string message;
        bool firstExecute;

        public DebugMessageAction(string msg)
        {
            message = msg;
            firstExecute = true;
        }

        public bool execute()
        {
            if(firstExecute)
            {
                firstExecute = false;
                return false;
            }
            UnityEngine.Debug.Log(message);
            return true;
        }

        public bool revert()
        {
            throw new NotImplementedException();
        }

        public bool cancel()
        {
            throw new NotImplementedException();
        }
    }
}
