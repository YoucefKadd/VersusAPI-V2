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
        private readonly DataContext _context;
        // initialisation du contexte de base de données
        public EquipeController(DataContext context)
        {
            this._context = context;
        }

        /*[HttpGet]
        public async Task<ActionResult<List<Equipe>>> Get()
        {
            return Ok(equipes);
        }*/

        // Méthode GET permettant de récupérer les équipes dans la base de donnée
        [HttpGet]
        public async Task<ActionResult<List<Equipe>>> Get()
        {
            return Ok(await _context.Equipes.ToListAsync());
        }



        /*[HttpGet("{id}")]
        public async Task<ActionResult<Equipe>> Get(int id)
        {
            var equipe = equipes.Find(e => e.Id == id);
            if(equipe == null) 
                BadRequest("Pas d'équipe trouvé");
            return Ok(equipe);
        }*/

        // Méthode GET permettant de récupérer une équipe spécifique avec un "ID"
        [HttpGet("{id}")]
        public async Task<ActionResult<Equipe>> Get(int id)
        {
            var equipe = await _context.Equipes.FindAsync(id);
            if (equipe == null)
                BadRequest("Pas d'équipe trouvé");
            return Ok(equipe);
        }

        /*[HttpPost]
        public async Task<ActionResult<List<Equipe>>> AddEquipe(Equipe equipe)
        {
            equipes.Add(equipe);
            return Ok(equipes);

        }*/

        // Méhtode POST permettant d'injecter un élément à la base donnée
        [HttpPost]
        public async Task<ActionResult<List<Equipe>>> AddEquipe(Equipe equipe)
        {
            _context.Equipes.Add(equipe);
            await _context.SaveChangesAsync();

            return Ok(await _context.Equipes.ToListAsync());

        }

        /*[HttpPut]
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
        }*/

        // Méthode PUT permettant de modifier/mettre à jour un élément dans la base de données
        [HttpPut]
        public async Task<ActionResult<Equipe>> Put(Equipe equipeReq)
        {
            var dbEquipe = await _context.Equipes.FindAsync(equipeReq.Id);
            if (dbEquipe == null)
                BadRequest("Pas d'équipe trouvé");

            dbEquipe.Name = equipeReq.Name;
            dbEquipe.Abreviation = equipeReq.Abreviation;
            dbEquipe.Pays = equipeReq.Pays;
            dbEquipe.Color = equipeReq.Color;

            await _context.SaveChangesAsync();

            return Ok(await _context.Equipes.ToListAsync());
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
