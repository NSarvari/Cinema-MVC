using Cinema.Service.IService;

namespace Cinema.Service
{
    public class FileService : IFileService
    {
        IWebHostEnvironment _environment;

        public FileService(IWebHostEnvironment environment)
        {
            this._environment = environment;
        }

        public Tuple<int, string> SaveImage
            (IFormFile imageFile)
        {
            try
            {
                var path = this._environment.WebRootPath;
                var pathFile = Path.Combine(path, "Upload");
                if (!Directory.Exists(pathFile))
                {
                    Directory.CreateDirectory(pathFile);
                }

                //Check the allowed Extentions
                var ext = Path.GetExtension(imageFile.FileName);
                var allowedExtentions = new string[]
                {"jpg","png","jpeg"};

                if (!allowedExtentions.Contains(ext))
                {
                    string msg = string.Format(
                        "Only {0} extentions are allowed",
                        string.Join(", ", allowedExtentions));
                    return new Tuple<int, string>(0, msg);
                }
                string uniqueString = Guid.NewGuid().ToString();
                var newFileName = uniqueString + ext;
                var fileWithPath = Path.Combine(path, newFileName);
                var stream = new FileStream(fileWithPath, FileMode.Create);
                imageFile.CopyTo(stream);
                stream.Close();
                return new Tuple<int, string>(1, newFileName);
            }
            catch (Exception ex)
            {

                return new Tuple<int, string>(0, "Error has occured.");
            }
        }

        public bool DeleteImage(string imageFileName)
        {
            try
            {
                var wwwPath = this._environment.WebRootPath;
                var path = Path.Combine(wwwPath,"Uploads\\", imageFileName);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
