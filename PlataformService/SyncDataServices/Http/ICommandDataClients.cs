using PlataformService.Dtos;

namespace PlataformService.SyncDataServices.Http
{
    public interface ICommandDataClients
    {
        Task SendPlatformToCommand(PlatformReadDto plat);
    }
}
