namespace GitHubRepo.Models
{
 
    public class RepositoriosResponse
    {
        public int Total_count { get; set; }
        public bool Incomplete_results { get; set; }
        public List<Repositorio>? Items { get; set; }
    }

    public class Repositorio
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Full_name { get; set; }
        public string? Visibility { get; set; }
        public string? Language { get; set; }
        public string? Html_url { get; set; }
        public string? Description { get; set; }
        public string? Clone_url { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }
        public Usuario? Owner { get; set; }
    }

    public class Usuario
    {
        public int Id { get; set; }
        public string? Login { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Avatar_url { get; set; }
        public string? Html_url { get; set; }
        public string? Repos_url { get; set; }
        public string? Company { get; set; }
        public int? Followers { get; set; }
        public int? Following { get; set; }
        public string? Blog { get; set; }
        public string? Twitter_username { get; set; }
        public string? Location { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }
    }
}
