using GitHubRepo.Models;
using GitHubRepo.Models.GithubAPI;
using System.Net.Http.Headers;

namespace GitHubRepo.Servicos
{
    public static class GithubAPI
    {
        private static readonly HttpClient HttpClient = new HttpClient();
        private static IConfigurationBuilder Builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
        private static IConfiguration oConfig = Builder.Build();
        private static string UrlGithub = oConfig["UrlGithub"];
        public static async Task<ResponseAPI> PesquisarRepositorios(string pesquisa)
        {
            try
            {
         
                if (string.IsNullOrEmpty(pesquisa))
                    return new ResponseAPI { Sucesso = false, Mensagem = "Pesquisa inválida", Objeto = null };

                HttpClient.DefaultRequestHeaders.Accept.Clear();
                HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpClient.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
                using var response = await HttpClient.GetAsync($"{UrlGithub}/search/repositories?q={pesquisa}in:name&sort=stars&order=desc&per_page=50");

                if (response.IsSuccessStatusCode)
                {
                    var responseRepositorios = await response.Content.ReadFromJsonAsync<RepositoriosResponse>();
                    return new ResponseAPI
                    {
                        Sucesso = response.IsSuccessStatusCode,
                        Mensagem = "Pesquisa realizada com sucesso",
                        Objeto = responseRepositorios?.Items?.Select(x =>
                        new RepositorioExibicao
                        {
                            Id = x.Id,
                            Nome = x.Name,
                            NomeCompleto = x.Full_name,
                            Descricao = x.Description,
                            Linguagem = x.Language,
                            Usuario = new UsuarioExibicao
                            {
                                Id = x.Owner?.Id,
                                Login = x.Owner?.Login,
                                LinkPerfil = x.Owner?.Html_url,
                                Avatar = x.Owner?.Avatar_url
                            },
                            Url = x.Html_url,
                            UrlClone = x.Clone_url,
                            Visibilidade = x.Visibility,
                            CriadoEm = x.Created_at,
                            AtualizadoEm = x.Updated_at
                        }).OrderBy(x => x.Usuario?.Login).ToList()
                    };
                }
                else
                {
                    return new ResponseAPI { Sucesso = false, Mensagem = "Ocorreu um erro ao realizar a pesquisa", Objeto = null };
                };
            }
            catch (Exception ex)
            {
                return new ResponseAPI { Sucesso = false, Mensagem = ex.Message, Objeto = null };
                throw;
            }
        }

        public static async Task<ResponseAPI> ObterRepositorio(int id)
        {
            try
            {
                if (id <= 0)
                    return new ResponseAPI { Sucesso = false, Mensagem = "Id inválido", Objeto = null };

                HttpClient.DefaultRequestHeaders.Accept.Clear();
                HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpClient.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
                using var response = await HttpClient.GetAsync($"{UrlGithub}/repositories/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var responseRepositorio = await response.Content.ReadFromJsonAsync<Repositorio>();
                    return new ResponseAPI
                    {
                        Sucesso = response.IsSuccessStatusCode,
                        Mensagem = "Pesquisa realizada com sucesso",
                        Objeto = new RepositorioExibicao
                        {
                            Id = responseRepositorio != null ? responseRepositorio.Id : 0,
                            Nome = responseRepositorio?.Name,
                            NomeCompleto = responseRepositorio?.Full_name,
                            Descricao = responseRepositorio?.Description,
                            Linguagem = responseRepositorio?.Language,
                            Url = responseRepositorio?.Html_url,
                            UrlClone = responseRepositorio?.Clone_url,
                            Usuario = new UsuarioExibicao
                            {
                                Id = responseRepositorio?.Owner?.Id,
                                Login = responseRepositorio?.Owner?.Login,
                                LinkPerfil = responseRepositorio?.Owner?.Html_url,
                                Avatar = responseRepositorio?.Owner?.Avatar_url
                            },
                            Visibilidade = responseRepositorio?.Visibility,
                            CriadoEm = responseRepositorio?.Created_at,
                            AtualizadoEm = responseRepositorio?.Updated_at
                        }
                    };
                }
                else
                {
                    var responseErro = await response.Content.ReadFromJsonAsync<ErroAPI>();
                    return new ResponseAPI { Sucesso = false, Mensagem = responseErro?.Message, Objeto = null};
                };
            }
            catch (Exception ex)
            {
                return new ResponseAPI { Sucesso = false, Mensagem = ex.Message, Objeto = null };
                throw;
            }
        }

        public static async Task<ResponseAPI> PesquisarUsuarios(string pesquisa)
        {
            try
            {
                if (string.IsNullOrEmpty(pesquisa))
                    return new ResponseAPI { Sucesso = false, Mensagem = "Pesquisa inválida", Objeto = null };

                HttpClient.DefaultRequestHeaders.Accept.Clear();
                HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpClient.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
                using var response = await HttpClient.GetAsync($"{UrlGithub}/search/users?q={pesquisa}&sort=stars&order=desc&per_page=50");

                if (response.IsSuccessStatusCode)
                {
                    var responseUsuarios = await response.Content.ReadFromJsonAsync<UsuarioResponse>();
                    return new ResponseAPI
                    {
                        Sucesso = response.IsSuccessStatusCode,
                        Mensagem = "Pesquisa realizada com sucesso",
                        Objeto = responseUsuarios?.Items?.Select(x => new UsuarioExibicao
                        {
                            Id = x.Id,
                            Login = x.Login,
                            Avatar = x.Avatar_url,
                            LinkPerfil = x.Html_url
                        }).OrderBy(y => y.Login).ToList()
                    };
                }
                else
                {
                    return new ResponseAPI { Sucesso = false, Mensagem = "Ocorreu um erro ao realizar a pesquisa", Objeto = null };
                };
            }
            catch (Exception ex)
            {
                return new ResponseAPI { Sucesso = false, Mensagem = ex.Message, Objeto = null };
                throw;
            }
        }

        public static async Task<ResponseAPI> ObterUsuario(int id)
        {
            try
            {
                if (id <= 0)
                    return new ResponseAPI { Sucesso = false, Mensagem = "Id inválido", Objeto = null };

                HttpClient.DefaultRequestHeaders.Accept.Clear();
                HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpClient.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
                using var response = await HttpClient.GetAsync($"{UrlGithub}/user/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var responseUsuario = await response.Content.ReadFromJsonAsync<Usuario>();

                    return new ResponseAPI
                    {
                        Sucesso = response.IsSuccessStatusCode,
                        Mensagem = "Pesquisa realizada com sucesso",
                        Objeto = new UsuarioExibicao
                        {
                            Id = responseUsuario?.Id,
                            Login = responseUsuario?.Login,
                            Email = responseUsuario?.Email,
                            Nome = responseUsuario?.Name,
                            Avatar = responseUsuario?.Avatar_url,
                            LinkPerfil = responseUsuario?.Html_url,
                            Empresa = responseUsuario?.Company,
                            Seguidores = responseUsuario?.Followers,
                            Seguindo = responseUsuario?.Following,
                            Blog = responseUsuario?.Blog,
                            CriadoEm = responseUsuario?.Created_at,
                            AtualizadoEm = responseUsuario?.Updated_at,
                            Localidade = responseUsuario?.Location,
                            Repositorios = await ObterRepositoriosUsuario(id),
                             
                            
                        }
                    };
                }
                else
                {
                    var responseErro = await response.Content.ReadFromJsonAsync<ErroAPI>();
                    return new ResponseAPI { Sucesso = false, Mensagem = responseErro?.Message, Objeto = null };
                };
            }
            catch (Exception ex)
            {
                return new ResponseAPI { Sucesso = false, Mensagem = ex.Message, Objeto = null };
                throw;
            }
        }

        public static async Task<List<RepositorioExibicao>?> ObterRepositoriosUsuario(int id)
        {

            try
            {
                HttpClient.DefaultRequestHeaders.Accept.Clear();
                HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpClient.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
                using var response = await HttpClient.GetAsync($"{UrlGithub}/user/{id}/repos");

                if (response.IsSuccessStatusCode)
                {
                    var responseRepositorios = await response.Content.ReadFromJsonAsync<List<Repositorio>>();
                    var responseRepositoriosUsuario = responseRepositorios?.Select(x => new RepositorioExibicao
                    {
                        Id = x.Id,
                        Nome = x.Name,
                        NomeCompleto = x.Full_name,
                        Descricao = x.Description,
                        Linguagem = x.Language,
                        Url = x.Html_url,
                        UrlClone = x.Clone_url,
                        Usuario = new UsuarioExibicao
                        {
                            Id = x.Owner?.Id,
                            Login = x.Owner?.Login,
                            Avatar = x.Owner?.Avatar_url,
                            LinkPerfil = x.Owner?.Html_url
                        },
                        Visibilidade = x.Visibility,
                        CriadoEm = x.Created_at,
                        AtualizadoEm = x.Updated_at
                    }).OrderBy(y => y.Nome).ToList();

                    return responseRepositoriosUsuario;
                }
                else
                {
                    return null;
                };
            }
            catch (Exception)
            {
                return null;
                throw;
            }

        }
    }
}
