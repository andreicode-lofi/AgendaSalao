using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using VagaSalão.Contexto;
using VagaSalão.Models;
using System.Linq;


namespace VagaSalão.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly appContexto _context;


        public HomeController(ILogger<HomeController> logger, appContexto context)
        {
            _logger = logger;
            _context = context;

        }



        //-----------------------------------------------------------------------------------Login
        //LOGIN
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        //POST lOGIN
        [HttpPost]
        public async Task<IActionResult> Login(string email, string senha)
        {
            SqlConnection sqlConnection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=VagaSalao;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            await sqlConnection.OpenAsync();

            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = $"SELECT * FROM Login WHERE Email = '{email}' AND Senha = '{senha}'";

            SqlDataReader reader = sqlCommand.ExecuteReader();

            if(await reader.ReadAsync())
            {
                int emailId = reader.GetInt32(0);
                string emailUser = reader.GetString(1);

                List<Claim> direitoAcesso = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, emailUser, emailId.ToString()),
                    new Claim(ClaimTypes.Name, emailUser, emailId.ToString())
                };

                var identity = new ClaimsIdentity(direitoAcesso, "Identity.login");
                var userPrincipal = new ClaimsPrincipal(new[] {identity});

                await HttpContext.SignInAsync(userPrincipal,
                    new AuthenticationProperties
                    {
                        IsPersistent = false,
                        ExpiresUtc = DateTime.Now.AddHours(1),
                    });

                return RedirectToAction("Index");
            }
            return Json(new { Msg = " Usuário não encontrado! " });

        }
        //Action para deslogar
        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync();
            }
            return RedirectToAction("Login", "Home");
        }

        //----------------------------------------------------------------------------Cadastros

     
       
        public async Task<IActionResult> Index(DateTime? dataAtendimento)
        {
            var cadastro = from m in _context.Cadastro
                           select m;

            if (dataAtendimento.HasValue)
            {
                cadastro = cadastro.Where(s => s.DataAtendimento.Value.ToShortDateString() == dataAtendimento.Value.ToShortDateString());
            }

           return View(await cadastro.ToListAsync());
        }




        //Get Create
        public IActionResult Create()
        {
            return View();
        }
        //Post creat
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Create([Bind("Id,Nome,SobreNome,Telefone,Email,DataAtendimento,Procedimento,NomeProfissional")]Cadastro cadastro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cadastro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            return View(cadastro);
        }

        //GET EDIT
        public async Task<IActionResult>Edit(int id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var cadastro = await _context.Cadastro.FindAsync(id);

            if(cadastro == null)
            {
                return NotFound();
            }

            return View(cadastro);
        }

        //POST EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult>Edit(int id, [Bind("Id,Nome,SobreNome,Telefone,Email,DataAtendimento,Procedimento,NomeProfissional")] Cadastro cadastro)
        {
            if(id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cadastro);
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if (!FormularioExists(cadastro.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cadastro);
        }

        public bool FormularioExists(int id)
        {
            return _context.Cadastro.Any(c => c.Id == id);
        }

        //GET DELETAR 
        public async Task<IActionResult> Delete(int id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var cadastro = _context.Cadastro
                .FirstOrDefault(c => c.Id == id);

            if(cadastro == null)
            {
                return NotFound();
            }

            return View(cadastro);
        }

        //POST DELETE   
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cadastro = await _context.Cadastro.FindAsync(id);
            _context.Cadastro.Remove(cadastro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //GET DETALHES
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cadastro = await _context.Cadastro
                .FirstOrDefaultAsync(m => m.Id == id);

            if (cadastro == null)
            {
                return NotFound();
            }
            return View(cadastro);
        }

      





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}