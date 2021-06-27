using System;
using System.Collections.Generic;

namespace NG.ServiceWorker
{
    public class Platform
    {
        public const string PLATFORM_IOS = "iOS";
        public const string PLATFORM_ANDROID = "Android";

        private static Platform s_singleton = null;
        public static Platform GetPlatform() { return s_singleton; }

        protected HashSet<string> Flags { get; private set; } = new HashSet<string>();

        public Platform()
        {
            s_singleton = this;
        }

        public bool HasFlag(string flag)
        {
            return this.Flags.Contains(flag);
        }

        public virtual double RetinaScale { get { return 1.0; } }
    }
}
