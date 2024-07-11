using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VTET.Business;
using Models = VTET.Data.Models;

namespace VTET.RazorWebApp.Pages.CustomerPage
{
    public class IndexModel : PageModel
    {
        private readonly IOrderBusiness _orderBusiness;
        private readonly ICustomerBusiness _customerBusiness;
        public IndexModel()
        {
            _orderBusiness ??= new OrderBusiness();
            _customerBusiness ??= new customerBusiness();
        }
        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public string SearchField { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public int PageSize { get; set; } = 10;
        public IList<Models.Customer> Customer { get; set; } = default!;
        public int TotalPages { get; set; }

        public async Task OnGetAsync()
        {
            var result = await _customerBusiness.GetAll();
            if (result != null && result.Status > 0 && result.Data != null)
            {
                Customer = result.Data as List<Models.Customer>;
               
                    if (!string.IsNullOrEmpty(SearchTerm) && !string.IsNullOrEmpty(SearchField))
                    {
                        switch (SearchField)
                        {
                            case "FullName":
                                Customer = Customer.Where(c => c.FullName != null && c.FullName != null && c.FullName.ToLower().Contains(SearchTerm.ToLower())).ToList();
                                break;
                            case "Gender":
                            Customer = Customer.Where(c => c.Gender != null && c.Gender != null && c.Gender.ToLower().Contains(SearchTerm.ToLower())).ToList();
                            break;
                            case "Role":
                            Customer = Customer.Where(c => c.Role != null && c.Role != null && c.Role.ToLower().Contains(SearchTerm.ToLower())).ToList();
                            break;
                            case "Phone":
                                Customer = Customer.Where(c => c.Phone != null && c.Phone.ToString().ToLower().Contains(SearchTerm.ToLower())).ToList();
                                break;

                            case "CreateDate":
                                Customer = Customer.Where(c => c.CreateDate != null && c.CreateDate.ToString().ToLower().Contains(SearchTerm.ToLower())).ToList();
                                break;
                            case "Email":
                                Customer = Customer.Where(c => c.Email != null && c.Email != null && c.Email.ToLower().Contains(SearchTerm.ToLower())).ToList();
                                break;                     
                    }
                    TotalPages = (int)Math.Ceiling(Customer.Count / (double)PageSize);


                    int startIndex = (PageIndex - 1) * PageSize;
                    Customer = Customer.Skip(startIndex).Take(PageSize).ToList();

                }
            }
        }
    }
}
