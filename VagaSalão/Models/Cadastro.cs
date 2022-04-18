using System.ComponentModel.DataAnnotations;

namespace VagaSalão.Models
{
    public class Cadastro
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Informe o seu nome", AllowEmptyStrings = false)]
        public string Nome { get; set; }

        [Display(Name = "SobreNome")]
        [Required(ErrorMessage ="Informe o seu sobre nome", AllowEmptyStrings = false)]
        public string SobreNome { get; set; }

        [Display (Name ="Telefone")]
        [Required(ErrorMessage = "Informe o seu telefone", AllowEmptyStrings =false)]
        public string Telefone { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name ="Data de atendimento")]
        [Required(ErrorMessage ="Informe a data de nascimento", AllowEmptyStrings = false)]
        public DateTime? DataAtendimento { get; set; }


        [Display(Name = "Procesimento")]
        [Required(ErrorMessage = "Informe o procedimento", AllowEmptyStrings = false)]
        public string Procedimento { get; set; }

        [Display(Name = "Nome do profissional")]
        [Required(ErrorMessage = "Informe o nome do profissional", AllowEmptyStrings = false)]
        public string NomeProfissional { get; set; }

    }
}
