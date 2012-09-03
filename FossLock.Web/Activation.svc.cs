using System;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace FossLock.Web
{
	[ServiceContract]
	public interface IActivation
	{
		[OperationContract]
		[WebGet(UriTemplate = "operation/{name}")]
	    string MyOperation(string name);
	}

	/// <summary>
	/// Description of Activation.
	/// </summary>
	public class Activation : IActivation
	{
		public string MyOperation(string name)
	    {
		  // implement the operation
		  return string.Format("Operation name: {0}", name);
	    }
	}
}
