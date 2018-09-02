using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreBeginnersCourseByMva.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace AspNetCoreBeginnersCourseByMva.Pages.Customers
{
    public class EditModel : PageModel
    {
        private readonly IAsyncDocumentSession _session;

        [BindProperty]
        public Customer Customer { get; set; }
        public EditModel(IAsyncDocumentSession session)
        {
            _session = session;
            _session.Advanced.WaitForIndexesAfterSaveChanges(timeout: TimeSpan.FromSeconds(30), throwOnTimeout: false);
        }
        public async Task OnGet(string id)
        {
            var customerFound = await _session.Query<Customer>().Where( c => c.Id == id).FirstAsync();
            
            if(customerFound != null)
            {
                Customer = new Customer() { Id = customerFound.Id, Name = customerFound.Name };
            }
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //var customerFound = await _session.LoadAsync<Customer>(Customer.Id);
            //if (customerFound != null)
            {
                await _session.StoreAsync(Customer);
                await _session.SaveChangesAsync();
            }
            return RedirectToPage("./Index");
        }
    }
}