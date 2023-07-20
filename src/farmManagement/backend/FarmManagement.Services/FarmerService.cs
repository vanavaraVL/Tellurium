using AutoMapper;
using FarmManagement.Dal.Entities;
using FarmManagement.Dal.Exceptions;
using FarmManagement.Dal.Repositories;
using FarmManagement.Models.Dto;
using FarmManagement.Models.Responses;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace FarmManagement.Services;

public interface IFarmerService
{
    Task<ResponseResultDto<IReadOnlyList<AnimalDto>>> GetAll();

    Task<ResponseResultDto<AnimalDto>> GetByName(string nameBase64);

    Task<ResponseResultDto<AnimalDto>> CreateNewItem(AnimalDto entityDto);

    Task<ResponseResultDto<AnimalDto>> EditItem(AnimalDto entityDto, string nameBase64);

    Task<ResponseResultDto<bool>> DeleteItem(string nameBase64);
}

public sealed class FarmerService: IFarmerService
{
    private readonly IAnimalRepository _animalRepository;
    private readonly IMapper _mapper;

    public FarmerService(IAnimalRepository animalRepository, 
        IMapper mapper)
    {
        _animalRepository = animalRepository ?? throw new ArgumentNullException(nameof(animalRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ResponseResultDto<IReadOnlyList<AnimalDto>>> GetAll()
    {
        var entityList = await _animalRepository.GetAsync();

        return new ResponseResultDto<IReadOnlyList<AnimalDto>>()
        {
            ResultItem = _mapper.Map<IReadOnlyList<AnimalDto>>(entityList)
        };
    }

    public async Task<ResponseResultDto<AnimalDto>> GetByName(string nameBase64)
    {
        var entity = await GetAnimalByBase64Name(nameBase64);

        return new ResponseResultDto<AnimalDto>()
        {
            ResultItem = _mapper.Map<AnimalDto>(entity)
        };
    }

    public async Task<ResponseResultDto<AnimalDto>> CreateNewItem(AnimalDto entityDto)
    {
        var entity = _mapper.Map<AnimalEntity>(entityDto);

        await _animalRepository.AddAsync(entity);

        return new ResponseResultDto<AnimalDto>()
        {
            ResultItem = _mapper.Map<AnimalDto>(entity)
        };
    }

    public async Task<ResponseResultDto<AnimalDto>> EditItem(AnimalDto entityDto, string nameBase64)
    {
        var name = GetName(nameBase64);
        var entity = await GetAnimalByBase64Name(nameBase64);

        _mapper.Map(entityDto, entity);

        await _animalRepository.UpdateAsync(entity, name);

        return new ResponseResultDto<AnimalDto>()
        {
            ResultItem = _mapper.Map<AnimalDto>(entity)
        };
    }

    public async Task<ResponseResultDto<bool>> DeleteItem(string nameBase64)
    {
        var entity = await GetAnimalByBase64Name(nameBase64);

        await _animalRepository.RemoveAsync(entity);

        return new ResponseResultDto<bool>()
        {
            ResultItem = true
        };
    }

    private async Task<AnimalEntity> GetAnimalByBase64Name(string nameBase64)
    {
        var name = GetName(nameBase64);

        var entity = await _animalRepository.GetByNameAsync(name);

        if (entity is null)
        {
            throw new EntityNotFoundException(name);
        }
        return entity;
    }

    private static string GetName(string nameBase64) => Encoding.UTF8.GetString(Convert.FromBase64String(nameBase64));
}