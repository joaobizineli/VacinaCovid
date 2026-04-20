using System.Runtime.Serialization;

namespace VacinaCovid.Utilitarios.Excepetions
{
    [Serializable]
    public class BusinessListException : Exception
    {
        public List<string> Messages { get; set; }

        public BusinessListException() { }

        public BusinessListException(IEnumerable<string> messages)
        {
            Messages = messages.ToList();
        }

        protected BusinessListException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
