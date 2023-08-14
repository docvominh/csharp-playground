using WebApi.Data;
using WebApi.Data.Repository;
using WebApi.Exceptions;
using WebApi.Mapper;
using WebApi.Models;

namespace WebApi.Services;

public interface IProductService
{
    public Task<Product> FindById(int id);
    public Task<Product> Save(Product product);
    public Task<Product> Update(Product product);
    public Task Delete(int id);
    public Task<List<Product>> FindAll();
}

internal class ProductService : IProductService
{
    private readonly IProductMapper _mapper;
    private readonly UnitOfWork _unitOfWork;

    public ProductService(IProductMapper mapper, UnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Product> FindById(int id)
    {
        var entity = await _unitOfWork.ProductRepository.FindById(id);
        if (entity == null)
        {
            throw new ResourceNotFoundException($"No entity found with id {id}");
        }

        return _mapper.ToModel(entity);
    }

    public async Task<Product> Save(Product product)
    {
        var entity = _mapper.ToEntity(product);
        entity = _unitOfWork.ProductRepository.Save(entity);
        await _unitOfWork.Commit();

        return _mapper.ToModel(entity);
    }

    public async Task<Product> Update(Product product)
    {
        var entity = _mapper.ToEntity(product);
        entity = _unitOfWork.ProductRepository.Update(entity);
        await _unitOfWork.Commit();

        return _mapper.ToModel(entity);
    }

    public async Task Delete(int id)
    {
        var task = _unitOfWork.ProductRepository.FindById(id);
        if (task.Result != null)
        {
            _unitOfWork.ProductRepository.Delete(task.Result);
        }

        await _unitOfWork.Commit();
    }

    public async Task<List<Product>> FindAll()
    {
        var entities = await _unitOfWork.ProductRepository.FindAll();
        return entities.Select(p => _mapper.ToModel(p)).ToList();
    }
}