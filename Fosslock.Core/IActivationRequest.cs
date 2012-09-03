using System;

namespace FossLock.Serializable
{
	/// <summary>
	/// I activation request.
	/// </summary>
	public interface IActivationRequest
	{

		DateTimeOffset RequestGenerationTime { get; set; }

		// hardware information

		// product information
		int ProductId { get; set; }

		// lock-to information
		LockProperties LockTo { get; set; }

		// customer information
		int CustomerId { get; set; }
	}
}

