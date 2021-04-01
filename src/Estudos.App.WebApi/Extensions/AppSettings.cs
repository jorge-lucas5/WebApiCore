namespace Estudos.App.WebApi.Extensions
{
    public class AppSettings
    {
        /// <summary>
        /// chave de criptografia
        /// </summary>
        public string Secret { get; set; }
        /// <summary>
        /// tempo de validade do token
        /// </summary>
        public int ExpiracaoHoras { get; set; }
        /// <summary>
        /// quem emite o token (a aplicação)
        /// </summary>
        public string Emissor { get; set; }
        /// <summary>
        /// indica as urls que o token é válido
        /// </summary>
        public string ValidoEm { get; set; }
    }
}