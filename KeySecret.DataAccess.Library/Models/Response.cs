namespace KeySecret.DataAccess.Library.Models
{
    public class Response
    {
        public string Status { get; set; }
        public string Message { get; set; }

        public override string ToString()
            => Status + ": " + Message;
    }
}