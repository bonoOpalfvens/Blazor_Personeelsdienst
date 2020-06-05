using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Personeelsdienst.Models
{
    public class Personeelslid
    {
        #region Fields
        private string _naam;
        #endregion

        #region Properties
        [Key]
        public long Id { get; set; }
        [MinLength(1, ErrorMessage = "Naam is verplicht")]
        public string Naam { get { return _naam; } set { _naam = value.Trim(); } }
        [Required(ErrorMessage = "School is verplicht")]
        public Entiteit Entiteit { get; set; }
        public List<Afwezigheid> Afwezigheden { get; set; }
        #endregion

        #region Constructors
        public Personeelslid(string naam, Entiteit school) : this()
        {
            Naam = naam;
            Entiteit = school;
        }
        private Personeelslid()
        {
            Afwezigheden = new List<Afwezigheid>();
        }
        #endregion

        #region Methods
        public bool IsAfwezig() => Afwezigheden.Any(a => a.IsLopend());
        public List<Afwezigheid> HuidigeAfwezigheden() => Afwezigheden.Where(a => a.IsLopend()).ToList();
        #endregion
    }
}
