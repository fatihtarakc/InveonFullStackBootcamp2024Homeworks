﻿namespace Library.MVC.Controllers
{
    public class BaseController : Controller
    {
        protected INotyfService notyfService => HttpContext.RequestServices.GetService<INotyfService>();

        public void NotifyInfo(string message) => notyfService.Information(message);

        public void NotifySuccess(string message) => notyfService.Success(message);

        public void NotifyError(string message) => notyfService.Error(message);
    }
}