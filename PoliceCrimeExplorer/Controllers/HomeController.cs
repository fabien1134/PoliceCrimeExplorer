using Microsoft.AspNetCore.Mvc;
using PoliceCrimeExplorer.Models;
using PoliceCrimeExplorer.Services;
using PoliceCrimeExplorer.ViewModels;
using System.Diagnostics;

namespace PoliceCrimeExplorer.Controllers
{
    public class HomeController(ILogger<HomeController> logger, IPoliceDataClientService policeDataClientService, IPoliceDataCalendarService policeDataCalendarService) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;
        private readonly IPoliceDataClientService _policeDataClientService = policeDataClientService;
        private readonly IPoliceDataCalendarService _policeDataCalendarService = policeDataCalendarService;

        public async Task<IActionResult> Index()
        {
            var policeDataUpdateViewModel = new PoliceDataUpdateViewModel();

            try
            {
                policeDataUpdateViewModel.LastPoliceDataUpdate = await _policeDataClientService.GetLastPoliceDataUpdate();
                policeDataUpdateViewModel.SuccessfullyRetrievedLastPoliceDataUpdate = true;
                policeDataUpdateViewModel.CalendarMaxDate = _policeDataCalendarService.GetCalendarMaximumDateBasedOnPoliceDataLastUpdate(DateTime.Today, policeDataUpdateViewModel.LastPoliceDataUpdate);
            }
            catch (Exception ex)
            {

            }

            return View(policeDataUpdateViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        #region MVC Actions
        /// <summary>
        /// Will retrieve crimes that have been commited within a one mile radius from a GPS location 
        /// </summary>
        /// <param name="latitudeGpsCord"></param>
        /// <param name="longitudeGpsCord"></param>
        /// <param name="searchDate"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetPoliceStreetCrimeDataInLocation(string latitudeGpsCord, string longitudeGpsCord, DateTime searchDate)
        {
            try
            {
                var streetCrimeNearGpsLocationData = await _policeDataClientService.GetStreetCrimeNearGpsLocation(latitudeGpsCord, longitudeGpsCord, searchDate);

                return Json(streetCrimeNearGpsLocationData); // Return data as JSON
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        #endregion


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
