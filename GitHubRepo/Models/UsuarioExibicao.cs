namespace GitHubRepo.Models
{
    public class UsuarioExibicao
    {
        public int? Id { get; set; }
        public string? Login { get; set; }
        public string? Email { get; set; }
        public string? Nome { get; set; }
        public string? Avatar { get; set; }
        public string? LinkPerfil { get; set; }
        public string? Empresa { get; set; }
        public int? Seguidores { get; set; }
        public int? Seguindo { get; set; }
        public string? Blog { get; set; }
        public string? Twitter{ get; set; }
        public string? Localidade { get; set; }
        public DateTime? CriadoEm { get; set; }
        public DateTime? AtualizadoEm { get; set; }
        public List<RepositorioExibicao>? Repositorios { get; set; }
    }
}
