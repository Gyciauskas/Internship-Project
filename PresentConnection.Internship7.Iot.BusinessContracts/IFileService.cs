using System.Collections.Generic;

namespace PresentConnection.Internship7.Iot.BusinessContracts
{
    public interface IFileService
    {
        string ConcatDirs(params string[] parts);
        void CreateFolder(string dir);
        void DeleteFile(string dir, string fileName);
        void DeleteFolder(string dir);
        byte[] DownloadFile(string dir, string fileName);
        bool ExistFolder(string dir);
        IEnumerable<string> GetFileList(string dir);
        void UploadFile(string dir, string fileName, byte[] file);
    }
}
