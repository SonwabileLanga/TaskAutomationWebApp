using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;

namespace TaskAutomationWebApp.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string DirectoryPath { get; set; }
        public string Message { get; set; }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            if (!string.IsNullOrEmpty(DirectoryPath) && Directory.Exists(DirectoryPath))
            {
                OrganizeFiles(DirectoryPath);
                Message = "Files organized successfully!";
            }
            else
            {
                Message = "The specified directory does not exist.";
            }
        }

        private void OrganizeFiles(string path)
        {
            string[] folders = { "Images", "Documents", "Audio", "Videos", "Others" };

            foreach (string folder in folders)
            {
                Directory.CreateDirectory(Path.Combine(path, folder));
            }

            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                string extension = Path.GetExtension(file).ToLower();
                string destinationFolder;

                switch (extension)
                {
                    case ".jpg":
                    case ".jpeg":
                    case ".png":
                    case ".gif":
                        destinationFolder = "Images";
                        break;
                    case ".pdf":
                    case ".doc":
                    case ".docx":
                    case ".txt":
                        destinationFolder = "Documents";
                        break;
                    case ".mp3":
                    case ".wav":
                        destinationFolder = "Audio";
                        break;
                    case ".mp4":
                    case ".avi":
                        destinationFolder = "Videos";
                        break;
                    default:
                        destinationFolder = "Others";
                        break;
                }

                string destPath = Path.Combine(path, destinationFolder, Path.GetFileName(file));
                File.Move(file, destPath);
                return File(byteArray, "application/octet-stream");
            }
        }
    }
}
