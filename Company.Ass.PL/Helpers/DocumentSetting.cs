using NuGet.Packaging.Signing;

namespace Company.Ass.PL.Helpers
{
    public static class DocumentSetting
    {
        //1. Upload
        //ImageName
        public static string UploadFile(IFormFile file, string folderName)
        {
            //1. Get Folder Location

            //var floderPath = Directory.GetCurrentDirectory() + "\\wwwroot\\files"+ folderName;
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"WWWROOT\files", folderName);

            //2. Get File Name And Make It Unique

            var fileName = $"{Guid.NewGuid()}{file.FileName}";
            // File Path

            var filepath = Path.Combine(folderPath, fileName);
            var fileStream = new FileStream(filepath, FileMode.Create);

            file.CopyTo(fileStream);
            return fileName;
        }
        //2. Delete

        public static void DeleteFile(string fileName, string folderName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName, fileName);

            if (File.Exists(filePath))
                File.Delete(filePath);
        }

    }
}
