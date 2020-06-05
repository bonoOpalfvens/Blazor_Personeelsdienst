using Personeelsdienst.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Personeelsdienst.Models
{
    public class Afwezigheid
    {
        #region Fields
        private string _vervanger;
        #endregion

        #region Properties
        [Key]
        public long Id { get; set; }
        [Required(ErrorMessage = "Personeelslid is verplicht")]
        public Personeelslid Personeelslid { get; set; }
        [Required(ErrorMessage = "Reden van afwezigheid is verplicht")]
        public RedenAfwezigheid RedenAfwezigheid { get; set; }
        [Required(ErrorMessage = "Begindatum is verplicht")]
        public DateTime BeginDatum { get; set; }
        public DateTime? EindDatum { get; set; }
        public string Vervanger { get { return _vervanger; } set { _vervanger = value.Trim(); } }
        public bool Verwerkt { get; set; } = false;
        public bool BewijsOk { get; set; } = false;
        public byte[] Bijlage { get; set; }
        #endregion

        #region Constructors
        public Afwezigheid(Personeelslid personeelslid, RedenAfwezigheid redenAfwezigheid, DateTime beginDatum) : this()
        {
            Personeelslid = personeelslid;
            RedenAfwezigheid = redenAfwezigheid;
            BeginDatum = beginDatum;
        }
        private Afwezigheid() { }
        #endregion

        #region Methods
        public bool IsAfgelopen() => (EindDatum is null) ? false : EindDatum?.Date.CompareTo(DateTime.Today) < 0;
        public bool IsLopend() => (EindDatum is null) ? BeginDatum.Date.CompareTo(DateTime.Today) <= 0 : EindDatum?.Date.CompareTo(DateTime.Today) >= 0 && BeginDatum.Date.CompareTo(DateTime.Today) <= 0;
        public bool IsBegonnen() => BeginDatum.Date.CompareTo(DateTime.Today) <= 0;
        #endregion
    }
}
