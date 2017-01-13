using MongoDB.Driver;

namespace PresentConnection.Internship7.Iot.Utils
{
    public interface IPagination
    {
        string Column { get; set; }
        SortDirection Direction { get; set; }
        int PageSize { get; set; }
        int Page { get; set; }
    }
}
