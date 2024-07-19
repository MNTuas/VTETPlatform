using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using VTET.Business;
using Models = VTET.Data.Models;

namespace VTET.RazorWebApp.Pages.Watch
{
    public class AddWatchModel : PageModel
    {
        private readonly IWatchBusiness _watchBusiness;
        private readonly IHostEnvironment _environment;
        [BindProperty]
        public IFormFile FileUpload { get; set; }
        public string Message { get; set; } = default;
        [BindProperty]
        public Models.Watch Watches { get; set; } = new Models.Watch();
        //add watch
        public AddWatchModel(IHostEnvironment environment)
        {
            _watchBusiness ??= new watchBusiness();
            _environment = environment;

        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (FileUpload != null)
                {
                    var fileName = FileUpload.FileName.Split('.');
                    var newFileName = $"{fileName[0]}{DateTime.Now.ToString("yyyyMMddHHmmss")}.{fileName[fileName.Length - 1]}";
                    var path = Path.Combine(_environment.ContentRootPath, "wwwroot/image", newFileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await FileUpload.CopyToAsync(fileStream);
                    }
                    Watches.Picture = $"{Request.Scheme}://{Request.Host}/image/{newFileName}";
                }
                var result = await _watchBusiness.Save(Watches);
                if (result.Status > 0)
                {
                    TempData["StatusMessage"] = result.Message;
                }
                else
                {
                    TempData["StatusMessage"] = "Error: " + result.Message;
                }
            }
            catch (Exception ex)
            {
                TempData["StatusMessage"] = ex.Message;
            }




            return RedirectToPage("/HomePage");
        }
        private async Task SaveWatch()
        {
            var watchResult = await _watchBusiness.Save(Watches);

            if (watchResult != null)
            {
                Message = watchResult.Message;
            }
            else
            {
                Message = "Error system";
            }
        }
    }
}
