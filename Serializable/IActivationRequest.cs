using System;

namespace FossLock.Core
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

