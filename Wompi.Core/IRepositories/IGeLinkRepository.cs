using Wompi.Core.EntityModels;
using Wompi.Core.EntityModels.API.Request;

namespace Wompi.Core.IRepositories
{
    public interface IGeLinkRepository
    {
        Task<GeLink> CreateWompiLink(WompiLinkRequest wompiLinkRequest);
    }
}
