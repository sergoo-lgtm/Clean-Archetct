using eTickets.Models;
using eTickets.UnitOfWork;
using eTickets.DTO.CinemaDTOS;
using eTickets.Middlewares;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Service;

public class CinemaService
{
    private readonly IUnitOfWork _unitOfWork;

    public CinemaService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<CinemaDto>> GetAllCinemasAsync()
    {
        var cinemas = await _unitOfWork.Cinemas.GetAll
            .AsQueryable()
            .ToListAsync();

        return cinemas.Select(c => new CinemaDto
        {
            Id = c.Id,
            Name = c.Name,
            Logo = c.Logo,
            Description = c.Description
        });
    }

    public async Task<CinemaDto> GetCinemaAsync(int id)
    {
        var cinema = await _unitOfWork.Cinemas.GetByIdAsync(id);

        if (cinema == null)
            throw new KeyNotFoundException($"Cinema with ID {id} not found.");

        return new CinemaDto
        {
            Id = cinema.Id,
            Name = cinema.Name,
            Logo = cinema.Logo,
            Description = cinema.Description
        };
    }

    public async Task<CinemaDto> AddCinemaAsync(CinemaInputDto dto)
    {
        var isExist = _unitOfWork.Cinemas.GetAll.Any(c => c.Name.ToLower() == dto.Name.ToLower());

        if (isExist)
        {
            throw new BusinessException($"Cinema name '{dto.Name}' already exists.");
        }

        var newCinema = new Cinema(dto.Name, dto.Logo, dto.Description);

        await _unitOfWork.Cinemas.AddAsync(newCinema);
        await _unitOfWork.SaveChangesAsync();

        return new CinemaDto
        {
            Id = newCinema.Id,
            Name = newCinema.Name,
            Logo = newCinema.Logo,
            Description = newCinema.Description
        };
    }

    public async Task<CinemaDto> UpdateCinemaAsync(int id, CinemaInputDto dto)
    {
        var existingCinema = await _unitOfWork.Cinemas.GetByIdAsync(id);

        if (existingCinema == null)
            throw new KeyNotFoundException($"Cinema with ID {id} not found.");

        var allCinemas = _unitOfWork.Cinemas.GetAll;
        var isDuplicate = allCinemas.Any(c => c.Name.ToLower() == dto.Name.ToLower() && c.Id != id);

        if (isDuplicate)
        {
            throw new BusinessException("Cannot update this name because it is already used by another cinema.");
        }

        existingCinema.Update(dto.Name, dto.Logo, dto.Description);

        await _unitOfWork.Cinemas.UpdateAsync(existingCinema);
        await _unitOfWork.SaveChangesAsync();

        return new CinemaDto
        {
            Id = existingCinema.Id,
            Name = existingCinema.Name,
            Logo = existingCinema.Logo,
            Description = existingCinema.Description
        };
    }

    public async Task<bool> DeleteCinemaAsync(int id)
    {
        var cinema = await _unitOfWork.Cinemas.GetByIdAsync(id);

        if (cinema == null)
            throw new KeyNotFoundException($"Cinema with ID {id} not found.");

        await _unitOfWork.Cinemas.RemoveAsync(id);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<CinemaInputDto> GetCinemaForEditAsync(int id)
    {
        var cinema = await _unitOfWork.Cinemas.GetByIdAsync(id);

        if (cinema == null)
            throw new KeyNotFoundException($"Cinema with ID {id} not found.");

        return new CinemaInputDto
        {
            Name = cinema.Name,
            Logo = cinema.Logo,
            Description = cinema.Description
        };
    }
}
