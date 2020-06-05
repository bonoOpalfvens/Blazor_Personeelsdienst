using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Personeelsdienst.Models
{
    public class Entiteit
    {
        #region Fields
        private string _entiteitsnaam;
        #endregion

        #region Properties
        [Key]
        public long Id { get; set; }
        [MinLength(1, ErrorMessage = "Entiteitsnaam is verplicht")]
        public string Entiteitsnaam { get { return _entiteitsnaam; } set { _entiteitsnaam = value.Trim(); } }
        public List<Personeelslid> Personeelsleden { get; set; }
        #endregion

        #region Constructors
        public Entiteit(string entiteitsnaam) : this()
        {
            Entiteitsnaam = entiteitsnaam;
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
