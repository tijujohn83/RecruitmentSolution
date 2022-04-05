using JobApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitmentApi.Models;
using RecruitmentApi.Service;
using System.Collections.Generic;

namespace RecruitmentApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecruitmentController : ControllerBase
    {
        private readonly ILogger<RecruitmentController> _logger;
        private readonly IRecruitmentService _recruitmentService;

        public RecruitmentController(ILogger<RecruitmentController> logger, IRecruitmentService recruitmentService)
        {
            _logger = logger;
            _recruitmentService = recruitmentService;
        }


        [HttpGet("accepted")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Candidate>> GetSelected()
        {
            //todo validate input for all methods
            return Ok(_recruitmentService.GetAcceptedCandidates());
        }

        [HttpPost("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Candidate>> Search(SearchCriteria searchCriteria)
        {            
            return Ok(_recruitmentService.SearchCandidates(searchCriteria));
        }


        [HttpPost("application/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CandidateStatus> SetCandidateStatus(CandidateStatus candidateStatus)
        {           
            return Ok(_recruitmentService.UpdateStatus(candidateStatus));
        }


    }
}
