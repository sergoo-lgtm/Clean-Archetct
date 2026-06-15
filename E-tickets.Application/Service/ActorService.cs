using eTickets.Models;
using eTickets.UnitOfWork;
using eTickets.DTO.ActorDTOS;
using eTickets.Middlewares;

namespace eTickets.Service;

public class ActorService
{
    private readonly IUnitOfWork _unitOfWork;

    public ActorService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IEnumerable<ActorDto> GetAllActors()
    {
        var actors = _unitOfWork.Actors.GetAll.ToList();
        
        return actors.Select(a => new ActorDto
        {
            Id = a.Id,
            FullName = a.FullName,
            ProfilePicture = a.ProfilePivture, 
            Bio = a.Bio
        });
    }

    public async Task<ActorDto> GetActorAsync(int id)
    {
        var actor = await _unitOfWork.Actors.GetByIdAsync(id);
        
        if (actor == null) 
            throw new KeyNotFoundException($"Actor with ID {id} not found.");

        return new ActorDto
        {
            Id = actor.Id,
            FullName = actor.FullName,
            ProfilePicture = actor.ProfilePivture,
            Bio = actor.Bio
        };
    }
    public async Task<ActorDto> AddActorAsync(ActorInputDto dto)
    {
        var isExist = _unitOfWork.Actors.GetAll.Any(a => a.FullName.ToLower() == dto.FullName.ToLower());

        if (isExist)
        {
            throw new BusinessException($"Actor name '{dto.FullName}' already exists.");
        }

        var newActor = new Actor(dto.FullName, dto.ProfilePicture, dto.Bio);

        await _unitOfWork.Actors.AddAsync(newActor);
        await _unitOfWork.SaveChangesAsync(); 

        return new ActorDto
        {
            Id = newActor.Id,
            FullName = newActor.FullName,
            ProfilePicture = newActor.ProfilePivture,
            Bio = newActor.Bio
        };
    }

    public async Task<ActorDto> UpdateAsync(int id, ActorInputDto dto)
    {
        var existingActor = await _unitOfWork.Actors.GetByIdAsync(id);

        if (existingActor == null)
            throw new KeyNotFoundException($"Actor with ID {id} not found.");
        
        var allActors =  _unitOfWork.Actors.GetAll;
        var isDuplicate = allActors.Any(a => a.FullName.ToLower() == dto.FullName.ToLower() && a.Id != id);

        if (isDuplicate)
        {
            throw new BusinessException("Cannot update this name because it is already used by another actor.");
        }

        existingActor.Update(dto.FullName, dto.ProfilePicture, dto.Bio);

        await _unitOfWork.Actors.UpdateAsync(existingActor);
        await _unitOfWork.SaveChangesAsync();

        return new ActorDto
        {
            Id = existingActor.Id,
            FullName = existingActor.FullName,
            ProfilePicture = existingActor.ProfilePivture,
            Bio = existingActor.Bio
        };
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var actor = await _unitOfWork.Actors.GetByIdAsync(id); 
        
        if (actor == null)
            throw new KeyNotFoundException($"Actor with ID {id} not found.");
        
        await _unitOfWork.Actors.RemoveAsync(id);
        await _unitOfWork.SaveChangesAsync();
        
        return true; 
    }
}