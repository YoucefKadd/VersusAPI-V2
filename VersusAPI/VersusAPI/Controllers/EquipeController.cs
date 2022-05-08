using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VersusAPI.Models;

namespace VersusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipeController : ControllerBase
    {
        private static List<Equipe> equipes = new List<Equipe>
        {
            new Equipe { Id = 1, Name = "FC Barcelone", Abreviation =   "BAR", Color = "Bleu", Pays = "Espagne"},
            new Equipe { Id = 2, Name = "Real Madrid", Abreviation = "MAD", Color = "Balnc", Pays = "Espagne" },
            new Equipe { Id = 3, Name = "Valence FC", Abreviation =   "VAL", Color = "Orange", Pays = "Espagne" }
        };

        [HttpGet]
        public async Task<ActionResult<List<Equipe>>> Get()
        {
            return Ok(equipes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Equipe>> Get(int id)
        {
            var equipe = equipes.Find(e => e.Id == id);
            if(equipe == null) 
                BadRequest("Pas d'équipe trouvé");
            return Ok(equipe);
        }

        [HttpPost]
        public async Task<ActionResult<List<Equipe>>> AddEquipe(Equipe equipe)
        {
            equipes.Add(equipe);
            return Ok(equipes);

        }

        [HttpPut]
        public async Task<ActionResult<Equipe>> Put(Equipe equipeReq)
        {
            var equipe = equipes.Find(e => e.Id == equipeReq.Id);
            if (equipe == null)
                BadRequest("Pas d'équipe trouvé");

            equipe.Name = equipeReq.Name;
            equipe.Abreviation = equipeReq.Abreviation;
            equipe.Pays = equipeReq.Pays;  
            equipe.Color = equipeReq.Color; 

            return Ok(equipe);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Equipe>>> Delete(int id)
        {
            var equipe = equipes.Find(e => e.Id == id);
            if (equipe == null)
                BadRequest("Pas d'équipe trouvé");
            equipes.Remove(equipe);
            return Ok(equipe);
        }
    }
}
