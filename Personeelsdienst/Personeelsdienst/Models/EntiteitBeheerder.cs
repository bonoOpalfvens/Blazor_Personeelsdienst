using System.ComponentModel.DataAnnotations;

namespace Personeelsdienst.Models
{
    public class EntiteitBeheerder
    {
        [Key]
        public long Id { get; set; }
        public long EntiteitId { get; set; }
        public long BeheerderId { get; set; }
        public Entiteit Entiteit { get; set; }
        public Beheerder Beheerder { get; set; }
    }
}
