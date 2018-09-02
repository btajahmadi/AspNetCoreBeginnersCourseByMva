using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreBeginnersCourseByMva.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Raven.Client.Documents.Session;

namespace AspNetCoreBeginnersCourseByMva.Pages.Customers
{
    public class CreateModel : PageModel
    {
        private readonly IAsyncDocumentSession _session;

        public CreateModel(IAsyncDocumentSession session)
        {
            _session = session ?? throw new ArgumentNullException();
        }
        [BindProperty]
        public Customer Customer { get; set; }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _session.StoreAsync(Customer);
            await _session.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}