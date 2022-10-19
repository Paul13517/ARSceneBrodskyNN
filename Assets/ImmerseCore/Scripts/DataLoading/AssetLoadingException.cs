using System;

namespace Immerse.Core.Data
{
    [Serializable]
    public class AssetLoadingException : Exception
    {
        public string Path { get; private set; }
        
        public AssetLoadingException(string path)
            : this(path, null, null)
        {
        }

        public AssetLoadingException(string path, string message)
            : this(path, message, null)
        {
        }

        public AssetLoadingException(string path, string message, Exception inner)
            : base(message, inner)
        {
            Path = path;
            if (inner == null)
            {
                return;
            }

            HResult = inner.HResult;
        }
    }
}