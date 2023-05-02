namespace GitHubRepo.Models
{
    public class RepositorioExibicao
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? NomeCompleto { get; set; }
        public string? Visibilidade { get; set; }
        public string? Linguagem { get; set; }
        public string? Url { get; set; }
        public string? Descricao { get; set; }
        public string? UrlClone { get; set; }
        public DateTime? CriadoEm { get; set; }
        public DateTime? AtualizadoEm { get; set; }
        public UsuarioExibicao? Usuario { get; set; }
    }


}
