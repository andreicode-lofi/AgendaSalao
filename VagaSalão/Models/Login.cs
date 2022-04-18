using System.ComponentModel.DataAnnotations;

namespace VagaSalão.Models
{
    public class Login
    {
        public int Id { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Informe o email", AllowEmptyStrings = false)]
        public string Email { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Informe sua senha", AllowEmptyStrings = false)]
        public string Senha { get; set; }
    }
}
