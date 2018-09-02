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
    public class IndexModel : PageModel
    {
        private readonly IAsyncDocumentSession _session;

        public IndexModel(IAsyncDocumentSession session)
        {
            _session = session ?? throw new ArgumentNullException();
        }
        [BindProperty]
        public List<Customer> AllCustomers { get; set; }
        public async Task OnGetAsync()
        {
            AllCustomers =  await _session.Query<Customer>().ToListAsync();
        }
        public async Task<IActionResult> OnPostDelete(Guid id)
        {
            if(id == null)
            {
                return RedirectToPage();
            }
            var customer = await _session.LoadAsync<Customer>(id.ToString());
            if (customer != null)
            {
                _session.Delete(customer);
                await _session.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}