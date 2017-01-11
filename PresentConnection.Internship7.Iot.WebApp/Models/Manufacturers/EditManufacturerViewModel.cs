namespace PresentConnection.Internship7.Iot.WebApp.Models
{
    public class EditManufacturerViewModel
    {
        public EditManufacturerViewModel(string uniqueName)
        {
            UniqueName = uniqueName;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string UniqueName { get; }
        public string ImagePath { get; set; }
    }
}