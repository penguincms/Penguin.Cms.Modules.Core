using Microsoft.AspNetCore.Mvc;

namespace Penguin.Cms.Modules.Core.Controllers
{
    public abstract class ModuleController : Controller
    {
        public virtual IActionResult Index(string Id = "")
        {
            return View((object)Id);
        }
    }
}