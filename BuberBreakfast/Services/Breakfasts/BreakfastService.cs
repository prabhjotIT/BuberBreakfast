using BuberBreakfast.Models;
using BuberBreakfast.ServiceErrors;
using ErrorOr;

namespace BuberBreakfast.Services.Breakfasts;

public class BreakfastService : IBreakfastService
{
    private static readonly Dictionary<Guid, Breakfast> _breakfasts =new();
    public ErrorOr<Created> CreateBreakfast(Breakfast breakfast)
    {// TODO: i have to store data in some database using entity core framework here
       _breakfasts.Add(breakfast.Id,breakfast);
       return Result.Created;
    }

    public ErrorOr<Deleted> DeletBreakfast(Guid id)
    {
        _breakfasts.Remove(id);
        return Result.Deleted;
    }

    public ErrorOr<Breakfast> GetBreakFast(Guid id)
    {
        if(_breakfasts.TryGetValue(id,out var breakfast))
        return _breakfasts[id];
        else 
        return Errors.Breakfast.NotFound;
    }

    public ErrorOr<UpsertedBreakfast> UpsertBreakfast(Breakfast breakfast)
    {
        var IsNewlyCreated =!_breakfasts.ContainsKey(breakfast.Id);
        _breakfasts[breakfast.Id]=breakfast;

        return new UpsertedBreakfast(IsNewlyCreated);
    }
}