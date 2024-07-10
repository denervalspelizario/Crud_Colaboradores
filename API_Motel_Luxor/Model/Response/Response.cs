namespace API_Motel_Luxor.Model.Response
{
    public class Response<T>
    {
        public T? Dados { get; set; }
        public string? Mensagem { get; set; }
        public string? Status { get; set; }
    }
}
