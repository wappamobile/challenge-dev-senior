using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Wappa.Framework.Model.Pessoa;
using System.Linq;
using Wappa.Framework.Model.Comum;
using Wappa.Framework.Data;
using Wappa.Service.Geocoder;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Wappa.Framework.Model;

namespace Wappa.Api.Driver.Controllers
{
    /// <summary>
    /// Api para manutencao de motoristas
    /// </summary>
    [Produces("application/json")]
    [Route("api/Driver")]
    public class DriverController : Controller
    {
        /// <summary>
        /// Consulta motoristas por nome e/ou sobrenome
        /// </summary>
        /// <remarks>M�todo respons�vel por disponibilizar uma busca por nome e/ou sobrenome</remarks>
        /// <param name="context">contexto de base</param>
        /// <param name="nome">Nome que ser� usado como filtro de busca</param>
        /// <param name="sobrenome">Sobrenome que ser� usado como filtro de busca</param>
        [HttpGet]
        public IEnumerable<Motorista> Get([FromServices]DriverContext context, [FromQuery]string nome, [FromQuery]string sobrenome)
        {
            IEnumerable<Motorista> resposta = null;

            if (!string.IsNullOrWhiteSpace(nome) || !string.IsNullOrWhiteSpace(sobrenome))
                resposta = BuscarPorNomeSobrenome(context, nome, sobrenome);

            return resposta;
        }

        /// <summary>
        /// Consulta um motorista por id
        /// </summary>
        /// <remarks>M�todo respons�vel por obter um motorista por id</remarks>
        /// <param name="context">contexto de base</param>
        /// <param name="id">Identificador do motorista</param>
        [HttpGet("{id}", Name = "Get")]
        public Motorista Get([FromServices]DriverContext context, long id)
        {
            IQueryable<Motorista> query = from m in context.Motoristas
                                          .Include(m => m.Endereco)
                                          .Include(m => m.Carro)
                                          where m.PessoaId == id
                                          select m;

            return query.SingleOrDefault();
        }

        /// <summary>
        /// Cadastra um motorista
        /// </summary>
        /// <remarks>M�todo respons�vel por criar um motorista</remarks>
        /// <param name="motorista">Dados do motorista</param>
        /// <param name="context">contexto de base</param>
        /// <param name="geocodingService">contexto de servico</param>
        [HttpPost]
        public void Post([FromServices]DriverContext context, [FromServices]IGeocodingService geocodingService, [FromBody]Motorista motorista)
        {
            CriarMotorista(context, motorista);
            ObterCoordenadas(geocodingService, motorista);
            context.SaveChanges();
        }

        /// <summary>
        /// Atualiza um motorista
        /// </summary>
        /// <remarks>M�todo respons�vel por atualizar um motorista</remarks>
        /// <param name="id">Identificador</param>
        /// <param name="motorista">Dados Atualizados</param>
        /// <param name="context">contexto de base</param>
        [HttpPut("{id}")]
        public void Put([FromServices]DriverContext context, long id, [FromBody]Motorista motorista)
        {
            try
            {
                if (ValidarMotoristaExistente(context, id))
                {
                    Motorista motoristaAtual = ObterMotorista(context, id);

                    AtualizarDadosMotorista(motoristaAtual, motorista);

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                if (ex is HttpResponseException)
                    throw;

                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Remove um motorista
        /// </summary>
        /// <remarks>M�todo respons�vel por remover um motorista</remarks>
        /// <param name="id">Identificador</param>
        /// <param name="context">contexto de base</param>
        [HttpDelete("{id}")]
        public void Delete([FromServices]DriverContext context, long id)
        {
            try
            {
                if (ValidarMotoristaExistente(context, id))
                {
                    Motorista motorista = ObterMotorista(context, id);

                    context.Motoristas.Remove(motorista);
                    context.Carros.Remove(motorista.Carro);
                    context.Enderecos.Remove(motorista.Endereco);

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                if (ex is HttpResponseException)
                    throw;

                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        private void ObterCoordenadas(IGeocodingService geocodingService, Motorista motorista)
        {
            Task<Endereco> task = Task.Run(() => geocodingService.ObterLocalizacaoAsync(motorista.Endereco));
            task.Wait();
        }

        private void AtualizarDadosMotorista(Motorista motoristaAtual, Motorista motorista)
        {
            motoristaAtual.Atualizar(motorista);
        }

        private Motorista ObterMotorista(DriverContext context, long id)
        {
            return context.Motoristas
                .Include(m => m.Carro)
                .Include(m => m.Endereco)
                .Where(m => m.PessoaId == id).Single();
        }

        private void CriarMotorista(DriverContext context, Motorista motorista)
        {
            try
            {
                ValidarMotoristaExistente(context, motorista);

                context.Motoristas.Add(motorista);

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex is HttpResponseException)
                    throw;

                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        private void ValidarMotoristaExistente(DriverContext context, Motorista motorista)
        {
            int existente = context.Motoristas.Where(c => c.Nome == motorista.Nome && c.Sobrenome == motorista.Sobrenome).Count();

            if (existente > 0)
                throw new HttpResponseException(HttpStatusCode.Conflict);
        }

        private bool ValidarMotoristaExistente(DriverContext context, long id)
        {
            int existente = context.Motoristas.Where(c => c.PessoaId == id).Count();

            return existente > 0;
        }

        private void ValidarRequisicao(Motorista motorista)
        {
            if (motorista == null)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
        }

        private static IEnumerable<Motorista> BuscarPorNomeSobrenome(DriverContext context, string nome, string sobrenome)
        {
            try
            {
                IQueryable<Motorista> query = from m in context.Motoristas
                                              .Include(m => m.Endereco)
                                              .Include(m => m.Carro)
                                              select m;

                if (!string.IsNullOrWhiteSpace(nome))
                    query = query.Where(c => c.Nome.Contains(nome));

                if (!string.IsNullOrWhiteSpace(sobrenome))
                    query = query.Where(c => c.Sobrenome.Contains(sobrenome));

                IEnumerable<Motorista> motoristas = query.OrderBy(c => c.NomeCompleto).ToList();

                return motoristas.Any() ? motoristas : null;
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
    }
}
