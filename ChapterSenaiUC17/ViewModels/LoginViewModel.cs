using System.ComponentModel.DataAnnotations;

namespace ChapterSenaiUC17.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informe o e-mail do usuário")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe o senha do usuário")]
        public string Senha { get; set; }
    }
}
