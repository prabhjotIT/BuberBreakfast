using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.Models;
using ErrorOr;

namespace BuberBreakfast.Services.Breakfasts;

public interface IBreakfastService {
    ErrorOr<Created>  CreateBreakfast(Breakfast breakfast);
    ErrorOr<Deleted> DeletBreakfast(Guid id);
    ErrorOr<Breakfast> GetBreakFast(Guid id);
    ErrorOr<UpsertedBreakfast> UpsertBreakfast(Breakfast breakfast);
}