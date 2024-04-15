using CaféCozyApp.Data;
using CaféCozyApp.Models;
using CaféCozyApp.Services.Interfaces;
using CaféCozyApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaféCozyApp.Controllers
{
    public class ContactController : Controller
    {

        private readonly IContactService _contactService;
        private readonly AppDbContext _context;


        public ContactController(IContactService contactService,AppDbContext context)
        {
            _contactService = contactService;
            _context= context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Contact> contacts = await _contactService.GetAllAsync();

            ContactVM contactVM = new ContactVM
            {
                Contacts = contacts
            };


            return View(contactVM);
        }


        [HttpPost]
        public async Task<IActionResult> Index(Message message)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View();
            //}

            _context.Messages.AddAsync(message);
            _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
