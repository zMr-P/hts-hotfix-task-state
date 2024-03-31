using System.ComponentModel.DataAnnotations;


namespace AgendamentoDeTarefas.Models
{
    public class RegistrarModel
    {
        [Required(ErrorMessage="O campo Usuário é obrigatório")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O campo senha é obrigatório")]
        [StringLength(50,ErrorMessage = "O campo senha deve ter {2} caracteres e caracter especial.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "O campo Confirmar senha é obrigatório")]
        [Compare(nameof(Password), ErrorMessage = "Senhas Diferentes")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
