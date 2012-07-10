using System;

namespace FossLock.Core
{
	public interface ICustomer
	{
		int Id { get; set; }

		string Name{get;set;}

		string Address1 { get; set; }
		string Address2 { get; set; }
		string City { get; set; }
		string State { get; set; }
		string Zip { get; set; }

		string Phone1 { get; set; }
		string Phone2 { get; set; }
		string Fax { get; set; }
		string Email { get; set; }

		string ContactFirstName{ get; set; }
		string ContactLastName { get; set; }


	}
}

