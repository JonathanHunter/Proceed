using System;

namespace Assets.Scripts.ProceduralGen
{
    class UnableToCorrectSequenceException : Exception
    {
        public UnableToCorrectSequenceException()
        {
        }

        public UnableToCorrectSequenceException(string message) : base(message)
        {
        }

        public UnableToCorrectSequenceException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
