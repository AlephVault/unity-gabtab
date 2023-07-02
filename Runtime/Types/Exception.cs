﻿namespace AlephVault.Unity.GabTab
{
    namespace Types
    {
        /// <summary>
        ///   Base exception for the GabTab features.
        /// </summary>
        public class Exception : AlephVault.Unity.Support.Types.Exception
        {
            public Exception() { }
            public Exception(string message) : base(message) { }
            public Exception(string message, System.Exception inner) : base(message, inner) { }
        }
    }
}
