namespace Acme.Core.Exceptions
{
    public class InvalidField 
    {
        public InvalidField() { }

        public InvalidField(string message, string fieldName)
        {
            Message = message;
            FieldName = fieldName;
        }

        public string FieldName { get; set; }
        public string Message { get; set; }
    }
}
