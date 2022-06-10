using CursoOnline.Application.Contratos;
using CursoOnline.Application.Dtos.Alunos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CursoOnline.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlunosController : ControllerBase
    {
        private readonly IAlunoService _alunoService;

        public AlunosController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(AlunoDto))]
        public async Task<IActionResult> Post(CreateAlunoDto createAlunoDto)
        {
            try
            {
                var alunoCriado = await _alunoService.Adicionar(createAlunoDto);
                return Ok(alunoCriado);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 500 };
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Update(Guid id, UpdateAlunoDto updateAlunoDto)
        {
            try
            {
                await _alunoService.Atualizar(id, updateAlunoDto);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 500 };
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(AlunoDto))]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var aluno = await _alunoService.ObterPorId(id);
                return Ok(aluno);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 500 };
            }
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<AlunoDto>))]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var alunos = await _alunoService.ObterLista();
                return Ok(alunos);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 500 };
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _alunoService.Deletar(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 500 };
            }
        }
    }
}