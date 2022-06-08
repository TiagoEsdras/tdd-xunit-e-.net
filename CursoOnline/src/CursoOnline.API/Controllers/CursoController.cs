using CursoOnline.Application.Contratos;
using CursoOnline.Application.Dtos.Cursos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CursoOnline.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CursoController : ControllerBase
    {
        private readonly ICursoService _cursoService;

        public CursoController(ICursoService cursoService)
        {
            _cursoService = cursoService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<CursoDto>))]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var cursos = await _cursoService.ObterCursos();
                return Ok(cursos);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 500 };
            }
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Post(CreateCursoDto cursoDto)
        {
            try
            {
                await _cursoService.Adicionar(cursoDto);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 500 };
            }
        }
    }
}