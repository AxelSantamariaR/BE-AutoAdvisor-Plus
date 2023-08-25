namespace Shared
{
    public class Respuesta
    {
        public string? Title { get; set; }
        public string? Message { get; set; }

        public Respuesta(string Title, string Message) {
            this.Title = Title;
            this.Message = Message;
        }

    }
}