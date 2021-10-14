




using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myPicoAPI.Data;

namespace myPicoAPI.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(LogUserActivity))]
    public class OwnerController : Controller
    {

        private readonly IMapper _mapper;
        private readonly IUnit _repo;
        public OwnerController(IUnit repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

       /*  [Route("api/approveAppointment/{id}")]
        [HttpGet]
        public async Task<IActionResult> getApproveAppointment(int id)
        {

        }
 */







    }
}