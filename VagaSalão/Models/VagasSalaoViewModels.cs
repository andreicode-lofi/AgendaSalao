using Microsoft.AspNetCore.Mvc.Rendering;

namespace VagaSalão.Models
{
    public class VagasSalaoViewModels
    {
        public List<Cadastro> Cadastro { get; set; }
        public SelectList? Nome { get; set; }
        public string SobreNome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public DateTime? DataAtendimento { get; set; }
        public string Procedimento{ get; set; }
        public string NomeProfissional { get; set; }

    }
}
