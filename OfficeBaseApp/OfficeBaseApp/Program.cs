using OfficeBaseApp.Entities;
using OfficeBaseApp.Repositories;
using OfficeBaseApp.Data;
using OfficeBaseApp.Repositories.Extensions;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;


namespace OfficeBaseApp;

class Program
{
    static void Main(string[] args)
    {
        Console.Title = "OfficeBaseApp v1";
        InitRepositories.Init();
        TextMenu.InitializeMenuActionMap();
        TextMenu.MenuStart();


        //customerRepository.Remove(customerRepository.GetItem("Maxim (US)"));
        //customerRepository.WriteAllToConsole();
        //vendorRepository.Remove(vendorRepository.GetItem("Boeing INC"));
        //vendorRepository.WriteAllToConsole();

    }
}