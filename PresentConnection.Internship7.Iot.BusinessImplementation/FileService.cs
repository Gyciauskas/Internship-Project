using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Utils;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.BusinessImplementation
{
    public class FileService : IFileService
    {
        private static readonly object lockObject = new object();
        readonly string defaultPath = @"C:\temp";

        public string ConcatDirs(params string[] parts)
        {
            if (parts == null || parts.Length == 0)
            {
                return string.Empty;
            }

            if (parts.Length == 1)
            {
                return parts[0];
            }

            var sb = new StringBuilder();
            sb.Append(string.Concat(parts.Take(parts.Length - 1).Select(this.FormatDirPart)));
            sb.Append(parts[parts.Length - 1]);
            return sb.ToString();
        }

        public void CreateFolder(string dir)
        {
            try
            {
                lock (lockObject)
                {
                    Directory.CreateDirectory(dir);
                }
            }
            catch (Exception ex)
            {
                string message = $"Failed to create folder {dir}.";
                throw new BusinessException(message, ex);
            }
        }

        public void DeleteFile(string dir, string fileName)
        {
            string path = ConcatPath(dir, fileName);
            try
            {
                lock (lockObject)
                {
                    File.Delete(path);
                }
            }
            catch (Exception ex)
            {
                string message = $"Failed to delete file {path}.";
                throw new BusinessException(message, ex);
            }
        }

        public void DeleteFolder(string dir)
        {
            string path = EnsureEndWithSlash(dir);
            try
            {
                lock (lockObject)
                {
                    Directory.Delete(path, true);
                }
            }
            catch (Exception ex)
            {
                string message = $"Failed to delete folder {path}.";
                throw new BusinessException(message, ex);
            }
        }

        public byte[] DownloadFile(string dir, string fileName)
        {
            string path = ConcatPath(dir, fileName);
            try
            {
                lock (lockObject)
                {
                    return File.ReadAllBytes(path);
                }
            }
            catch (Exception ex)
            {
                string message = $"Failed to download file {path}.";
                throw new BusinessException(message, ex);
            }
        }

        public bool ExistFolder(string dir)
        {
            string path = EnsureEndWithSlash(dir);
            try
            {
                return Directory.Exists(path);
            }
            catch (Exception ex)
            {
                string message = $"Failed to check if exist folder  {path}.";
                throw new BusinessException(message, ex);
            }
        }

        public IEnumerable<string> GetFileList(string dir)
        {
            string path = this.EnsureEndWithSlash(dir);
            try
            {
                lock (lockObject)
                {
                    string[] files = Directory.GetFiles(path);
                    return files.Select(Path.GetFileName);
                }
            }
            catch (Exception ex)
            {
                string message = $"Failed to get file list from {path}.";
                throw new BusinessException(message, ex);
            }
        }

        public void UploadFile(string dir, string fileName, byte[] file)
        {
            if (string.IsNullOrEmpty(dir))
            {
                dir = defaultPath;
            }

            string path = ConcatPath(dir, fileName);

            try
            {
                lock (lockObject)
                {
                    File.WriteAllBytes(path, file);
                }
            }
            catch (Exception ex)
            {
                string message = $"Failed to upload file {path}.";
                throw new BusinessException(message, ex);
            }
        }

        private string ConcatPath(string path, string name)
        {
            path = EnsureEndWithSlash(path);
            return string.Concat(path, name);
        }

        private string EnsureEndWithSlash(string path)
        {
            if (path.EndsWith("\\"))
            {
                return path;
            }

            return string.Concat(path, "\\");
        }

        private string FormatDirPart(string part)
        {
            if (part.StartsWith("\\"))
            {
                return EnsureEndWithSlash(part.Substring(1));
            }

            return EnsureEndWithSlash(part);
        }
    }
}
