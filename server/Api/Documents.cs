using Alaska.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Api
{
    public static class Documents
    {
        internal static void MapDocumentEndPoints(this WebApplication app)
        {
            app.MapGet("/documents/images/{filename}", GetImageAsync);
            app.MapPost("/documents/images", UploadImageAsync).RequireAuthorization();
        }
        private static IResult GetImageAsync(string filename)
        {
            string sFilename = Path.Combine(AppContext.BaseDirectory, "wwwroot", "images", filename);

            if (System.IO.File.Exists(sFilename))
            {
                string contentType = GetContentType(sFilename);
                return Results.File(sFilename, contentType);
            }
            else
            {
                return Results.NotFound();
            }
        }
        private static async Task<IResult> UploadImageAsync(HttpRequest request)
        {
            if (!request.HasFormContentType)
            {
                return Results.BadRequest("Invalid form content type.");
            }

            var form = await request.ReadFormAsync();
            var file = form.Files.FirstOrDefault();

            if (file == null || file.Length == 0)
            {
                return Results.BadRequest("No file uploaded or the file is empty.");
            }

            string uploadsFolder = Path.Combine(AppContext.BaseDirectory, "wwwroot", "images");
            if (!System.IO.Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);
            string sNewFilename = DateTime.Now.Ticks.ToString() + GetExtension(file.FileName);
            string filePath = Path.Combine(uploadsFolder, sNewFilename);
            string imageURL = "/images/" + sNewFilename;
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return Results.Ok(new CommonResult() { Success = true, Message = imageURL });
        }
        private static string GetExtension(string filename)
        {
            int pos = filename.LastIndexOf('.');
            return pos >= 0 ? filename.Substring(pos) : string.Empty;
        }

        private static string GetContentType(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".bmp" => "image/bmp",
                ".svg" => "image/svg+xml",
                _ => "application/octet-stream",
            };
        }
    }
}
