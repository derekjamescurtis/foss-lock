using System;
using FossLock.Model.Base;

namespace FossLock.Model
{
    /// <summary>
    ///     A single time that a license was successfully activated by the server.
    /// </summary>
    public class Activation : EntityBase
    {
        /// <summary>
        /// Indicates the date and time this activation was created.
        /// </summary>
        public DateTimeOffset ActivationDateTime { get; set; }

        /// <summary>
        ///     Indicates the date and time that this acviation was made invalid.
        ///     This is only set when the activation is destoryed, so it can be recreated.
        /// </summary>
        public DateTimeOffset? DeactivationDateTime { get; set; }

        /// <summary>
        ///     A SHA-256 hash of the hardware identifiers
        /// </summary>
        public string HardwareFingerprint { get; set; }

        /// <summary>
        ///     A reference to the license this activation was generated for.
        /// </summary>
        public virtual License License { get; set; }
    }
}