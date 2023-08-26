 namespace BuberBreakfast.Contracts.Breakfast;
    public record CreateBreakfastRequest(
        Guid Id,
        string Name,
        string Description,
        DateTime StartDateTime, 
        DateTime EndDateTime,
        List<string>Savory,
        List<string>Sweet
    );