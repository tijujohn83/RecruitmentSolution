﻿using JobApi.Models;
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

        [HttpGet("rejected")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Candidate>> GetRejected()
        {
            //todo validate input for all methods
            return Ok(_recruitmentService.GetRejectedCandidates());
        }

        [HttpGet("technologies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Candidate>> GetTechnologies()
        {
            //todo validate input for all methods
            return Ok(_recruitmentService.GetTechnologies());
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
        public ActionResult<UpdateStatusResult> SetCandidateStatus(CandidateStatus candidateStatus)
        {
            var updateStatusResult = _recruitmentService.UpdateStatus(candidateStatus);
            if (updateStatusResult == UpdateStatusResult.NotFound)
            {
                return NotFound("Candidate does not exist");
            }
            if (updateStatusResult == UpdateStatusResult.Failure)
            {
                return BadRequest("This candidate is locked");
            }
            return Ok(updateStatusResult.ToString());
        }

    }
}
