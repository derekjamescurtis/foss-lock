/*
 * Created by SharpDevelop.
 * User: Derek
 * Date: 9/5/2012
 * Time: 11:09 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace FossLock.Web.Model
{
	/// <summary>
	/// Description of Product.
	/// </summary>
	public class Product
	{
		public Product()
		{
		}

        public int Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public DateTime ReleaseDate { get; set; }
        public LockProperties DefaultLockProperties { get; set; }
        public string Notes { get; set; }
        public int TrialDays { get; set; }
        public virtual ICollection<ProductFeature> AvailableFeatures { get; set; }
        public virtual ICollection<ProductVersion> Versions { get; set; }
		
	}
}
