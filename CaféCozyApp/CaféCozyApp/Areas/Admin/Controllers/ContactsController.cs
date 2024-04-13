using CaféCozyApp.Areas.Admin.ViewModels.Category;
using CaféCozyApp.Areas.Admin.ViewModels.Contact;
using CaféCozyApp.Data;
using CaféCozyApp.Models;
using CaféCozyApp.Services;
using CaféCozyApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CaféCozyApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IContactService _contact;

        public ContactsController(AppDbContext context, IContactService contact)
        {
            _context = context;
            _contact = contact;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Contact> contacts = await _contact.GetAllAsync();

            return View(contacts);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(ContactCreateVM contact)
        {
            try
            {
                Contact newContact = new()
                {
                    LocationAdress = contact.LocationAdress,
                    PhoneNumber = contact.PhoneNumber,
                    EmailAdress = contact.EmailAdress
                };

                await _context.Contacts.AddAsync(newContact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                throw;
            }
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();
            Contact dbContact = await _contact.GetByIdAsync(id);
            if (dbContact is null) return NotFound();

            ContactUpdateVM model = new()
            {
                LocationAdress = dbContact.LocationAdress,
                PhoneNumber = dbContact.PhoneNumber,
                EmailAdress = dbContact.EmailAdress
            };

            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, ContactUpdateVM contactUpdate)
        {
            try
            {
                if (id == null) return BadRequest();
                Contact dbContact = await _contact.GetByIdAsync(id);
                if (dbContact is null) return NotFound();

                ContactUpdateVM model = new()
                {
                    LocationAdress =dbContact.LocationAdress,
                    PhoneNumber = dbContact.PhoneNumber,
                    EmailAdress =dbContact.EmailAdress
                };

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                dbContact.LocationAdress = contactUpdate.LocationAdress;
                dbContact.PhoneNumber = contactUpdate.PhoneNumber;
                dbContact.EmailAdress = contactUpdate.EmailAdress;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                @ViewBag.error = ex.Message;
                return View();
            }
        }



        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null) return BadRequest();

            Contact contact = await _contact.GetByIdAsync(id);
            if (contact == null) return NotFound();

            _context.Contacts.Remove(contact);
            _context.SaveChanges();

            return Ok();
        }
    }
}
