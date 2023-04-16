using server.DataAccess.Entities;
using server.ViewModel.Posts;
using System.Text.RegularExpressions;

namespace server.Helper
{
    public static class Utilities
    {
        public static string STATIC_IMAGE_PATH = @"/Uploads/Images/";
        public static async Task<bool> UploadFile(IFormFile file, string filePath, string fileName = null)
        {
            try
            {
                CreateIfMissing(filePath);

                string pathFile = Path.Combine(filePath,fileName);

                var supportedTypes = new[] { "jpg", "jpeg", "png", "gif","webp" };
                var fileExt = Path.GetExtension(file.FileName).Substring(1);

                if (!supportedTypes.Contains(fileExt.ToLower())) /// Khác các file định nghĩa
                {
                    return false;
                }
                else
                {
                    using (var stream = new FileStream(pathFile, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static void CreateIfMissing(string path)
        {
            bool folderExists = Directory.Exists(path);
            if (!folderExists)
                Directory.CreateDirectory(path);
        }
        public static string SEOUrl(string url)
        {
            url = url.ToLower();
            url = Regex.Replace(url, @"[áàạảãâấầậẩẫăắằặẳẵ]", "a");
            url = Regex.Replace(url, @"[éèẹẻẽêếềệểễ]", "e");
            url = Regex.Replace(url, @"[óòọỏõôốồộổỗơớờợởỡ]", "o");
            url = Regex.Replace(url, @"[íìịỉĩ]", "i");
            url = Regex.Replace(url, @"[ýỳỵỉỹ]", "y");
            url = Regex.Replace(url, @"[úùụủũưứừựửữ]", "u");
            url = Regex.Replace(url, @"[đ]", "d");
            url = Regex.Replace(url, @"_", "-");

            //2. Chỉ cho phép nhận:[0-9a-z-\s]
            url = Regex.Replace(url.Trim(), @"[^0-9a-z-\s]", "").Trim();
            //xử lý nhiều hơn 1 khoảng trắng --> 1 kt
            url = Regex.Replace(url.Trim(), @"\s+", "-");
            //thay khoảng trắng bằng -
            url = Regex.Replace(url, @"\s", "-");
            while (true)
            {
                if (url.IndexOf("--") != -1)
                {
                    url = url.Replace("--", "-");
                }
                else
                {
                    break;
                }
            }
            return url;
        }
        public static PostViewModel MapToPostModel(Post post , PostLang postLang)
        {
            return new PostViewModel
            {
                Id = postLang.Id,
                PostId = post.Id,
                CategoryId = post.CategoryId,
                LanguageId = postLang.LanguageId,
                Thumbnail = post.Thumbnail,
                Title = postLang.Title,
                Body = postLang.Body,
                ShortDescription = postLang.ShortDescription,
                Author = post.Author,
                CreatedDate = post.CreatedDate,
                Views = post.Views,
                Slug = postLang.Slug,
                IsShowOnHomePage = post.IsShowOnHomePage
            };
        }
    }
}
