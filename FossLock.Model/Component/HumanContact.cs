using FossLock.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FossLock.Model.Component
{
    /// <summary> A human being that works at a customer's office.
    /// This includes their contact information.
    /// </summary>
    public class HumanContact : EntityBase
    {
        /// <summary> The first name of this person.
        /// </summary>
        [Required, StringLength(255)]
        public string FirstName { get; set; }

        /// <summary> The last name of this person.
        /// </summary>
        [Required, StringLength(255)]
        public string LastName { get; set; }

        /// <summary> The main contact phone number.
        /// </summary>
        [Phone]
        public string Phone1 { get; set; }

        /// <summary> A secondary phone number.
        /// </summary>
        [Phone]
        public string Phone2 { get; set; }

        /// <summary> A fax number.. because some people like to pretend it's 1980 still.
        /// </summary>
        [Phone]
        public string Fax { get; set; }

        /// <summary> This person's contact e-mail address.
        /// </summary>
        [EmailAddress]
        public string Email { get; set; }

        /// <summary> Any extra user-provided data for this entity.
        /// </summary>
        public string Notes { get; set; }

    }
}
