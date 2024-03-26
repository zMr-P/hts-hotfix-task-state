using System.ComponentModel.DataAnnotations;


namespace AgendamentoDeTarefas.Models
{
    public class RegistrarModel
    {
        [Required(ErrorMessage="O campo {0} é obrigatório")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50,ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "O campo Confirmar senha é obrigatório")]
        [Compare(nameof(Password), ErrorMessage = "Senhas Diferentes")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
