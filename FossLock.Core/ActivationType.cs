namespace FossLock.Core
{
    public enum ActivationType
    {
        /// <summary>
        /// 
        /// </summary>
        Manual  = 0x0,

        /// <summary>
        /// 
        /// </summary>
        Email   = 1 << 0,

        /// <summary>
        /// Activation performed via the REST API in FossLock.Web
        /// </summary>
        OnlineAPI  = 1 << 1,

        /// <summary>
        /// Activation is performed via Twilio SMS.
        /// </summary>
        SMS     = 1 << 2,

        /// <summary>
        /// DO NOT USE.  Currently unsupported.  This will for automated voice based activation via Twilio.  
        /// </summary>
        Phone = 1 << 3,


    }
}
