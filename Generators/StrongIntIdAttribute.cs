using System;

namespace Diabase.StrongTypes
{
    [AttributeUsage(AttributeTargets.Struct, Inherited = false)]
    public class StrongIntIdAttribute : StrongValueIdAttribute
    {
        public bool IncludePublicIdSupport { get; set; }
        public string PublicIdAesKey { get; set; }
        public string PublicIdAesIv { get; set; }
    }
}
