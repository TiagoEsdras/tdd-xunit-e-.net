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

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(CursoDto))]
        public async Task<IActionResult> GetBiId(Guid id)
        {
            try
            {
                var curso = await _cursoService.ObterPorId(id);
                return Ok(curso);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 500 };
            }
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(CursoDto))]
        public async Task<IActionResult> Post(CreateCursoDto cursoDto)
        {
            try
            {
                var cursoCriado = await _cursoService.Adicionar(cursoDto);
                return Ok(cursoCriado);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 500 };
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(CursoDto))]
        public async Task<IActionResult> Update(Guid id, UpdateCursoDto cursoDto)
        {
            try
            {
                var cursoAtualizado = await _cursoService.Atualizar(id, cursoDto);
                return Ok(cursoAtualizado);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 500 };
            }
        }
    }
}