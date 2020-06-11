using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Personeelsdienst.Models
{
    public class Beheerder
    {
        #region Fields
        private string _email;
        #endregion

        #region Properties
        [Key]
        public long Id { get; set; }
        [Required(ErrorMessage = "Email is verplicht")]
        [EmailAddress(ErrorMessage = "Ongeldige email")]
        public string Email { get { return _email; } set { _email = value.Trim().ToLower(); } }
        public List<EntiteitBeheerder> Entiteiten { get; set; }
        #endregion

        #region Constructors
        public Beheerder(string email) : this()
        {
            Email = email;
        }
        public Beheerder()
        {
            Entiteiten = new List<EntiteitBeheerder>();
        }
        #endregion
    }
}
