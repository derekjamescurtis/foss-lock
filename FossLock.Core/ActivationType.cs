using System;

namespace FossLock.Core
{
    /// <summary>
    /// Indicates a type of activation method.
    /// This is a flag-type enumeration.
    /// </summary>
    [Flags]
    public enum ActivationType
    {
        //TODO: Some of these 'see crefs' need updated after the Web API is completed.

        /// <summary>
        /// The <see cref="FossLock.Model.License"/> is activated by manually presenting the licensing server with the activation data (via the web site in <see cref="FossLock.Web"/>), then presenting the 
        /// the application with the resulting data from the activation response.
        /// </summary>
        Manual  = 0x0,

        /// <summary>
        /// Activation request must be sent by the <see cref="FossLock.Model.Customer"/>'s registered e-mail address, to an e-mail server that the activation server is capiable of reading/sending from.
        /// The <see cref="FossLock.Model.Activation"/> information will be returned to that <see cref="FossLock.Model.Customer"/>'s e-mail address, which they must manually register in their software.
        /// </summary>
        Email   = 1 << 0,

        /// <summary>
        /// Activation performed via the REST API in <see cref="FossLock.Web"/>.
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
