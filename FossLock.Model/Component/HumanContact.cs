using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FossLock.Model.Component
{
    /// <summary> A human being that works at a customer's office.
    /// This includes their contact information.
    /// </summary>
    [ComplexType]
    public class HumanContact
    {
        /// <summary> The first name of this person.
        /// </summary>
        [StringLength(255)]
        [Required]
        public string FirstName { get; set; }

        /// <summary> The last name of this person.
        /// </summary>
        [StringLength(255)]
        [Required]
        public string LastName { get; set; }

        /// <summary> The main contact phone number.
        /// </summary>
        [MaxLength(40)]
        [Phone]
        public string Phone1 { get; set; }

        /// <summary> A secondary phone number.
        /// </summary>
        [MaxLength(40)]
        [Phone]
        public string Phone2 { get; set; }

        /// <summary> A fax number.. because some people like to pretend it's 1980 still.
        /// </summary>
        [MaxLength(40)]
        [Phone]
        public string Fax { get; set; }

        /// <summary> This person's contact e-mail address.
        /// </summary>
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary> Any extra user-provided data for this entity.
        /// </summary>
        public string Notes { get; set; }
    }
}