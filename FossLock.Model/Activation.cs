using System;

namespace FossLock.Model
{
	/// <summary>
	/// Description of Activation.
	/// </summary>
	public class Activation
	{
		public Activation(){ }

        public int Id { get; set; }
        public DateTimeOffset ActivationDateTime { get; set; }
        public DateTimeOffset? DeactivationDateTime { get; set; }
        public string EncryptedSerializedHwdFP { get; set; }
        public virtual License License { get; set; }

	}
}
