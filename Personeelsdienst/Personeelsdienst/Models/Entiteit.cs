using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Personeelsdienst.Models
{
    public class Entiteit
    {
        #region Fields
        private string _entiteitsnaam, _email;
        #endregion

        #region Properties
        [Key]
        public long Id { get; set; }
        [Required(ErrorMessage = "Entiteitsnaam is verplicht")]
        [MinLength(1, ErrorMessage = "Entiteitsnaam is verplicht")]
        public string Entiteitsnaam { get { return _entiteitsnaam; } set { _entiteitsnaam = value.Trim(); } }
        [Required(ErrorMessage = "Email is verplicht")]
        [EmailAddress(ErrorMessage = "Email is verplicht")]
        public string Email { get { return _email; } set { _email = value.Trim().ToLower(); } }
        public List<Personeelslid> Personeelsleden { get; set; }
        public List<EntiteitBeheerder> Beheerders { get; set; }
        #endregion

        #region Constructors
        public Entiteit(string entiteitsnaam, string email) : this()
        {
            Entiteitsnaam = entiteitsnaam;
            Email = email;
        }
        private Entiteit() 
        {
            Personeelsleden = new List<Personeelslid>();
        }
        #endregion

        #region Methods
        public bool BevatAfwezigheden => Personeelsleden.Any(p => p.IsAfwezig());
        public List<Personeelslid> AfwezigePersoneelsleden() => Personeelsleden.Where(p => p.IsAfwezig()).ToList();
        #endregion
    }
}
