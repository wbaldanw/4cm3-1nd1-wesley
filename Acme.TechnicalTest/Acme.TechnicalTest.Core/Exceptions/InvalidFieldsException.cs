namespace Acme.Core.Exceptions
{
    public class InvalidFieldsException : Exception
    {
        public InvalidFieldsException() { }
        public InvalidFieldsException(List<InvalidField> invalidFields) 
        {
            _invalidFields = invalidFields;
        }
        public InvalidFieldsException(params InvalidField[] invalidFields)
        {
            _invalidFields = invalidFields.ToList();
        }        public InvalidFieldsException(IEnumerable<InvalidField> invalidFields)
        {
            _invalidFields = invalidFields.ToList();
        }
        public InvalidFieldsException(string fieldName, string message)
        {
            _invalidFields.Add(new InvalidField(message, fieldName));
        }

        private List<InvalidField> _invalidFields = new List<InvalidField>();
        public IReadOnlyCollection<InvalidField> InvalidFields => _invalidFields.ToList().AsReadOnly();

        public void AddInvalidField(string fieldName, string message) => _invalidFields.Add(new InvalidField(message, fieldName));

        public void AddInvalidFields(IEnumerable<InvalidField> invalidFields) => _invalidFields.AddRange(invalidFields);
        
        public bool HasInvalidFields => _invalidFields.Any();

        public override string Message => this.ToString();

        public override string ToString() => string.Join(Environment.NewLine, _invalidFields.Select(x => x.Message).ToList());

        public Dictionary<string, IList<string>> ToErrors()
        {
            var dictionary = new Dictionary<string, IList<string>>();

            var errorsGroupByName = _invalidFields.GroupBy(x => x.FieldName);
            foreach ( var errorGroup in errorsGroupByName)
            {
                dictionary.Add(errorGroup.Key, errorGroup.Select(x => x.Message).ToList());
            }

            return dictionary;
        }
    }    
}
