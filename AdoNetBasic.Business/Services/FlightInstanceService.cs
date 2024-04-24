using AdoNetBasic.Business.Dtos;
using AdoNetBasic.Business.Exceptions;
using AdoNetBasic.Business.Mappers;
using AdoNetBasic.Models;
using AdoNetBasic.Repositories;

namespace AdoNetBasic.Business.Services;

internal sealed class FlightInstanceService : BaseService, IFlightInstanceService
{
    private readonly IFlightInstanceRepository _flightInstanceRepository;
    private readonly IPlaneDetailRepository _planeDetailRepository;
    private readonly IPlaneModelRepository _planeModelRepository;
    private readonly IPilotRepository _pilotRepository;
    private readonly IFlightAttendantRepository _flightAttendantRepository;
    private readonly IFlightRepository _flightRepository;

    public FlightInstanceService(IFlightInstanceRepository flightInstanceRepository, IPlaneDetailRepository planeDetailRepository,
        IPlaneModelRepository planeModelRepository, IPilotRepository pilotRepository, IFlightAttendantRepository flightAttendantRepository,
        IFlightRepository flightRepository)
    {
        _flightInstanceRepository = flightInstanceRepository;
        _planeDetailRepository = planeDetailRepository;
        _planeModelRepository = planeModelRepository;
        _pilotRepository = pilotRepository;
        _flightAttendantRepository = flightAttendantRepository;
        _flightRepository = flightRepository;
    }

    /// <summary>
    /// Retrieve flight instances having leaving date and time within the given range
    /// </summary>
    /// <param name="startDateTimeLeave">Beginning date and time of the range</param>
    /// <param name="endDateTimeLeave">Ending date and time of the range</param>
    public async Task<IReadOnlyList<FlightInstanceDto>> GetAllFlightInstancesWithinDateTimeLeaveRangeAsync(DateTime startDateTimeLeave, DateTime endDateTimeLeave)
    {
        try
        {
            IReadOnlyList<FlightInstance> flightInstances = await _flightInstanceRepository.GetWithinDateTimeLeaveRangeAsync(startDateTimeLeave, endDateTimeLeave);

            if (!flightInstances.Any())
                throw new NoDataException(nameof(FlightInstance));

            return await GetFlightInstanceDtosAsync(flightInstances);
        }
        catch (Exception ex)
        {
            LogError($"{nameof(GetAllFlightInstancesWithinDateTimeLeaveRangeAsync)} failure: {ex.Message}");
            return Array.Empty<FlightInstanceDto>();
        }
    }

    /// <summary>
    /// Retrieve flight instances served by a plane produced by the given manufacturer
    /// </summary>
    /// <param name="planeManufacturerName">Name of the plane's manufacturer</param>
    public async Task<IReadOnlyList<FlightInstanceDto>> GetAllFlightInstancesServedByPlaneManufacturerAsync(string planeManufacturerName)
    {
        try
        {
            IReadOnlyList<FlightInstance> flightInstances = await _flightInstanceRepository.GetByPlaneManufacturerNameAsync(planeManufacturerName);

            if (!flightInstances.Any())
                throw new NoDataException(nameof(FlightInstance));

            return await GetFlightInstanceDtosAsync(flightInstances);
        }
        catch (Exception ex)
        {
            LogError($"{nameof(GetAllFlightInstancesServedByPlaneManufacturerAsync)} failure: {ex.Message}");
            return Array.Empty<FlightInstanceDto>();
        }
    }

    /// <summary>
    /// Update the co-pilot for the flight instance having the given flight number and departure date and time
    /// </summary>
    /// <param name="flightNo">Given flight number</param>
    /// <param name="dateTimeLeave">Given departure date and time</param>
    /// <param name="coPilotFirstName">First name of the updated co-pilot</param>
    /// <param name="coPilotLastName">Last name of the updated co-pilot</param>
    /// <returns>Number of affected rows</returns>
    public async Task<int> UpdateFlightInstanceCoPilotAsync(string flightNo, DateTime dateTimeLeave, string coPilotFirstName, string coPilotLastName)
    {
        try
        {
            FlightInstance flightInstance = await _flightInstanceRepository.GetByFlightNoAndDateTimeLeaveAsync(flightNo, dateTimeLeave)
                ?? throw new NoDataException(nameof(FlightInstance));

            Pilot coPilot = await _pilotRepository.GetByFirstNameAndLastNameAsync(coPilotFirstName, coPilotLastName)
                ?? throw new ItemNotFoundException(nameof(Pilot), $"{coPilotFirstName} {coPilotLastName}");

            flightInstance.CoPilotAboardId = coPilot.PilotId;

            return await _flightInstanceRepository.UpdateAsync(flightInstance);
        }
        catch (Exception ex)
        {
            LogError($"{nameof(UpdateFlightInstanceCoPilotAsync)} failure: {ex.Message}");
            return 0;
        }
    }

    /// <summary>
    /// Update the arrival date and time for the flight instances departing from the given airport by setting the given delay
    /// </summary>
    /// <param name="airportCode">Code of the departure airport</param>
    /// <param name="delay">Delay on arrival date and time</param>
    /// <returns>Number of affected rows</returns>
    public async Task<int> SetDelayForFlightInstancesArrivingFromAirportAsync(string airportCode, TimeSpan delay)
    {
        try
        {
            if (delay.TotalHours == 0)
                throw new InvalidOperationException("Delay cannot be zero");

            IReadOnlyList<FlightInstance> flightInstances = await _flightInstanceRepository.GetByFlightArriveFromAirportCodeAsync(airportCode);

            if (!flightInstances.Any())
                throw new NoDataException(nameof(FlightInstance));

            foreach (FlightInstance flightInstance in flightInstances)
                flightInstance.DateTimeArrive += delay;

            return await _flightInstanceRepository.UpdateRangeAsync(flightInstances);
        }
        catch (Exception ex)
        {
            LogError($"{nameof(SetDelayForFlightInstancesArrivingFromAirportAsync)} failure: {ex.Message}");
            return 0;
        }
    }

    #region Locals

    private async Task<IReadOnlyList<FlightInstanceDto>> GetFlightInstanceDtosAsync(IReadOnlyList<FlightInstance> flightInstances)
    {
        List<FlightInstanceDto> results = new();

        foreach (FlightInstance flightInstance in flightInstances)
            results.Add(await GetFlightInstanceDtoAsync(flightInstance));

        return results;
    }

    private async Task<FlightInstanceDto> GetFlightInstanceDtoAsync(FlightInstance flightInstance)
    {
        Flight flight = await _flightRepository.GetByFlightNoAsync(flightInstance.FlightNo)
            ?? throw new ItemNotFoundException(nameof(Flight), flightInstance.FlightNo);
        PlaneDto planeDto = await GetPlaneDtoAsync(flightInstance.PlaneId);
        PilotDto pilotDto = await GetPilotDtoAsync(flightInstance.PilotAboardId);
        IReadOnlyList<AttendantDto> allAttendantsDtos = await GetAllAttendantsDtosAsync(flightInstance.InstanceId);

        return FlightInstanceMapper.Map(flightInstance, flight, planeDto, pilotDto, allAttendantsDtos);
    }

    private async Task<PlaneDto> GetPlaneDtoAsync(int planeId)
    {
        PlaneDetail planeDetail = await _planeDetailRepository.GetByPlaneIdAsync(planeId)
            ?? throw new ItemNotFoundException(nameof(PlaneDetail), $"{planeId}");

        PlaneModel planeModel = await _planeModelRepository.GetByModelNumberAsync(planeDetail.ModelNumber)
            ?? throw new ItemNotFoundException(nameof(PlaneModel), $"{planeDetail.ModelNumber}");

        return PlaneMapper.Map(planeDetail, planeModel);
    }

    private async Task<PilotDto> GetPilotDtoAsync(int pilotId)
    {
        Pilot pilot = await _pilotRepository.GetByPilotIdAsync(pilotId)
            ?? throw new ItemNotFoundException(nameof(Pilot), $"{pilotId}");

        int age = (int)Math.Floor((DateTime.Now - pilot.Dob).TotalDays / 365);

        return PilotMapper.Map(pilot, age);
    }

    private async Task<IReadOnlyList<AttendantDto>> GetAllAttendantsDtosAsync(int flightInstanceId)
    {
        IReadOnlyList<FlightAttendant> flightAttendants = await _flightAttendantRepository.GetAllByFlightInstanceIdAsync(flightInstanceId);
        List<AttendantDto> attendantDtos = new();

        foreach (FlightAttendant flightAttendant in flightAttendants)
            attendantDtos.Add(await GetAttendantDtoAsync(flightAttendant));

        return attendantDtos;
    }

    private async Task<AttendantDto> GetAttendantDtoAsync(FlightAttendant flightAttendant)
    {
        bool isMentor = await _flightAttendantRepository.IsMentorAsync(flightAttendant.AttendantId);
        return AttendantMapper.Map(flightAttendant, isMentor);
    }

    #endregion
}
