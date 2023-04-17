using System.Runtime.Serialization;

namespace UserDataAccess.Repository.Exceptions
{
    [Serializable]
    internal class FailedToFindEntityByIdException : Exception
    {
        public string Value;

        public FailedToFindEntityByIdException()
        {
        }

        public FailedToFindEntityByIdException(string? message) : base(message)
        {
        }

        public FailedToFindEntityByIdException(string? message, string value) : this(message)
        {
            Value = value;
        }

        public FailedToFindEntityByIdException(string? message, Exception? innerException, string value) : base(message, innerException)
        {
            Value = value;
        }

        protected FailedToFindEntityByIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}