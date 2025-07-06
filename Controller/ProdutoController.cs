using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechStoreApi.Dto;

namespace TechStoreApi.Controller
{
    [Route("produto")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        protected readonly Context _context;
        public ProdutoController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProduto([FromQuery] string? nome, [FromQuery] decimal? precoMin,
        [FromQuery] decimal? precoMax, [FromQuery] int Pag, [FromQuery] int PagTamanho)
        {
            IQueryable<Produto> query = _context.Produtos.AsQueryable();
            if (nome != null)
            {
                query = query.Where(p => p.Nome.Contains(nome));
            }
            if (precoMin != null)
            {
                query = query.Where(p => p.Preco >= precoMin);
            }
            if (precoMax != null)
            {
                query = query.Where(p => p.Preco <= precoMax);
            }

            int produtosEncontrados = await query.CountAsync();
            List<Produto> produtos = await query.Skip((Pag - 1) * PagTamanho).Take(PagTamanho).ToListAsync();

            return Ok(new { produtosEncontrados, produtos });
        }

        [HttpPost]
        public async Task<IActionResult> PostProduto([FromBody] ProdutoPost produto)
        {
            if (produto.Nome == null)
            {
                return BadRequest("O campo de nome não pode está vazio");
            }
            if (produto.Preco <= 0 || produto.Estoque <= 0)
            {
                return BadRequest("O preço e estoques deve possuir um valor e não podem ser negativos");
            }
            Produto novoProduto = new Produto
            {
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco,
                Estoque = produto.Estoque,
            };
            _context.Produtos.Add(novoProduto);
            await _context.SaveChangesAsync();

            return Created();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto(int id, [FromBody] ProdutoPost produto)
        {
            Produto buscaProduto = _context.Produtos.Where(p => p.ProdutoId == id).First();
            if (buscaProduto == null)
            {
                return BadRequest("Não produto com este id");
            }
            if (produto.Nome == null)
            {
                return BadRequest("O campo de nome não pode está vazio");
            }
            if (produto.Preco <= 0 || produto.Estoque <= 0)
            {
                return BadRequest("O preço e estoques deve possuir um valor e não podem ser negativos");
            }
            buscaProduto.Nome = produto.Nome;
            buscaProduto.Descricao = produto.Descricao;
            buscaProduto.Preco = produto.Preco;
            buscaProduto.Estoque = produto.Estoque;
            buscaProduto.DataCadastro = DateTime.Now;
            _context.SaveChanges();

            return Ok("Produto atualizado com sucesso!");
        }
    }    
}