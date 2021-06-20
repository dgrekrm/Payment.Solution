using Microsoft.AspNetCore.Mvc;
using Payment.Solution.DataAccessLayer.Interfaces;
using Payment.Solution.RuntimeModels;
using Payment.Solution.ViewModels;
using System.Linq;

namespace Payment.Solution.WebApplication.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IRepository<Invoice> _invoiceRepository;

        public InvoiceController(IRepository<Invoice> invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MyInvoices()
        {
            if (HttpContext.Session.TryGetValue("IdentityNumber", out byte[] identityNumber))
            {
                return View(_invoiceRepository
                    .Get($"{nameof(InvoiceViewModel.IdentityNumber)} = '{System.Text.Encoding.UTF8.GetString(identityNumber).Replace("\"", "")}'")
                    .Select(s => (InvoiceViewModel)s)
                    .ToList());
            }
            else
            {
                ViewBag.Error = "Unauthorized access";
            }
            return View();
        }

        [HttpPost]
        public IActionResult GetInvoices(int paymentStatus)
        {
            if (HttpContext.Session.TryGetValue("IdentityNumber", out byte[] identityNumber))
            {
                return Json(new
                {
                    status = 1,
                    invoices = _invoiceRepository
                    .Get($"{nameof(InvoiceViewModel.IdentityNumber)} = '{System.Text.Encoding.UTF8.GetString(identityNumber).Replace("\"", "")}' " +
                    (paymentStatus == -1 ? string.Empty : $"AND {nameof(InvoiceViewModel.PaymentStatus)} = {paymentStatus}"))
                    .Select(s => (InvoiceViewModel)s)
                    .ToList()
                });
            }
            else
            {
                return Json(new
                {
                    status = 0,
                    message = "Unauthorized access"
                });
            }
        }

        [HttpPost]
        public IActionResult DoPayment(string invoiceId)
        {
            try
            {
                _invoiceRepository
                .Update($"{nameof(Invoice.Id)} = '{invoiceId}'", $"{nameof(Invoice.PaymentStatus)} = {1}");
                return Json(new { status = 1 });
            }
            catch (System.Exception ex)
            {
                return Json(new { status = 0, message = ex.ToString() });
            }
        }
    }
}
