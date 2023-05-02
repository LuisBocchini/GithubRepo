namespace GitHubRepo.Models.GithubAPI
{
    public class UsuarioResponse
    {
        public int Total_count { get; set; }
        public bool Incomplete_results { get; set; }
        public List<Usuario>? Items { get; set; }
    }
}
