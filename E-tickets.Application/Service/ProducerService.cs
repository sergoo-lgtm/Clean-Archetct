using eTickets.Models;
using eTickets.UnitOfWork;
using eTickets.DTO.ProducerDTOS;
using eTickets.Middlewares;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Service;

public class ProducerService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProducerService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ProducerDto>> GetAllProducersAsync()
    {
        var producers = await _unitOfWork.Producers.GetAll
            .AsQueryable()
            .ToListAsync();

        return producers.Select(p => new ProducerDto
        {
            Id = p.Id,
            FullName = p.FullName,
            ProfilePictureURL = p.ProfilePictureURL,
            Bio = p.Bio
        });
    }

    public async Task<ProducerDto> GetProducerAsync(int id)
    {
        var producer = await _unitOfWork.Producers.GetByIdAsync(id);

        if (producer == null)
            throw new KeyNotFoundException($"Producer with ID {id} not found.");

        return new ProducerDto
        {
            Id = producer.Id,
            FullName = producer.FullName,
            ProfilePictureURL = producer.ProfilePictureURL,
            Bio = producer.Bio
        };
    }

    public async Task<ProducerDto> AddProducerAsync(ProducerInputDto dto)
    {
        var isExist = _unitOfWork.Producers.GetAll.Any(p => p.FullName.ToLower() == dto.FullName.ToLower());

        if (isExist)
        {
            throw new BusinessException($"Producer name '{dto.FullName}' already exists.");
        }

        var newProducer = new Producer(dto.FullName, dto.ProfilePictureURL, dto.Bio);

        await _unitOfWork.Producers.AddAsync(newProducer);
        await _unitOfWork.SaveChangesAsync();

        return new ProducerDto
        {
            Id = newProducer.Id,
            FullName = newProducer.FullName,
            ProfilePictureURL = newProducer.ProfilePictureURL,
            Bio = newProducer.Bio
        };
    }

    public async Task<ProducerDto> UpdateProducerAsync(int id, ProducerInputDto dto)
    {
        var existingProducer = await _unitOfWork.Producers.GetByIdAsync(id);

        if (existingProducer == null)
            throw new KeyNotFoundException($"Producer with ID {id} not found.");

        var allProducers = _unitOfWork.Producers.GetAll;
        var isDuplicate = allProducers.Any(p => p.FullName.ToLower() == dto.FullName.ToLower() && p.Id != id);

        if (isDuplicate)
        {
            throw new BusinessException("Cannot update this name because it is already used by another producer.");
        }

        existingProducer.Update(dto.FullName, dto.ProfilePictureURL, dto.Bio);

        await _unitOfWork.Producers.UpdateAsync(existingProducer);
        await _unitOfWork.SaveChangesAsync();

        return new ProducerDto
        {
            Id = existingProducer.Id,
            FullName = existingProducer.FullName,
            ProfilePictureURL = existingProducer.ProfilePictureURL,
            Bio = existingProducer.Bio
        };
    }

    public async Task<bool> DeleteProducerAsync(int id)
    {
        var producer = await _unitOfWork.Producers.GetByIdAsync(id);

        if (producer == null)
            throw new KeyNotFoundException($"Producer with ID {id} not found.");

        await _unitOfWork.Producers.RemoveAsync(id);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<ProducerInputDto> GetProducerForEditAsync(int id)
    {
        var producer = await _unitOfWork.Producers.GetByIdAsync(id);

        if (producer == null)
            throw new KeyNotFoundException($"Producer with ID {id} not found.");

        return new ProducerInputDto
        {
            FullName = producer.FullName,
            ProfilePictureURL = producer.ProfilePictureURL,
            Bio = producer.Bio
        };
    }
}
