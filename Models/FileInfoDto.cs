namespace Folder_Sublister.Models
{
    public class FileInfoDto
    {
        public string FileName { get; set; } = string.Empty;
        public string Extension { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public double SizeKB { get; set; }
    }
}
