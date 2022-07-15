using CursoOnline.Application.Contratos;
using CursoOnline.Application.Dtos.Matriculas;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CursoOnline.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MatriculasController : ControllerBase
    {
        private readonly IMatriculaService _matriculaService;

        public MatriculasController(IMatriculaService matriculaService)
        {
            _matriculaService = matriculaService;
        }

        /// <summary>
        /// Cria uma matricula
        /// </summary>
        /// <param name="createMatriculaDto"></param>
        /// <returns>MatriculaDto</returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(MatriculaDto))]
        public async Task<IActionResult> Post(CreateMatriculaDto createMatriculaDto)
        {
            try
            {
                var matriculaCriada = await _matriculaService.Adicionar(createMatriculaDto);
                return Ok(matriculaCriada);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 500 };
            }
        }

        /// <summary>
        /// Obtem uma matricula pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>MatriculaDto</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(MatriculaDto))]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var matricula = await _matriculaService.ObterPorId(id);
                return Ok(matricula);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 500 };
            }
        }

        /// <summary>
        /// Obtem lista de matriculas
        /// </summary>
        /// <returns>List<MatriculaDto></MatriculaDto></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<MatriculaDto>))]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var matriculas = await _matriculaService.ObterMatriculas();
                return Ok(matriculas);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 500 };
            }
        }

        /// <summary>
        /// Atualiza uma matricula
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateMatriculaDto"></param>
        /// <returns>MatriculaDto</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(MatriculaDto))]
        public async Task<IActionResult> Update(Guid id, UpdateMatriculaDto updateMatriculaDto)
        {
            try
            {
                var matriculaAtualizada = await _matriculaService.Atualizar(id, updateMatriculaDto);
                return Ok(matriculaAtualizada);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 500 };
            }
        }

        /// <summary>
        /// Deleta uma matricula
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _matriculaService.Deletar(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 500 };
            }
        }
    }
}