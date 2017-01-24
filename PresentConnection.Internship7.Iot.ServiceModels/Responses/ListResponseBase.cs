using System.Runtime.Serialization;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    public class ListResponseBase<T> : ResponseBase<T>
    {
        protected ListResponseBase()
        {
        }

        protected ListResponseBase(T result) : base(result)
        {
            Result = result;
        }

        [DataMember]
        public int TotalCount { get; set; }
    }
}