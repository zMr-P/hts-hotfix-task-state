using System.ComponentModel.DataAnnotations.Schema;

namespace AgendamentoDeTarefas.Entites
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public StatusTarefa Status { get; set; }

        [ForeignKey("Usuario")]
        public string IdUsuario { get; set; }
        public MeuUsuario Usuario { get; set; }
    }
    public enum StatusTarefa : int
    {
        Pendente = 0,
        Finalizado = 1
    }
}
