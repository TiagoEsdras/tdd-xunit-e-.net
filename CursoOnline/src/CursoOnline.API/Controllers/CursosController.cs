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
    public class CursosController : ControllerBase
    {
        private readonly ICursoService _cursoService;

        public CursosController(ICursoService cursoService)
        {
            _cursoService = cursoService;
        }

        /// <summary>
        /// Cria um curso
        /// </summary>
        /// <param name="cursoDto"></param>
        /// <returns>CursoDto</returns>
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

        /// <summary>
        /// Obtem a lista de todos cursos
        /// </summary>
        /// <returns>List<CursoDto></CursoDto></returns>
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

        /// <summary>
        /// Obtem um curso pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>CursoDto</returns>
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

        /// <summary>
        /// Atualiza um curso
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cursoDto"></param>
        /// <returns>CursoDto</returns>
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

        /// <summary>
        /// Deleta um curso
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _cursoService.Deletar(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 500 };
            }
        }
    }
}