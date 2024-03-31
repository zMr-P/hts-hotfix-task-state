using System.ComponentModel.DataAnnotations;

namespace AgendamentoDeTarefas.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="O campo Usuário é obrigatório")]
        public string UserName {  get; set; }

        [Required(ErrorMessage ="O campo senha é obrigatório")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
