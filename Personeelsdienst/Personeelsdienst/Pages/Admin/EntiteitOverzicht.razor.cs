using Microsoft.AspNetCore.Components;
using Personeelsdienst.Models;
using Personeelsdienst.Models.IRepositories;
using System.Collections.Generic;

namespace Personeelsdienst.Pages.Admin
{
    public class EntiteitOverzichtBase : ComponentBase
    {
        [Inject]
        public IEntiteitRepository EntiteitRepository { get; set; }
        public IList<Entiteit> Entiteiten => EntiteitRepository.GetAll();
    }
}
