using Microsoft.AspNetCore.Mvc;
using Penguin.Cms.Core.Services;
using Penguin.Extensions.Collections;
using Penguin.Navigation.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Penguin.Cms.Modules.Core.ViewComponents
{
    [ViewComponent]
    public class NavigationMenu : ViewComponent
    {
        private ComponentService ComponentService { get; set; }

        public NavigationMenu(ComponentService componentService)
        {
            ComponentService = componentService;
        }

        public IViewComponentResult Invoke(string name)
        {
            List<INavigationMenu> Menus = ComponentService.GetComponents<INavigationMenu, string>(name).ToList();

            //INavigationMenu Template = Menus.FirstOrDefault();

            Navigation.NavigationMenu Template = Activator.CreateInstance<Navigation.NavigationMenu>();

            Template.Name = name;
            Template.Text = name;

            foreach (INavigationMenu additional in Menus)
            {
                if (additional.Name == Template.Name && additional.Children.AnyNotNull())
                {
                    foreach (INavigationMenu child in additional.Children)
                    {
                        Template.Children.Add(child);
                    }
                }
            }

            return View(Template);
        }
    }
}