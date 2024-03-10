using Microsoft.AspNetCore.Mvc;
using PoliceCrimeExplorer.Models;
using System.Diagnostics;

namespace PoliceCrimeExplorer.Controllers
{
    public class HomeController(ILogger<HomeController> logger) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        #region Police API Interactions
        /// <summary>
        /// Will retrieve crimes that have been commited within a one mile radius from a GPS location 
        /// </summary>
        /// <param name="gpsCord1"></param>
        /// <param name="gpsCord2"></param>
        /// <param name="searchDate"></param>
        /// <returns></returns>
        public IActionResult GetPoliceStreetCrimeDataInLocation(string gpsCord1, string gpsCord2, string searchDate)
        {
            var apiData = "LALALA";//_apiService.GetDataFromApi(); // Fetch data from the API
            return Json(apiData); // Return data as JSON
        }
        #endregion


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
