using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using e_commerce_blackcat_api.Repositories;
using e_commerce_blackcat_api.Interfaces;
using e_commerce_blackcat_api.Src.Dtos;
using e_commerce_blackcat_api.Src.Models;
using e_commerce_blackcat_api.Src.Mappers;
using e_commerce_blackcat_api.Helpers;


namespace e_commerce_blackcat_api.Src.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(ILogger<ProductController> logger, IUnitOfWork unitOfWork) : ControllerBase
{
    private readonly ILogger<ProductController> _logger = logger;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    [HttpGet]
    public async Task<ActionResult<ApiResponse<object>>> GetAll([FromQuery] ProductParams queryParams)
    {
        var pagedResult = await _unitOfWork.ProductRepository.GetPagedProductsAsync(queryParams);

        var dtoList = pagedResult.Items.Select(p => p.ToProductDto());

        var response = new
        {
            products = dtoList,
            pagination = new
            {
                currentPage = pagedResult.PageNumber,
                pageSize = pagedResult.PageSize,
                totalItems = pagedResult.TotalCount,
                totalPages = (int)Math.Ceiling((double)pagedResult.TotalCount / pagedResult.PageSize)
            }
        };

        return Ok(new ApiResponse<object>(true, "Productos obtenidos correctamente", response));
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<ProductDto>>> GetById(int id)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
        if (product == null)
            return NotFound(new ApiResponse<ProductDto>(false, "Producto no encontrado"));

        return Ok(new ApiResponse<ProductDto>(true, "Producto encontrado", product.ToProductDto()));
    }

    [Authorize(Roles = "Administrador")]
    [HttpPost]
    public async Task<ActionResult<ApiResponse<ProductDto>>> Create([FromBody] ProductCreateDto dto)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            return BadRequest(new ApiResponse<ProductDto>(false, "Datos inválidos", default, errors));
        }

        var product = dto.ToProductFromCreateDto();
        await _unitOfWork.ProductRepository.AddAsync(product);
        var saved = await _unitOfWork.CompleteAsync() ;

        if (!saved)
            return StatusCode(500, new ApiResponse<ProductDto>(false, "No se pudo guardar el producto"));

        return CreatedAtAction(nameof(GetById), new { id = product.Id }, new ApiResponse<ProductDto>(true, "Producto creado exitosamente", product.ToProductDto()));
    }

    [Authorize(Roles = "Administrador")]
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<ProductDto>>> Update(int id, [FromBody] ProductUpdateDto dto)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
        if (product == null)
            return NotFound(new ApiResponse<ProductDto>(false, "Producto no encontrado"));

        product.UpdateProductFromDto(dto);
        await _unitOfWork.ProductRepository.Update(product);
        var saved = await _unitOfWork.CompleteAsync();

        if (!saved)
            return StatusCode(500, new ApiResponse<ProductDto>(false, "No se pudo actualizar el producto"));

        return Ok(new ApiResponse<ProductDto>(true, "Producto actualizado exitosamente", product.ToProductDto()));
    }

    [Authorize(Roles = "Administrador")]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<string>>> Delete(int id)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
        if (product == null)
            return NotFound(new ApiResponse<string>(false, "Producto no encontrado"));

        _unitOfWork.ProductRepository.Delete(product);
        var saved = await _unitOfWork.CompleteAsync();

        if (!saved)
            return StatusCode(500, new ApiResponse<string>(false, "No se pudo eliminar el producto"));

        return Ok(new ApiResponse<string>(true, "Producto eliminado correctamente", "OK"));
    }
}
