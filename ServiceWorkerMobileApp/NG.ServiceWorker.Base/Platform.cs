using System;
using System.Collections.Generic;

namespace NG.ServiceWorker
{
    public class Platform
    {
        #region Singleton

        private static Platform s_singleton = null;
        public static Platform GetPlatform() { return s_singleton; }

        #endregion

        #region Flags

        public const string PLATFORM_IOS = "iOS";
        public const string PLATFORM_ANDROID = "Android";
        public const string PLATFORM_WINDOWS = "Windows";
        public const string PLATFORM_UWP = "UWP";

        protected HashSet<string> Flags { get; private set; } = new HashSet<string>();

        #endregion

        #region OS object names

        public const string OS_ANDROID_CONTEXT = "DroidContext";

        #endregion

        #region Base

        public Platform()
        {
            s_singleton = this;
        }

        public bool HasFlag(string flag)
        {
            return this.Flags.Contains(flag);
        }

        #endregion

        #region Virtual functionality

        public virtual double RetinaScale { get { return 1.0; } }

        public virtual object GetOsObject(string objectName) { return null; }

        #endregion
    }
}
