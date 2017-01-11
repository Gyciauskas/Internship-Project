using System.Collections.Generic;
using CodeMash.Net.DataContracts;

namespace PresentConnection.Internship7.Iot.WebApp.Models
{
    public class ListViewModelBase<T> : ListRequestBase
    {
        public ListViewModelBase()
        {
            Items = new List<T>();
        }

        public List<T> Items { get; set; }

        public int TotalCount { get; set; }
    }
}