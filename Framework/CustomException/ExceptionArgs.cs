using System;

namespace Framework.CustomException
{
    [Serializable]
    public abstract class ExceptionArgs
    {
        public virtual String Message
        {
            get
            {
                return String.Empty;
            }
        }
    }
}
