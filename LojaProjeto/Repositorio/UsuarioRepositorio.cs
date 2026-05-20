using LojaProjeto.Interfaces;
using LojaProjeto.Models;
using MySql.Data.MySqlClient;


namespace LojaProjeto.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly string _connectionString;
        public UsuarioRepositorio(IConfiguration config) =>
            _connectionString = config.GetConnectionString("Conexao");
        public Usuario Validar(string email, string senha )
        {
            using var coon = new MySqlConnection(_connectionString);
            coon.Open();
            var sql = "SELECT * FROM Usuarios WHERE Email = @e AND Senha = @s";
            var cmd = new MySqlCommand(sql, coon);
            cmd.Parameters.AddWithValue("@e", email);
            cmd.Parameters.AddWithValue("@s", senha);
                
             using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Usuario
                { 
                    Id = Convert.ToInt32(reader["id"]),
                    Nome = reader["Nome"].ToString()!,
                    Email = reader["Email"].ToString()!,
                    Nivel = reader["Nivel"].ToString()!
                };
            }
            return null; 
        }
    }
}
