﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Api.Models;
using UserService.Api.Queries;


namespace UserService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("allusers")]
        public async Task<IActionResult> GetAll()
        {
            var users =  await _mediator.Send(new GetAllUsersQuery());
            return Ok(users);
        }
        [HttpGet]
        [Route("allusers/details")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllDetails()
        {
            var users = await _mediator.Send(new GetAllUsersDetailsQuery());
            return Ok(users);
        }

        [HttpGet]
        [Route("user/{id=}")]
        public async Task<IActionResult> GetById(string? id=null)
        {
            var data = await _mediator.Send(new GetUserByIdQuery(id!));
            return data.Match<IActionResult>(
                result => result == null ? NotFound() : Ok(result),
                error => BadRequest(error)
            );
                
        }

        [HttpGet]
        [Route("user/details/{id=}")]
        public async Task<IActionResult> GetDetailsById(string? id=null)
        {
            var data = await _mediator.Send(new GetUserDetailsByIdQuery(id!));
            return data.Match<IActionResult>(
                result => result == null ? NotFound() : Ok(result),
                error => BadRequest(error)
            );

        }

        [HttpPost]
        [Route("user/create")]
        public async Task<IActionResult> Create([FromBody] UserRequest request)
        {
            var data = await _mediator.Send(request);
            return data.Match<IActionResult>(
                result => Ok(result) ,
                error => BadRequest(error)
            );
        }

        [HttpDelete]
        [Route("user/delete/id/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _mediator.Send(new DeleteUserQuery(id));
            return data.Match<IActionResult>(
                result => result == null ? NotFound() : Ok(result),
                error => BadRequest(error)
            );
        }
        [HttpDelete]
        [Route("user/delete/userid/{userid}")]
        public async Task<IActionResult> Delete([FromRoute]string userid)
        {
            var data = await _mediator.Send(new DeleteUserQuery(userid));
            return data.Match<IActionResult>(
                result => result == null ? NotFound() : Ok(result),
                error => BadRequest(error)
            );
        }
    }
}
