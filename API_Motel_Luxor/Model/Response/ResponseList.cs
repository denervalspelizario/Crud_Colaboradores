namespace API_Motel_Luxor.Model.Response
{
    public class ResponseList<T>
    {
        public T? Dados { get; set; }
        public string? Mensagem { get; set; }
    }
}
